using MOBZize.Properties;
using System.CodeDom;
using System.Collections;
using System.Diagnostics;
using System.Net.Http.Json;

namespace MOBZize
{
  public partial class MOBZizeForm : Form
  {
    private const int UpdateMs = 200;
    private const string ToolName = "MOBZize";

    private ImageList _imageList = new();

    private string _lastOpenedPath;
    private bool _cancelled = false;

    private const string ICON_FOLDER = nameof(Resources.folder_o);
    private const string ICON_FOLDER_OPEN = nameof(Resources.folder_open_o);

    private const string ICON_FILE = nameof(Resources.file_o);

    private const string ICON_ERROR = nameof(Resources.exclamation);

    private string _titleBase = $"{ToolName} v{Application.ProductVersion}";

    // For list view sorting: default on size descending
    private bool _sortAscending = false;
    private int _sortColumnIndex = 1;

    private ToolStripStatusLabel[] _columnStatusLabels;

    // The current selected list item (a SizeItem, i.e. directory or file)
    private SizeItem SelectedListItem => (SizeItem)_listView.SelectedItems[0].Tag;
    // The current selected tree directory
    private SizeDirectory SelectedTreeDirectory => (SizeDirectory)_treeView.SelectedNode.Tag;


    public MOBZizeForm()
    {
      InitializeComponent();

      Icon = Resources.MobZize;
      Text = _titleBase;

      // Set up the image list at 24x24, 32-bits
      _imageList.ColorDepth = ColorDepth.Depth32Bit;
      _imageList.ImageSize = new Size(24, 24);

      _imageList.Images.Add(ICON_FILE, Resources.file_o);
      _imageList.Images.Add(ICON_FOLDER, Resources.folder_o);
      _imageList.Images.Add(ICON_FOLDER_OPEN, Resources.folder_open_o);
      _imageList.Images.Add(ICON_ERROR, Resources.exclamation);

      _treeView.ImageList = _imageList;
      _listView.SmallImageList = _imageList;

      _lastOpenedPath = Environment.CurrentDirectory;

      // Show only the default tool strip
      _loadingStatusStrip.Visible = false;

      // Start by sorting on size descending
      _listView.ListViewItemSorter = new SizeSorter(false);

      // The status labels corresponding to the list view columns
      _columnStatusLabels = new[] {
        _listNameStatusLabel,
        _listSizeStatusLabel,
        _listPercentageStatusLabel,
        _listFoldersStatusLabel,
        _listFilesStatusLabel,
        _listBytesStatusLabel
      };

      foreach (var label in _columnStatusLabels)
        label.Text = "";
    }

    /// <summary>
    /// Load the folder specified on the command line if specified
    /// </summary>
    private async void Form_Load(object sender, EventArgs e)
    {
      // [0] is the name of the executable, [1] is a folder to open
      if (Environment.GetCommandLineArgs().Length > 1)
      {
        string path = Environment.GetCommandLineArgs()[1];
        await LoadSizesAsync(path);
        // Save this path for Refresh and Open
        _lastOpenedPath = Path.GetFullPath(path);
      }

      try
      {
        var client = new HttpClient();
        var versionString = await client.GetFromJsonAsync<string>($"https://www.mobzystems.com/api/toolversion?t={ToolName}") ?? "";
        if (versionString != "")
        {
          var version = new Version(versionString);
          if (version > new Version(Application.ProductVersion))
          {
            _updateAvailableButton.Text = "Update available";
            _updateAvailableButton.ToolTipText = $"{ToolName} {version.ToString(3)} is available";
            _updateAvailableButton.Visible = true;
          }
        }
      }
      catch
      {
        // Ignore errors checking for updates
      }
    }

    private async Task<string> GetToolVersion(string toolName)
    {
      var client = new HttpClient();
      var version = await client.GetFromJsonAsync<string>("https://www.mobzystems.com/api/toolversion?t=" + toolName);
      return version ?? "";
    }

    /// <summary>
    /// Load the sizes of a folder and subfolders
    /// </summary>
    /// <param name="path">The path to load. May be relative</param>
    private async Task LoadSizesAsync(string path)
    {
      _splitContainer.Enabled = false;
      _splitContainer.UseWaitCursor = true; // Doesn't work?

      try
      {
        // We haven't cancelled yet
        _cancelled = false;

        // Swap the normal status strip and the loading status strip:
        _topStatusStrip.Visible = false;
        _loadingLabel.Text = $"Loading '{path}'...";
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

      // Once we loaded, we can refresh:
      _refreshToolButton.Enabled = true;
    }

    /// <summary>
    /// Add a node and subnodes to the tree
    /// </summary>
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
      else
      {
        node.ImageKey = ICON_FOLDER;
        node.SelectedImageKey = ICON_FOLDER_OPEN;
      }
    }

