using MOBZize.Properties;
using System.CodeDom;
using System.Collections;
using System.Diagnostics;

namespace MOBZize
{
  public partial class MOBZizeForm : Form
  {
    private const int UpdateMs = 200;

    private ImageList _imageList = new();

    private string _lastOpenedPath;
    private bool _cancelled = false;

    private const string ICON_FOLDER = nameof(Resources.folder_o);
    private const string ICON_FOLDER_OPEN = nameof(Resources.folder_open_o);

    private const string ICON_FILE = nameof(Resources.file_o);

    private const string ICON_ERROR = nameof(Resources.exclamation);

    private string _titleBase = $"MOBZize v{Application.ProductVersion}";

    // For list view sorting: default on size descending
    private bool _sortAscending = false;
    private int _sortColumnIndex = 1;

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
      public int Compare(object? x, object? y)
      {
        // Compare the SizeItems associated with the list items
        // Muliply with the factor to support descending sorts
        return _factor * CompareItems((SizeItem)((ListViewItem)x!).Tag, (SizeItem)((ListViewItem)y!).Tag);
      }

      /// <summary>
      /// Helper method to sort folders (1) before files (2).
      /// Independent of ascending/descending sort order!
      /// </summary>
      public int FoldersFirst(SizeItem item1, SizeItem item2)
      {
        return _factor * (item1 is SizeDirectory ? 1 : 2).CompareTo(item2 is SizeDirectory ? 1 : 2);
      }
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

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        return item1.SizeInBytes.CompareTo(item2.SizeInBytes);
      }
    }

    /// <summary>
    /// Sort on the total number of files
    /// </summary>
    private class FileCountSorter : ListViewSorter
    {
      public FileCountSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        // dir-dir: total files
        if (item1 is SizeDirectory && item2 is SizeDirectory)
          return ((SizeDirectory)item1).TotalFileCount.CompareTo(((SizeDirectory)item2).TotalFileCount);
        // file-file: name
        if (item1 is SizeFile && item2 is SizeFile)
          return string.Compare(item1.Name, item2.Name, true);
        // otherwise: folder first
        return FoldersFirst(item1, item2);
      }
    }

    private class DirectoryCountSorter : ListViewSorter
    {
      public DirectoryCountSorter(bool ascending) : base(ascending) { }

      public override int CompareItems(SizeItem item1, SizeItem item2)
      {
        if (item1 is SizeDirectory && item2 is SizeDirectory)
          return ((SizeDirectory)item1).TotalDirectoryCount.CompareTo(((SizeDirectory)item2).TotalDirectoryCount);
        if (item1 is SizeFile && item2 is SizeFile)
          return string.Compare(item1.Name, item2.Name, true);
        // otherwise: folder first
        return FoldersFirst(item1, item2);
      }
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
        1 => new SizeSorter(_sortAscending), // Size
        2 => new SizeSorter(_sortAscending), // Percentage
        3 => new DirectoryCountSorter(_sortAscending), // Folders
        4 => new FileCountSorter(_sortAscending), // Files
        5 => new SizeSorter(_sortAscending), // Bytes
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
        _listNameStatusLabel.Text = $"{dir.Directories.Count:#,,0} directories, {dir.Files.Count:#,,0} files";
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

    /// <summary>
    /// Mirror the widths of list view columns in the status labels of its status strip
    /// </summary>
    private void _listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      switch (e.ColumnIndex)
      {
        case 0:
          _listNameStatusLabel.Width = _listView.Columns[0].Width;
          break;
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
          throw new NotImplementedException($"No status label for index {e.ColumnIndex}");
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

    /// <summary>
    /// "Reveal" a directory in Explorer, i.e. show its parent with
    /// this directory selected
    /// </summary>
    private void _showDirInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
      {
        var path = ((SizeDirectory)_treeView.SelectedNode.Tag).FullName;
        Process.Start("explorer.exe", $"/select,\"{path}\"");
      }
    }

    /// <summary>
    /// "Open" a directory in Explorer, i.e. show its contents
    /// </summary>
    private void _openDirInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
      {
        var path = ((SizeDirectory)_treeView.SelectedNode.Tag).FullName;
        Process.Start("explorer.exe", $"\"{path}\"");
      }
    }

    /// <summary>
    /// Reveal an item in Explorer
    /// </summary>
    private void showItemInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_listView.SelectedItems.Count > 0)
      {
        var item = ((SizeItem)_listView.SelectedItems[0].Tag)!;
        Process.Start("explorer.exe", $"/select,\"{item.FullName}\"");
      }
    }

    /// <summary>
    /// Open an item directory or reveal a file in Explorer
    /// </summary>
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
  }
}