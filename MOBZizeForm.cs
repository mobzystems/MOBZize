using Microsoft.VisualBasic.Logging;
using MOBZize.Properties;
using System.CodeDom;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Xml;

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

      // _statusLabel.Text = "Open a folder to get started.";

      _lastOpenedPath = Environment.CurrentDirectory;

      // Start by sorting on size descending
      _listView.ListViewItemSorter = new SizeSorter(false);
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

        await LoadSizesAsync(_rootPath);

        _lastOpenedPath = Path.GetFullPath(_rootPath);
      }
    }

    /// <summary>
    /// Load the security of a folder and (optionally) subfolders to a certain depth
    /// </summary>
    /// <param name="path">The path to load. May be relative</param>
    /// <param name="depth">The depth to load. 0 = all, 1 is path only, 2+ = additional levels</param>
    private async Task LoadSizesAsync(string path)
    {
      _splitContainer.Enabled = false;
      _splitContainer.UseWaitCursor = true; // Doesn't work?

      try
      {
        // Hide the Open button, show the Cancel button
        _cancelled = false;

        _loadingLabel.Text = $"Loading '{path}'...";

        _topStatusStrip.Visible = false;
        _loadingStatusStrip.Visible = true;

        // Keep a (case insensitive!) tab on which directory was added to the tree where
        var nodeDict = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);

        // Add the root path as the only node
        _treeView.Nodes.Clear();
        var firstNode = _treeView.Nodes.Add(Path.GetFullPath(path));
        firstNode.ImageKey = ICON_FOLDER;
        nodeDict.Add(firstNode.Text, firstNode);
        firstNode.Expand();

        _listView.Items.Clear();

        // We only update the status label every so many ms, to prevent it eating CPU
        var lastUpdateTime = DateTime.Now.AddHours(-1);
        // This method is called on every folder. Return true to cancel
        var callback = (string fullPath) =>
        {
          // Skip paths we already added
          if (!nodeDict.ContainsKey(fullPath))
          {
            // Find the parent:
            var parentPath = Path.GetDirectoryName(fullPath);
            // Update only first level nodes to show progress
            if (nodeDict.TryGetValue(parentPath!, out TreeNode? node) && node == firstNode)
            {
              // We found the parent: add the node
              Invoke(() =>
              {
                var newNode = node.Nodes.Add(fullPath, Path.GetFileName(fullPath));
                newNode.ImageKey = ICON_FOLDER;
                nodeDict.Add(fullPath, newNode);
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
              _loadingLabel.Text = fullPath;
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

        if (a != null)
        {
          _loadingLabel.Text = "Displaying results...";

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

        _topStatusStrip.Visible = true;
        _loadingStatusStrip.Visible = false;

        // Re-enable Open button
        _topStatusStrip.Enabled = true;
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
        var child = node.Nodes.Add(d.FullName, d.Name);
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

    private string NiceSize(long bytes)
    {
      const long _1kB = 1024L;
      const long _1MB = _1kB * 1024;
      const long _1GB = _1MB * 1024;
      if (bytes > _1GB)
        return $"{(double)bytes / _1GB:#,,0.0} GB";
      if (bytes > _1MB)
        return $"{(double)bytes / _1MB:#,,0.0} MB";
      if (bytes > _1kB)
        return $"{(double)bytes / _1kB:#,,0.0} kB";
      return $"{bytes:#,,0} B";
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
        _treeStatusLabel.Text = dir.FullName;
        _listNameStatusLabel.Text = $"{dir.Directories.Count:#,,0} directories, {dir.Files.Count:#,,0} files.";
        _listSizeStatusLabel.Text = NiceSize(dir.SizeInBytes);
        _listPercentageStatusLabel.Text = "100%";
        _listFoldersStatusLabel.Text = dir.TotalDirectoryCount.ToString("#,,0");
        _listFilesStatusLabel.Text = dir.TotalFileCount.ToString("#,,0");
        _listBytesStatusLabel.Text = dir.SizeInBytes.ToString("#,,0");

        foreach (var subdir in dir.Directories)
        {
          var item = new ListViewItem(subdir.Name);

          item.Tag = subdir;
          item.SubItems.Add(NiceSize(subdir.SizeInBytes));
          item.SubItems.Add(PercentageOf(subdir.SizeInBytes, dir.SizeInBytes));
          item.SubItems.Add(subdir.TotalDirectoryCount.ToString("#,,0"));
          item.SubItems.Add(subdir.TotalFileCount.ToString("#,,0"));
          item.SubItems.Add(subdir.SizeInBytes.ToString("#,,0"));

          if (subdir.Exception == null)
            item.ImageKey = ICON_FOLDER;
          else
            item.ImageKey = ICON_ERROR;

          _listView.Items.Add(item);
        }

        foreach (var file in dir.Files)
        {
          var item = new ListViewItem(file.Name);

          item.Tag = file;
          item.SubItems.Add(NiceSize(file.SizeInBytes));
          item.SubItems.Add(PercentageOf(file.SizeInBytes, dir.SizeInBytes));
          item.SubItems.Add("-"); // Directories
          item.SubItems.Add("-"); // Files
          item.SubItems.Add(file.SizeInBytes.ToString("#,,0"));

          if (file.Exception == null)
            item.ImageKey = ICON_FILE;
          else
            item.ImageKey = ICON_ERROR;

          _listView.Items.Add(item);
        }

        _upToolButton.Enabled = e.Node.Level > 0;
      }
    }

    private string PercentageOf(long n, long total)
    {
      if (total == 0)
        return "-";
      return ((double)n / total).ToString("##0%");
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
          await LoadSizesAsync(dlg.SelectedPath);
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
    private void _showDirInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
      {
        var path = ((SizeDirectory)_treeView.SelectedNode.Tag).FullName;
        Process.Start("explorer.exe", $"/select,\"{path}\"");
      }
    }

    private void _openDirInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
      {
        var path = ((SizeDirectory)_treeView.SelectedNode.Tag).FullName;
        Process.Start("explorer.exe", $"\"{path}\"");
      }
    }

    private void showItemInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_listView.SelectedItems.Count > 0)
      {
        var item = ((SizeItem)_listView.SelectedItems[0].Tag)!;
        Process.Start("explorer.exe", $"/select,\"{item.FullName}\"");
      }
    }


    private void openItemInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_listView.SelectedItems.Count > 0)
      {
        var item = ((SizeItem)_listView.SelectedItems[0].Tag)!;
        if (item is SizeDirectory)
          Process.Start("explorer.exe", $"\"{item.FullName}\"");
        else
          Process.Start("explorer.exe", $"/select,\"{item.FullName}\"");
      }
    }

    private void _listView_DoubleClick(object sender, EventArgs e)
    {
      // We must have a selected item on the right AND a selected folder
      if (_listView.SelectedItems.Count > 0 && _treeView.SelectedNode != null)
      {
        // Find the file object
        var item = (SizeItem)(_listView.SelectedItems[0].Tag);
        var dir = item as SizeDirectory;
        if (dir != null)
        {
          // Search the tree for the item with the right name
          var nodes = _treeView.SelectedNode.Nodes.Find(dir.FullName, false);
          if (nodes.Length == 1)
          {
            // If we found one, select it
            _treeView.SelectedNode = nodes[0];
            nodes[0].EnsureVisible();
          }
        }
      }
    }

    private bool _sortAscending = false;
    private int _sortColumnIndex = 1;

    private abstract class ListViewSorter : IComparer
    {
      protected int _factor;

      public ListViewSorter(bool ascending)
      {
        _factor = ascending ? 1 : -1;
      }

      public abstract int CompareItems(SizeItem item1, SizeItem item2);

      public int Compare(object? x, object? y)
      {
        //if (x == null && y == null)
        //  return 0;
        //if (x != null && y != null)
        return _factor * CompareItems((SizeItem)((ListViewItem)x!).Tag, (SizeItem)((ListViewItem)y!).Tag);
        //return y == null ? 1 : -1;
      }
    }

    private class NameSorter : ListViewSorter
    {
      public NameSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        if (item1.GetType() == item2.GetType())
          return string.Compare(item1.Name, item2.Name, true);
        return _factor * (item1 is SizeDirectory ? 1 : 2).CompareTo(item2 is SizeDirectory ? 1 : 2);
      }
    }

    private class SizeSorter : ListViewSorter
    {
      public SizeSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        return item1.SizeInBytes.CompareTo(item2.SizeInBytes);
      }
    }

    private class FileCountSorter : ListViewSorter
    {
      public FileCountSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        if (item1 is SizeDirectory && item2 is SizeDirectory)
          return ((SizeDirectory)item1).TotalFileCount.CompareTo(((SizeDirectory)item2).TotalFileCount);
        if (item1 is SizeFile && item2 is SizeFile)
          return string.Compare(item1.Name, item2.Name, true);
        return _factor * (item1 is SizeDirectory ? 1 : 2).CompareTo(item2 is SizeDirectory ? 1 : 2);
      }
    }

    private class FolderCountSorter : ListViewSorter
    {
      public FolderCountSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        if (item1 is SizeDirectory && item2 is SizeDirectory)
          return ((SizeDirectory)item1).TotalDirectoryCount.CompareTo(((SizeDirectory)item2).TotalDirectoryCount);
        if (item1 is SizeFile && item2 is SizeFile)
          return string.Compare(item1.Name, item2.Name, true);
        return _factor * (item1 is SizeDirectory ? 1 : 2).CompareTo(item2 is SizeDirectory ? 1 : 2);
      }
    }

    private void _listView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      if (e.Column == _sortColumnIndex)
        _sortAscending = !_sortAscending;
      else
      {
        _sortColumnIndex = e.Column;
        _sortAscending = true;
      }

      _listView.ListViewItemSorter = e.Column switch
      {
        1 => new SizeSorter(_sortAscending), // Size
        2 => new SizeSorter(_sortAscending), // Percentage
        3 => new FolderCountSorter(_sortAscending), // Folders
        4 => new FileCountSorter(_sortAscending), // Files
        5 => new SizeSorter(_sortAscending), // Bytes
        _ => new NameSorter(_sortAscending) // Name
      };
    }

    private void _listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      switch (e.ColumnIndex)
      {
        case 1:
          _listSizeStatusLabel.Width = _listView.Columns[1].Width;
          break;
        case 2:
          _listPercentageStatusLabel.Width = _listView.Columns[2].Width;
          break;
        case 3:
          _listFoldersStatusLabel.Width = _listView.Columns[3].Width;
          break;
        case 4:
          _listFilesStatusLabel.Width = _listView.Columns[4].Width;
          break;
        case 5:
          _listBytesStatusLabel.Width = _listView.Columns[5].Width;
          break;
        default:
          _listNameStatusLabel.Width = _listView.Columns[0].Width;
          break;
      }
    }

    private void _upToolButton_Click(object sender, EventArgs e)
    {
      var node = _treeView.SelectedNode;
      if (node != null && node.Level > 0)
        _treeView.SelectedNode = node.Parent;
    }
  }
}