    /// <summary>
    /// Return a "Nice size" string for a length in bytes, e.g. "1.2 GB"
    /// </summary>
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
    /// Return a percentage of a whole
    /// </summary>
    private string PercentageOf(long n, long total)
    {
      if (total == 0)
        return "-";
      return ((double)n / total).ToString("##0%");
    }

    #region "List view sorting"
    /// <summary>
    /// Base class for all list view sorters
    /// </summary>
    private abstract class ListViewSorter : IComparer
    {
      // This factor is +1 for ascending or -1 for descending
      protected int _factor;

      public ListViewSorter(bool ascending)
      {
        _factor = ascending ? 1 : -1;
      }

      public abstract int CompareItems(SizeItem item1, SizeItem item2);

      /// <summary>
      /// This is the ICompare.Compare method. Delegates to the abstract CompareItems
      /// method
      /// </summary>
      /// <param name="x">The first object (a list view item). Never null!</param>
      /// <param name="y">The second object, also never null</param>
      public int Compare(object? x, object? y) =>
        // Compare the SizeItems associated with the list items
        // Multiply with the factor to support descending sorts
        _factor * CompareItems((SizeItem)((ListViewItem)x!).Tag, (SizeItem)((ListViewItem)y!).Tag);

      /// <summary>
      /// Helper method to sort folders (1) before files (2).
      /// Independent of ascending/descending sort order!
      /// </summary>
      public int FoldersFirst(SizeItem item1, SizeItem item2) =>
        _factor * (item1 is SizeDirectory ? 1 : 2).CompareTo(item2 is SizeDirectory ? 1 : 2);
    }

    /// <summary>
    /// Sorter on name
    /// </summary>
    private class NameSorter : ListViewSorter
    {
      public NameSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        if (item1.GetType() == item2.GetType())
          return string.Compare(item1.Name, item2.Name, true);
        return FoldersFirst(item1, item2);
      }
    }

