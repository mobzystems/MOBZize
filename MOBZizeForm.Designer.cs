namespace MOBZize
{
  partial class MOBZizeForm
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      ColumnHeader _nameColumn;
      ColumnHeader _sizeColumn;
      ColumnHeader _percentageColumn;
      ColumnHeader _filesColumn;
      ColumnHeader _directoriesColumn;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MOBZizeForm));
      _splitContainer = new SplitContainer();
      _treeView = new TreeView();
      _treeViewContextMenu = new ContextMenuStrip(components);
      _showDirInExplorerMenuItem = new ToolStripMenuItem();
      _openDirInExplorerMenuItem = new ToolStripMenuItem();
      _treeStatusStrip = new StatusStrip();
      _treeStatusLabel = new ToolStripStatusLabel();
      _listView = new ListView();
      _bytesColumn = new ColumnHeader();
      _lastModifiedColumn = new ColumnHeader();
      _listViewContextMenu = new ContextMenuStrip(components);
      _showItemInExplorerMenuItem = new ToolStripMenuItem();
      _openItemInExplorerMenuItem = new ToolStripMenuItem();
      _listStatusStrip = new StatusStrip();
      _listNameStatusLabel = new ToolStripStatusLabel();
      _listSizeStatusLabel = new ToolStripStatusLabel();
      _listPercentageStatusLabel = new ToolStripStatusLabel();
      _listFoldersStatusLabel = new ToolStripStatusLabel();
      _listFilesStatusLabel = new ToolStripStatusLabel();
      _listBytesStatusLabel = new ToolStripStatusLabel();
      _listLastModifiedStatusLabel = new ToolStripStatusLabel();
      _topStatusStrip = new StatusStrip();
      _openButton = new ToolStripDropDownButton();
      _refreshToolButton = new ToolStripDropDownButton();
      _updateAvailableButton = new ToolStripDropDownButton();
      _topStatusLabel = new ToolStripStatusLabel();
      _upToolButton = new ToolStripDropDownButton();
      _loadingStatusStrip = new StatusStrip();
      _cancelButton = new ToolStripDropDownButton();
      _loadingLabel = new ToolStripStatusLabel();
      _nameColumn = new ColumnHeader();
      _sizeColumn = new ColumnHeader();
      _percentageColumn = new ColumnHeader();
      _filesColumn = new ColumnHeader();
      _directoriesColumn = new ColumnHeader();
      ((System.ComponentModel.ISupportInitialize)_splitContainer).BeginInit();
      _splitContainer.Panel1.SuspendLayout();
      _splitContainer.Panel2.SuspendLayout();
      _splitContainer.SuspendLayout();
      _treeViewContextMenu.SuspendLayout();
      _treeStatusStrip.SuspendLayout();
      _listViewContextMenu.SuspendLayout();
      _listStatusStrip.SuspendLayout();
      _topStatusStrip.SuspendLayout();
      _loadingStatusStrip.SuspendLayout();
      SuspendLayout();
      // 
      // _nameColumn
      // 
      _nameColumn.Text = "Name";
      _nameColumn.Width = 200;
      // 
      // _sizeColumn
      // 
      _sizeColumn.Text = "Size";
      _sizeColumn.TextAlign = HorizontalAlignment.Right;
      _sizeColumn.Width = 100;
      // 
      // _percentageColumn
      // 
      _percentageColumn.Text = "%";
      _percentageColumn.TextAlign = HorizontalAlignment.Right;
      _percentageColumn.Width = 75;
      // 
      // _filesColumn
      // 
      _filesColumn.Text = "Files";
      _filesColumn.TextAlign = HorizontalAlignment.Right;
      _filesColumn.Width = 100;
      // 
      // _directoriesColumn
      // 
      _directoriesColumn.Text = "Folders";
      _directoriesColumn.TextAlign = HorizontalAlignment.Right;
      _directoriesColumn.Width = 100;
      // 
      // _splitContainer
      // 
      _splitContainer.Dock = DockStyle.Fill;
      _splitContainer.Location = new Point(0, 60);
      _splitContainer.Name = "_splitContainer";
      // 
      // _splitContainer.Panel1
      // 
      _splitContainer.Panel1.Controls.Add(_treeView);
      _splitContainer.Panel1.Controls.Add(_treeStatusStrip);
      // 
      // _splitContainer.Panel2
      // 
      _splitContainer.Panel2.Controls.Add(_listView);
      _splitContainer.Panel2.Controls.Add(_listStatusStrip);
      _splitContainer.Size = new Size(1317, 553);
      _splitContainer.SplitterDistance = 434;
      _splitContainer.TabIndex = 0;
      // 
      // _treeView
      // 
      _treeView.ContextMenuStrip = _treeViewContextMenu;
      _treeView.Dock = DockStyle.Fill;
      _treeView.FullRowSelect = true;
      _treeView.HideSelection = false;
      _treeView.Location = new Point(0, 0);
      _treeView.Margin = new Padding(6, 9, 6, 9);
      _treeView.Name = "_treeView";
      _treeView.Size = new Size(434, 531);
      _treeView.TabIndex = 0;
      _treeView.AfterSelect += _treeView_AfterSelect;
      _treeView.NodeMouseClick += _treeView_NodeMouseClick;
      // 
      // _treeViewContextMenu
      // 
      _treeViewContextMenu.ImageScalingSize = new Size(32, 32);
      _treeViewContextMenu.Items.AddRange(new ToolStripItem[] { _showDirInExplorerMenuItem, _openDirInExplorerMenuItem });
      _treeViewContextMenu.Name = "_treeViewContextMenu";
      _treeViewContextMenu.Size = new Size(238, 48);
      _treeViewContextMenu.Opening += _treeViewContextMenu_Opening;
      // 
      // _showDirInExplorerMenuItem
      // 
      _showDirInExplorerMenuItem.Name = "_showDirInExplorerMenuItem";
      _showDirInExplorerMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
      _showDirInExplorerMenuItem.Size = new Size(237, 22);
      _showDirInExplorerMenuItem.Text = "Show in &Explorer";
      _showDirInExplorerMenuItem.Click += _showDirInExplorerMenuItem_Click;
      // 
      // _openDirInExplorerMenuItem
      // 
      _openDirInExplorerMenuItem.Name = "_openDirInExplorerMenuItem";
      _openDirInExplorerMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.O;
      _openDirInExplorerMenuItem.Size = new Size(237, 22);
      _openDirInExplorerMenuItem.Text = "&Open in Explorer";
      _openDirInExplorerMenuItem.Click += _openDirInExplorerMenuItem_Click;
      // 
      // _treeStatusStrip
      // 
      _treeStatusStrip.Items.AddRange(new ToolStripItem[] { _treeStatusLabel });
      _treeStatusStrip.Location = new Point(0, 531);
      _treeStatusStrip.Name = "_treeStatusStrip";
      _treeStatusStrip.Padding = new Padding(1, 0, 27, 0);
      _treeStatusStrip.Size = new Size(434, 22);
      _treeStatusStrip.SizingGrip = false;
      _treeStatusStrip.TabIndex = 1;
      _treeStatusStrip.Text = "statusStrip1";
      // 
      // _treeStatusLabel
      // 
      _treeStatusLabel.Name = "_treeStatusLabel";
      _treeStatusLabel.Size = new Size(406, 17);
      _treeStatusLabel.Spring = true;
      _treeStatusLabel.Text = "Open a folder to get started";
      _treeStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // _listView
      // 
      _listView.Columns.AddRange(new ColumnHeader[] { _nameColumn, _sizeColumn, _percentageColumn, _directoriesColumn, _filesColumn, _bytesColumn, _lastModifiedColumn });
      _listView.ContextMenuStrip = _listViewContextMenu;
      _listView.Dock = DockStyle.Fill;
      _listView.FullRowSelect = true;
      _listView.HideSelection = true;
      _listView.Location = new Point(0, 0);
      _listView.Margin = new Padding(6, 9, 6, 9);
      _listView.Name = "_listView";
      _listView.Size = new Size(879, 531);
      _listView.TabIndex = 0;
      _listView.UseCompatibleStateImageBehavior = false;
      _listView.View = View.Details;
      _listView.ColumnClick += _listView_ColumnClick;
      _listView.ColumnWidthChanged += _listView_ColumnWidthChanged;
      _listView.DoubleClick += _listView_DoubleClick;
      _listView.KeyPress += _listView_KeyPress;
      // 
      // _bytesColumn
      // 
      _bytesColumn.Text = "Bytes";
      _bytesColumn.TextAlign = HorizontalAlignment.Right;
      _bytesColumn.Width = 150;
      // 
      // _lastModifiedColumn
      // 
      _lastModifiedColumn.Text = "Last modified";
      _lastModifiedColumn.Width = 150;
      // 
      // _listViewContextMenu
      // 
      _listViewContextMenu.ImageScalingSize = new Size(32, 32);
      _listViewContextMenu.Items.AddRange(new ToolStripItem[] { _showItemInExplorerMenuItem, _openItemInExplorerMenuItem });
      _listViewContextMenu.Name = "_treeViewContextMenu";
      _listViewContextMenu.Size = new Size(163, 48);
      _listViewContextMenu.Opening += _listViewContextMenu_Opening;
      // 
      // _showItemInExplorerMenuItem
      // 
      _showItemInExplorerMenuItem.Name = "_showItemInExplorerMenuItem";
      _showItemInExplorerMenuItem.Size = new Size(162, 22);
      _showItemInExplorerMenuItem.Text = "Show in &Explorer";
      _showItemInExplorerMenuItem.Click += showItemInExplorerMenuItem_Click;
      // 
      // _openItemInExplorerMenuItem
      // 
      _openItemInExplorerMenuItem.Name = "_openItemInExplorerMenuItem";
      _openItemInExplorerMenuItem.Size = new Size(162, 22);
      _openItemInExplorerMenuItem.Text = "&Open in Explorer";
      _openItemInExplorerMenuItem.Click += openItemInExplorerMenuItem_Click;
      // 
      // _listStatusStrip
      // 
      _listStatusStrip.Items.AddRange(new ToolStripItem[] { _listNameStatusLabel, _listSizeStatusLabel, _listPercentageStatusLabel, _listFoldersStatusLabel, _listFilesStatusLabel, _listBytesStatusLabel, _listLastModifiedStatusLabel });
      _listStatusStrip.Location = new Point(0, 531);
      _listStatusStrip.Name = "_listStatusStrip";
      _listStatusStrip.Padding = new Padding(1, 0, 27, 0);
      _listStatusStrip.Size = new Size(879, 22);
      _listStatusStrip.TabIndex = 1;
      _listStatusStrip.Text = "statusStrip2";
      // 
      // _listNameStatusLabel
      // 
      _listNameStatusLabel.AutoSize = false;
      _listNameStatusLabel.Name = "_listNameStatusLabel";
      _listNameStatusLabel.Size = new Size(37, 17);
      _listNameStatusLabel.Text = "name";
      _listNameStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // _listSizeStatusLabel
      // 
      _listSizeStatusLabel.AutoSize = false;
      _listSizeStatusLabel.Name = "_listSizeStatusLabel";
      _listSizeStatusLabel.Size = new Size(26, 17);
      _listSizeStatusLabel.Text = "size";
      _listSizeStatusLabel.TextAlign = ContentAlignment.MiddleRight;
      // 
      // _listPercentageStatusLabel
      // 
      _listPercentageStatusLabel.AutoSize = false;
      _listPercentageStatusLabel.Name = "_listPercentageStatusLabel";
      _listPercentageStatusLabel.Size = new Size(66, 17);
      _listPercentageStatusLabel.Text = "percentage";
      _listPercentageStatusLabel.TextAlign = ContentAlignment.MiddleRight;
      // 
      // _listFoldersStatusLabel
      // 
      _listFoldersStatusLabel.AutoSize = false;
      _listFoldersStatusLabel.Name = "_listFoldersStatusLabel";
      _listFoldersStatusLabel.Size = new Size(43, 17);
      _listFoldersStatusLabel.Text = "folders";
      _listFoldersStatusLabel.TextAlign = ContentAlignment.MiddleRight;
      // 
      // _listFilesStatusLabel
      // 
      _listFilesStatusLabel.AutoSize = false;
      _listFilesStatusLabel.Name = "_listFilesStatusLabel";
      _listFilesStatusLabel.Size = new Size(28, 17);
      _listFilesStatusLabel.Text = "files";
      _listFilesStatusLabel.TextAlign = ContentAlignment.MiddleRight;
      // 
      // _listBytesStatusLabel
      // 
      _listBytesStatusLabel.AutoSize = false;
      _listBytesStatusLabel.Name = "_listBytesStatusLabel";
      _listBytesStatusLabel.Size = new Size(35, 17);
      _listBytesStatusLabel.Text = "bytes";
      _listBytesStatusLabel.TextAlign = ContentAlignment.MiddleRight;
      // 
      // _listLastModifiedStatusLabel
      // 
      _listLastModifiedStatusLabel.Name = "_listLastModifiedStatusLabel";
      _listLastModifiedStatusLabel.Size = new Size(76, 17);
      _listLastModifiedStatusLabel.Text = "last modified";
      // 
      // _topStatusStrip
      // 
      _topStatusStrip.Dock = DockStyle.Top;
      _topStatusStrip.Items.AddRange(new ToolStripItem[] { _openButton, _refreshToolButton, _updateAvailableButton, _topStatusLabel, _upToolButton });
      _topStatusStrip.Location = new Point(0, 0);
      _topStatusStrip.Name = "_topStatusStrip";
      _topStatusStrip.Padding = new Padding(1, 0, 13, 0);
      _topStatusStrip.ShowItemToolTips = true;
      _topStatusStrip.Size = new Size(1317, 30);
      _topStatusStrip.SizingGrip = false;
      _topStatusStrip.TabIndex = 2;
      // 
      // _openButton
      // 
      _openButton.Image = Properties.Resources.folder_open_o;
      _openButton.ImageTransparentColor = Color.Magenta;
      _openButton.Name = "_openButton";
      _openButton.Padding = new Padding(4);
      _openButton.ShowDropDownArrow = false;
      _openButton.Size = new Size(98, 28);
      _openButton.Text = "Open folder";
      _openButton.ToolTipText = "Open folder to display";
      _openButton.Click += _openButton_Click;
      // 
      // _refreshToolButton
      // 
      _refreshToolButton.Enabled = false;
      _refreshToolButton.Image = (Image)resources.GetObject("_refreshToolButton.Image");
      _refreshToolButton.ImageTransparentColor = Color.Magenta;
      _refreshToolButton.Name = "_refreshToolButton";
      _refreshToolButton.Padding = new Padding(4);
      _refreshToolButton.ShowDropDownArrow = false;
      _refreshToolButton.Size = new Size(74, 28);
      _refreshToolButton.Text = "Refresh";
      _refreshToolButton.ToolTipText = "Refresh this folder";
      _refreshToolButton.Click += _refreshToolButton_Click;
      // 
      // _updateAvailableButton
      // 
      _updateAvailableButton.Image = Properties.Resources.info_circle;
      _updateAvailableButton.ImageTransparentColor = Color.Magenta;
      _updateAvailableButton.Name = "_updateAvailableButton";
      _updateAvailableButton.ShowDropDownArrow = false;
      _updateAvailableButton.Size = new Size(36, 28);
      _updateAvailableButton.Text = "...";
      _updateAvailableButton.Visible = false;
      _updateAvailableButton.Click += _updateAvailableButton_Click;
      // 
      // _topStatusLabel
      // 
      _topStatusLabel.Name = "_topStatusLabel";
      _topStatusLabel.Size = new Size(1081, 25);
      _topStatusLabel.Spring = true;
      // 
      // _upToolButton
      // 
      _upToolButton.Alignment = ToolStripItemAlignment.Right;
      _upToolButton.Enabled = false;
      _upToolButton.Image = (Image)resources.GetObject("_upToolButton.Image");
      _upToolButton.ImageTransparentColor = Color.Magenta;
      _upToolButton.Name = "_upToolButton";
      _upToolButton.Overflow = ToolStripItemOverflow.Never;
      _upToolButton.Padding = new Padding(4);
      _upToolButton.ShowDropDownArrow = false;
      _upToolButton.Size = new Size(50, 28);
      _upToolButton.Text = "Up";
      _upToolButton.ToolTipText = "Go to parent folder";
      _upToolButton.Click += _upToolButton_Click;
      // 
      // _loadingStatusStrip
      // 
      _loadingStatusStrip.Dock = DockStyle.Top;
      _loadingStatusStrip.Items.AddRange(new ToolStripItem[] { _cancelButton, _loadingLabel });
      _loadingStatusStrip.Location = new Point(0, 30);
      _loadingStatusStrip.Name = "_loadingStatusStrip";
      _loadingStatusStrip.Padding = new Padding(1, 0, 13, 0);
      _loadingStatusStrip.ShowItemToolTips = true;
      _loadingStatusStrip.Size = new Size(1317, 30);
      _loadingStatusStrip.SizingGrip = false;
      _loadingStatusStrip.TabIndex = 4;
      _loadingStatusStrip.Text = "Loading...";
      // 
      // _cancelButton
      // 
      _cancelButton.Image = (Image)resources.GetObject("_cancelButton.Image");
      _cancelButton.ImageTransparentColor = Color.Magenta;
      _cancelButton.Name = "_cancelButton";
      _cancelButton.Padding = new Padding(4);
      _cancelButton.ShowDropDownArrow = false;
      _cancelButton.Size = new Size(71, 28);
      _cancelButton.Text = "Cancel";
      _cancelButton.ToolTipText = "Cancel loading this folderl";
      _cancelButton.Click += _cancelButton_Click;
      // 
      // _loadingLabel
      // 
      _loadingLabel.Name = "_loadingLabel";
      _loadingLabel.Size = new Size(1232, 25);
      _loadingLabel.Spring = true;
      _loadingLabel.Text = "...";
      _loadingLabel.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // MOBZizeForm
      // 
      AutoScaleDimensions = new SizeF(96F, 96F);
      AutoScaleMode = AutoScaleMode.Dpi;
      ClientSize = new Size(1317, 613);
      Controls.Add(_splitContainer);
      Controls.Add(_loadingStatusStrip);
      Controls.Add(_topStatusStrip);
      Name = "MOBZizeForm";
      Text = "MOBZize";
      Load += Form_Load;
      _splitContainer.Panel1.ResumeLayout(false);
      _splitContainer.Panel1.PerformLayout();
      _splitContainer.Panel2.ResumeLayout(false);
      _splitContainer.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)_splitContainer).EndInit();
      _splitContainer.ResumeLayout(false);
      _treeViewContextMenu.ResumeLayout(false);
      _treeStatusStrip.ResumeLayout(false);
      _treeStatusStrip.PerformLayout();
      _listViewContextMenu.ResumeLayout(false);
      _listStatusStrip.ResumeLayout(false);
      _listStatusStrip.PerformLayout();
      _topStatusStrip.ResumeLayout(false);
      _topStatusStrip.PerformLayout();
      _loadingStatusStrip.ResumeLayout(false);
      _loadingStatusStrip.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private SplitContainer _splitContainer;
    private TreeView _treeView;
    private ListView _listView;
    private ContextMenuStrip _treeViewContextMenu;
    private ToolStripMenuItem _showDirInExplorerMenuItem;
    private ColumnHeader _bytesColumn;
    private ToolStripMenuItem _openDirInExplorerMenuItem;
    private ContextMenuStrip _listViewContextMenu;
    private ToolStripMenuItem _showItemInExplorerMenuItem;
    private ToolStripMenuItem _openItemInExplorerMenuItem;
    private StatusStrip _treeStatusStrip;
    private StatusStrip _listStatusStrip;
    private ToolStripStatusLabel _treeStatusLabel;
    private ToolStripStatusLabel _listNameStatusLabel;
    private ToolStripStatusLabel _listSizeStatusLabel;
    private ToolStripStatusLabel _listPercentageStatusLabel;
    private ToolStripStatusLabel _listFoldersStatusLabel;
    private ToolStripStatusLabel _listFilesStatusLabel;
    private ToolStripStatusLabel _listBytesStatusLabel;
    private StatusStrip _topStatusStrip;
    private ToolStripDropDownButton _openButton;
    private ToolStripDropDownButton _refreshToolButton;
    private StatusStrip _loadingStatusStrip;
    private ToolStripDropDownButton _cancelButton;
    private ToolStripStatusLabel _loadingLabel;
    private ToolStripStatusLabel _topStatusLabel;
    private ToolStripDropDownButton _upToolButton;
        private ToolStripDropDownButton _updateAvailableButton;
    private ColumnHeader _lastModifiedColumn;
    private ToolStripStatusLabel _listLastModifiedStatusLabel;
  }
}