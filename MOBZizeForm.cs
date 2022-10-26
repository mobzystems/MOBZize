using MOBZize.Properties;
using System.Configuration;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;

namespace MOBZize
{
  public partial class MOBZizeForm : Form
  {
    private const int UpdateMs = 200;

    private ImageList _imageList = new();

    private string? _rootPath;
    private string _lastOpenedPath;
    private bool _cancelled = false;

    private const string ICON_FOLDER = nameof(Resources.folder_o);
    private const string ICON_FOLDER_OPEN = nameof(Resources.folder_open_o);

    private const string ICON_FILE = nameof(Resources.file_o);

    private const string ICON_ERROR = nameof(Resources.exclamation);

    private string _titleBase = $"MOBZize v{Application.ProductVersion}";

    public MOBZizeForm()
    {
      InitializeComponent();

      Icon = Resources.MobZize;
      Text = _titleBase;

      _imageList.ColorDepth = ColorDepth.Depth32Bit;
      _imageList.ImageSize = new Size(24, 24);

      _imageList.Images.Add(ICON_FILE, Resources.file_o);
      _imageList.Images.Add(ICON_FOLDER, Resources.folder_o);
      _imageList.Images.Add(ICON_FOLDER_OPEN, Resources.folder_open_o);
      _imageList.Images.Add(ICON_ERROR, Resources.exclamation);

      _treeView.ImageList = _imageList;
      _listView.SmallImageList = _imageList;
      _openButton.ImageList = _imageList;
      _openButton.ImageKey = ICON_FOLDER_OPEN;

      _depthListBox.SelectedIndex = 0;

      _statusLabel.Text = "Open a directory to get started.";

      _lastOpenedPath = Environment.CurrentDirectory;
    }

    /// <summary>
    /// Load the folder specified on the command line if specified
    /// </summary>
    private async void MobZecForm_Load(object sender, EventArgs e)
    {
      // [0] is the name of the executable, [1] is a folder to open and [2] is depth
      if (Environment.GetCommandLineArgs().Length > 1)
      {
        _rootPath = Environment.GetCommandLineArgs()[1];

        //int depth = 0;
        //if (Environment.GetCommandLineArgs().Length > 2)
        //  int.TryParse(Environment.GetCommandLineArgs()[2], out depth);

        await LoadSecurityAsync(_rootPath);

        _lastOpenedPath = Path.GetFullPath(_rootPath);
        //// Update the combo box if we chose an applicable depth
        //if (depth < _depthListBox.Items.Count)
        //  _depthListBox.SelectedIndex = depth;
      }
    }

    /// <summary>
    /// Load the security of a folder and (optionally) subfolders to a certain depth
    /// </summary>
    /// <param name="path">The path to load. May be relative</param>
    /// <param name="depth">The depth to load. 0 = all, 1 is path only, 2+ = additional levels</param>
    private async Task LoadSecurityAsync(string path)
    {
      _splitContainer.Enabled = false;
      _splitContainer.UseWaitCursor = true; // Doesn't work?

      try
      {
        // Hide the Open button, show the Cancel button
        _cancelled = false;
        _openPanel.Visible = false;
        _statusLabel.Text = $"Loading '{path}'...";
        _cancelButton.Visible = true;

        // Keep a (case insensitive!) tab on which directory was added to the tree where
        var nodeDict = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);

        // Add the root path as the only node
        _treeView.Nodes.Clear();
        var firstNode = _treeView.Nodes.Add(Path.GetFullPath(path));
        nodeDict.Add(firstNode.Text, firstNode);
        firstNode.Expand();

        _listView.Items.Clear();

        // We only update the status label every so many ms, to prevent it eating CPU
        var lastUpdateTime = DateTime.Now.AddHours(-1);
        // This method is called on every folder. Return true to cancel
        var callback = (string name) =>
        {
          // Skip paths we already added
          if (!nodeDict.ContainsKey(name))
          {
            // Find the parent:
            var parentPath = Path.GetDirectoryName(name);
            // Update only first level nodes to show progress
            if (nodeDict.TryGetValue(parentPath!, out TreeNode? node) && node == firstNode)
            {
              // We found the parent: add the node
              Invoke(() =>
              {
                var newNode = node.Nodes.Add(Path.GetFileName(name));
                nodeDict.Add(name, newNode);
                // Show this node
                newNode.EnsureVisible();
                _treeView.EndUpdate();
                _treeView.Update();
                _treeView.BeginUpdate();
              });
            }
          }

          var time = DateTime.Now;
          if (time.Subtract(lastUpdateTime).TotalMilliseconds > UpdateMs)
          {
            // Update the status label USING INVOKE()
            Invoke(() =>
            {
              _statusLabel.Text = name;
            });

            lastUpdateTime = time;
          }
          return _cancelled;
        };

        // This is the result:
        SizeDirectory? a = null;

        try
        {
          // Block updates to the tree
          _treeView.BeginUpdate();
          a = await Task.Run(() => SizeDirectory.FromPath(path, 0, callback));
        }
        catch (Exception ex)
        {
          // We failed somehow - no result
          MessageBox.Show(this, ex.Message, $"Error loading '{path}'", MessageBoxButtons.OK, MessageBoxIcon.Error);
          a = null;
        }
        finally
        {
          _treeView.EndUpdate();
        }

        // Swap Open and Cancel again, disable Open while we're displaying
        _openPanel.Enabled = false;
        _openPanel.Visible = true;
        _cancelButton.Visible = false;

        if (a != null)
        {
          _statusLabel.Text = "Displaying results...";
          _topPanel.Update();

          _treeView.BeginUpdate();

          _treeView.Nodes.Clear();

          // Add the root node with its full path
          var rootNode = _treeView.Nodes.Add(a.FullName);
          ColorNode(rootNode, a);

          rootNode.Tag = a;
          AddNodes(a, rootNode);
          rootNode.Expand();
          _treeView.SelectedNode = rootNode;

          _treeView.EndUpdate();

          // Update the window title
          Text = $"{a.FullName} - {_titleBase}";
        }
        else
        {
          _statusLabel.Text = $"Failed to load '{path}'";
        }

        // Re-enable Open button
        _openPanel.Enabled = true;
      }
      finally
      {
        _splitContainer.Enabled = true;
        _splitContainer.UseWaitCursor = false;
      }
    }