    /// <summary>
    /// Sort on size (in bytes)
    /// </summary>
    private class SizeSorter : ListViewSorter
    {
      public SizeSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2) =>
        item1.SizeInBytes.CompareTo(item2.SizeInBytes);
    }

    private abstract class DirectoryOnlySorter : ListViewSorter
    {
      public DirectoryOnlySorter(bool ascending) : base(ascending) { }

      public abstract int CompareDirectories(SizeDirectory dir1, SizeDirectory dir2);

      /// <summary>
      /// Sort directories only; sort files on name, after directories
      /// </summary>
      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        // dir-dir: total files
        if (item1 is SizeDirectory dir1 && item2 is SizeDirectory dir2)
          return CompareDirectories(dir1, dir2);
        // file-file: name
        if (item1 is SizeFile && item2 is SizeFile)
          return _factor * string.Compare(item1.Name, item2.Name, true);
        // otherwise: folder first
        return FoldersFirst(item1, item2);
      }
    }

    /// <summary>
    /// Sort on the total number of files
    /// </summary>
    private class FileCountSorter : DirectoryOnlySorter
    {
      public FileCountSorter(bool ascending) : base(ascending) { }

      public override int CompareDirectories(SizeDirectory dir1, SizeDirectory dir2) =>
        dir1.TotalFileCount.CompareTo(dir2.TotalFileCount);
    }

    /// <summary>
    /// Sort on the total number of directories
    /// </summary>
    private class DirectoryCountSorter : DirectoryOnlySorter
    {
      public DirectoryCountSorter(bool ascending) : base(ascending) { }

      public override int CompareDirectories(SizeDirectory dir1, SizeDirectory dir2) =>
        dir1.TotalDirectoryCount.CompareTo(dir2.TotalDirectoryCount);
    }

    /// <summary>
    /// Set the correct list view sorter when a column is clicked
    /// </summary>
    private void _listView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      // When clicking the already selected column, swap ascending/descending
      if (e.Column == _sortColumnIndex)
        _sortAscending = !_sortAscending;
      else
      {
        // Set a new sort column, ascending
        _sortColumnIndex = e.Column;
        _sortAscending = true;
      }

      _listView.ListViewItemSorter = e.Column switch
      {
        1 or 2 or 5 => new SizeSorter(_sortAscending), // 1: Size, 2: Percentage, 5: Bytes
        3 => new DirectoryCountSorter(_sortAscending), // Folders
        4 => new FileCountSorter(_sortAscending), // Files
        _ => new NameSorter(_sortAscending) // Name
      };
    }
    #endregion

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
        _listNameStatusLabel.Text = $"{dir.Directories.Count:#,,0} folder(s), {dir.Files.Count:#,,0} file(s)";
        _listSizeStatusLabel.Text = NiceSize(dir.SizeInBytes);
        _listPercentageStatusLabel.Text = "100%";
        _listFoldersStatusLabel.Text = dir.TotalDirectoryCount.ToString("#,,0");
        _listFilesStatusLabel.Text = dir.TotalFileCount.ToString("#,,0");
        _listBytesStatusLabel.Text = dir.SizeInBytes.ToString("#,,0");

        foreach (var subdir in dir.Directories)
          _listView.Items.Add(new ListViewItem(new[] {
            subdir.Name,
            NiceSize(subdir.SizeInBytes),
            PercentageOf(subdir.SizeInBytes, dir.SizeInBytes),
            subdir.TotalDirectoryCount.ToString("#,,0"),
            subdir.TotalFileCount.ToString("#,,0"),
            subdir.SizeInBytes.ToString("#,,0")
          },
          subdir.Exception != null ? ICON_ERROR : ICON_FOLDER)
          { Tag = subdir });

        foreach (var file in dir.Files)
          _listView.Items.Add(new ListViewItem(new[] {
            file.Name,
            NiceSize(file.SizeInBytes),
            PercentageOf(file.SizeInBytes, dir.SizeInBytes),
            "-", // Directories
            "-", // Files
            file.SizeInBytes.ToString("#,,0")
          },
          file.Exception != null ? ICON_ERROR : ICON_FILE)
          { Tag = file });

        _upToolButton.Enabled = e.Node.Level > 0;
      }
    }

    /// <summary>
    /// When a node in the tree is (not left)-clicked, select it
    /// This sets the right node for the context menu
    /// </summary>
    private void _treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Node != null && e.Button != MouseButtons.Left)
        _treeView.SelectedNode = e.Node;
    }

    /// <summary>
    /// "Open" a directory when it's double-clicked in the list view
    /// </summary>
    private void _listView_DoubleClick(object sender, EventArgs e)
    {
      // We must have a selected item on the right AND a selected folder
      if (_listView.SelectedItems.Count > 0 && _treeView.SelectedNode != null)
      {
        // Find the file object
        var dir = SelectedListItem as SizeDirectory;
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

    /// <summary>
    /// Mirror the widths of list view columns in the status labels of its status strip
    /// </summary>
    private void _listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      if (e.ColumnIndex < 0 || e.ColumnIndex >= _columnStatusLabels.Length)
        throw new ArgumentOutOfRangeException($"No status label for column index {e.ColumnIndex}");

      // Set the width of the status label to the width of the column
      _columnStatusLabels[e.ColumnIndex].Width = _listView.Columns[e.ColumnIndex].Width;
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

    private void OpenInExplorer(string path) => Process.Start("explorer.exe", $"/select,\"{path}\"");
    private void RevealInExplorer(string path) => Process.Start("explorer.exe", $"/select,\"{path}\"");

    /// <summary>
    /// "Reveal" a directory in Explorer, i.e. show its parent with
    /// this directory selected
    /// </summary>
    private void _showDirInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
        RevealInExplorer(SelectedTreeDirectory.FullName);
    }

    /// <summary>
    /// "Open" a directory in Explorer, i.e. show its contents
    /// </summary>
    private void _openDirInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
        OpenInExplorer(SelectedTreeDirectory.FullName);
    }

    /// <summary>
    /// Reveal an item in Explorer
    /// </summary>
    private void showItemInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_listView.SelectedItems.Count > 0)
        RevealInExplorer(SelectedListItem.FullName);
    }

    /// <summary>
    /// Open an item directory or reveal a file in Explorer
    /// </summary>
    private void openItemInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_listView.SelectedItems.Count > 0)
      {
        var item = SelectedListItem;
        if (item is SizeDirectory)
          OpenInExplorer(item.FullName);
        else
          RevealInExplorer(item.FullName);
      }
    }

    /// <summary>
    /// Go to parent folder
    /// </summary>
    private void _upToolButton_Click(object sender, EventArgs e)
    {
      var node = _treeView.SelectedNode;
      if (node != null && node.Level > 0)
        _treeView.SelectedNode = node.Parent;
    }

    /// <summary>
    /// Refresh, i.e. reload the current folder structure
    /// </summary>
    private async void _refreshToolButton_Click(object sender, EventArgs e)
    {
      await LoadSizesAsync(_lastOpenedPath);
    }

    /// <summary>
    /// Prepare the list view context menu. There can be no list item selected
    /// and then we need to disable some menu items
    /// </summary>
    private void _listViewContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      var hasSelectedItem = _listView.SelectedItems.Count > 0;

      _showItemInExplorerMenuItem.Enabled = hasSelectedItem;
      _openItemInExplorerMenuItem.Enabled = hasSelectedItem;
    }

    /// <summary>
    /// Open the tool home page
    /// </summary>
    private void _updateAvailableButton_Click(object sender, EventArgs e)
    {
      try
      {
        var pi = new ProcessStartInfo($"https://www.mobzystems.com/Tools/{ToolName}");
        pi.UseShellExecute = true;
        Process.Start(pi);
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Could not open web page", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}