    /// <summary>
    /// Add a node and subnodes to the tree
    /// </summary>
    /// <returns>Whether the tree contains any items that shoud be visible (unused)</returns>
    private void AddNodes(SizeDirectory dir, TreeNode node)
    {
      foreach (var d in dir.Directories)
      {
        var child = node.Nodes.Add(d.Name);
        child.Tag = d;
        ColorNode(child, d);

        AddNodes(d, child);
      }
    }

    /// <summary>
    /// Set node properties based on AclDirectory
    /// </summary>
    private void ColorNode(TreeNode node, SizeDirectory dir)
    {
      if (dir.Exception != null)
      {
        node.ImageKey = ICON_ERROR;
        node.SelectedImageKey = ICON_ERROR;
      }
      {
        node.ImageKey = ICON_FOLDER;
        node.SelectedImageKey = ICON_FOLDER_OPEN;
      }
    }

    /// <summary>
    /// Update the list view when a tree node is clicked
    /// </summary>
    private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      _listView.Items.Clear();

      if (e.Node != null && e.Node.Tag != null)
      {
        var dir = (SizeDirectory)e.Node.Tag;
        _statusLabel.Text = $"{dir.FullName} contains {dir.Files.Count} file(s), {dir.Directories.Count} directories. Total size: {dir.SizeInBytes:#,,0} byte(s).";

        foreach (var subdir in dir.Directories)
        {
          var item = _listView.Items.Add(subdir.Name);
          item.Tag = subdir;
          item.SubItems.Add(subdir.SizeInBytes.ToString("#,,0"));
          if (subdir.Exception == null)
            item.ImageKey = ICON_FOLDER;
          else
            item.ImageKey = ICON_ERROR;
        }

        foreach (var file in dir.Files)
        {
          var item = _listView.Items.Add(file.Name);
          item.Tag = file;
          item.SubItems.Add(file.SizeInBytes.ToString("#,,0"));
          if (file.Exception == null)
            item.ImageKey = ICON_FILE;
          else
            item.ImageKey = ICON_ERROR;
        }
      }
    }

    /// <summary>
    /// Allow the user to choose a folder to open and parse
    /// </summary>
    private async void _openButton_Click(object sender, EventArgs e)
    {
      using (var dlg = new FolderBrowserDialog())
      {
        dlg.UseDescriptionForTitle = true;
        dlg.Description = "Choose a folder to open";
        dlg.SelectedPath = _lastOpenedPath;

        if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedPath != null)
        {
          _lastOpenedPath = dlg.SelectedPath;
          await LoadSecurityAsync(dlg.SelectedPath);
        }
      }
    }

    /// <summary>
    /// Signal the current loading task that it should terminate
    /// </summary>
    private void _cancelButton_Click(object sender, EventArgs e)
    {
      _cancelled = true;
    }

    private void _treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      // Also select items using the right mouse button
      if (e.Node != null && e.Button != MouseButtons.Left)
        _treeView.SelectedNode = e.Node;
    }

    /// <summary>
    /// Default menu item for directories. Hidden if there are custom commands in AppSettings
    /// </summary>
    private void _showInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
      {
        var path = ((SizeDirectory)_treeView.SelectedNode.Tag).FullName;
        Process.Start("explorer.exe", $"/select,\"{path}\"");
      }
    }
  }
}