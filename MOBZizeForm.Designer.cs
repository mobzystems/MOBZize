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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.ColumnHeader _nameColumn;
      System.Windows.Forms.ColumnHeader _sizeColumn;
      System.Windows.Forms.ColumnHeader _percentageColumn;
      System.Windows.Forms.ColumnHeader _filesColumn;
      System.Windows.Forms.ColumnHeader _directoriesColumn;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MOBZizeForm));
      this._splitContainer = new System.Windows.Forms.SplitContainer();
      this._treeView = new System.Windows.Forms.TreeView();
      this._treeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._showInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._openInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._treeStatusStrip = new System.Windows.Forms.StatusStrip();
      this._treeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._listView = new System.Windows.Forms.ListView();
      this._bytesColumn = new System.Windows.Forms.ColumnHeader();
      this._listViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.showItemInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openItemInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._listStatusStrip = new System.Windows.Forms.StatusStrip();
      this._listNameStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._listSizeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._listPercentageStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._listFoldersStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._listFilesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._listBytesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._topStatusStrip = new System.Windows.Forms.StatusStrip();
      this._openButton = new System.Windows.Forms.ToolStripDropDownButton();
      this._refreshToolButtpn = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this._upToolButton = new System.Windows.Forms.ToolStripDropDownButton();
      this._loadingStatusStrip = new System.Windows.Forms.StatusStrip();
      this._cancelButton = new System.Windows.Forms.ToolStripDropDownButton();
      this._loadingLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      _nameColumn = new System.Windows.Forms.ColumnHeader();
      _sizeColumn = new System.Windows.Forms.ColumnHeader();
      _percentageColumn = new System.Windows.Forms.ColumnHeader();
      _filesColumn = new System.Windows.Forms.ColumnHeader();
      _directoriesColumn = new System.Windows.Forms.ColumnHeader();
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
      this._splitContainer.Panel1.SuspendLayout();
      this._splitContainer.Panel2.SuspendLayout();
      this._splitContainer.SuspendLayout();
      this._treeViewContextMenu.SuspendLayout();
      this._treeStatusStrip.SuspendLayout();
      this._listViewContextMenu.SuspendLayout();
      this._listStatusStrip.SuspendLayout();
      this._topStatusStrip.SuspendLayout();
      this._loadingStatusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // _nameColumn
      // 
      _nameColumn.Text = "Name";
      _nameColumn.Width = 200;
      // 
      // _sizeColumn
      // 
      _sizeColumn.Text = "Size";
      _sizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      _sizeColumn.Width = 100;
      // 
      // _percentageColumn
      // 
      _percentageColumn.Text = "%";
      _percentageColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      _percentageColumn.Width = 75;
      // 
      // _filesColumn
      // 
      _filesColumn.Text = "Files";
      _filesColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      _filesColumn.Width = 100;
      // 
      // _directoriesColumn
      // 
      _directoriesColumn.Text = "Folders";
      _directoriesColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      _directoriesColumn.Width = 100;
      // 
      // _splitContainer
      // 
      this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this._splitContainer.Location = new System.Drawing.Point(0, 60);
      this._splitContainer.Name = "_splitContainer";
      // 
      // _splitContainer.Panel1
      // 
      this._splitContainer.Panel1.Controls.Add(this._treeView);
      this._splitContainer.Panel1.Controls.Add(this._treeStatusStrip);
      // 
      // _splitContainer.Panel2
      // 
      this._splitContainer.Panel2.Controls.Add(this._listView);
      this._splitContainer.Panel2.Controls.Add(this._listStatusStrip);
      this._splitContainer.Size = new System.Drawing.Size(1142, 454);
      this._splitContainer.SplitterDistance = 378;
      this._splitContainer.TabIndex = 0;
      // 
      // _treeView
      // 
      this._treeView.ContextMenuStrip = this._treeViewContextMenu;
      this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this._treeView.FullRowSelect = true;
      this._treeView.HideSelection = false;
      this._treeView.Location = new System.Drawing.Point(0, 0);
      this._treeView.Name = "_treeView";
      this._treeView.Size = new System.Drawing.Size(378, 432);
      this._treeView.TabIndex = 0;
      this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeView_AfterSelect);
      this._treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._treeView_NodeMouseClick);
      // 
      // _treeViewContextMenu
      // 
      this._treeViewContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
      this._treeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._showInExplorerMenuItem,
            this._openInExplorerMenuItem});
      this._treeViewContextMenu.Name = "_treeViewContextMenu";
      this._treeViewContextMenu.Size = new System.Drawing.Size(238, 48);
      // 
      // _showInExplorerMenuItem
      // 
      this._showInExplorerMenuItem.Name = "_showInExplorerMenuItem";
      this._showInExplorerMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
      this._showInExplorerMenuItem.Size = new System.Drawing.Size(237, 22);
      this._showInExplorerMenuItem.Text = "Show in &Explorer";
      this._showInExplorerMenuItem.Click += new System.EventHandler(this._showDirInExplorerMenuItem_Click);
      // 
      // _openInExplorerMenuItem
      // 
      this._openInExplorerMenuItem.Name = "_openInExplorerMenuItem";
      this._openInExplorerMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
      this._openInExplorerMenuItem.Size = new System.Drawing.Size(237, 22);
      this._openInExplorerMenuItem.Text = "&Open in Explorer";
      this._openInExplorerMenuItem.Click += new System.EventHandler(this._openDirInExplorerMenuItem_Click);
      // 
      // _treeStatusStrip
      // 
      this._treeStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._treeStatusLabel});
      this._treeStatusStrip.Location = new System.Drawing.Point(0, 432);
      this._treeStatusStrip.Name = "_treeStatusStrip";
      this._treeStatusStrip.Size = new System.Drawing.Size(378, 22);
      this._treeStatusStrip.SizingGrip = false;
      this._treeStatusStrip.TabIndex = 1;
      this._treeStatusStrip.Text = "statusStrip1";
      // 
      // _treeStatusLabel
      // 
      this._treeStatusLabel.Name = "_treeStatusLabel";
      this._treeStatusLabel.Size = new System.Drawing.Size(363, 17);
      this._treeStatusLabel.Spring = true;
      this._treeStatusLabel.Text = "Ready";
      this._treeStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _listView
      // 
      this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            _nameColumn,
            _sizeColumn,
            _percentageColumn,
            _directoriesColumn,
            _filesColumn,
            this._bytesColumn});
      this._listView.ContextMenuStrip = this._listViewContextMenu;
      this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
      this._listView.FullRowSelect = true;
      this._listView.HideSelection = true;
      this._listView.Location = new System.Drawing.Point(0, 0);
      this._listView.Name = "_listView";
      this._listView.Size = new System.Drawing.Size(760, 432);
      this._listView.TabIndex = 0;
      this._listView.UseCompatibleStateImageBehavior = false;
      this._listView.View = System.Windows.Forms.View.Details;
      this._listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._listView_ColumnClick);
      this._listView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this._listView_ColumnWidthChanged);
      this._listView.DoubleClick += new System.EventHandler(this._listView_DoubleClick);
      // 
      // _bytesColumn
      // 
      this._bytesColumn.Text = "Bytes";
      this._bytesColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this._bytesColumn.Width = 150;
      // 
      // _listViewContextMenu
      // 
      this._listViewContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
      this._listViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showItemInExplorerMenuItem,
            this.openItemInExplorerMenuItem});
      this._listViewContextMenu.Name = "_treeViewContextMenu";
      this._listViewContextMenu.Size = new System.Drawing.Size(163, 48);
      // 
      // showItemInExplorerMenuItem
      // 
      this.showItemInExplorerMenuItem.Name = "showItemInExplorerMenuItem";
      this.showItemInExplorerMenuItem.Size = new System.Drawing.Size(162, 22);
      this.showItemInExplorerMenuItem.Text = "Show in &Explorer";
      this.showItemInExplorerMenuItem.Click += new System.EventHandler(this.showItemInExplorerMenuItem_Click);
      // 
      // openItemInExplorerMenuItem
      // 
      this.openItemInExplorerMenuItem.Name = "openItemInExplorerMenuItem";
      this.openItemInExplorerMenuItem.Size = new System.Drawing.Size(162, 22);
      this.openItemInExplorerMenuItem.Text = "&Open in Explorer";
      this.openItemInExplorerMenuItem.Click += new System.EventHandler(this.openItemInExplorerMenuItem_Click);
      // 
      // _listStatusStrip
      // 
      this._listStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._listNameStatusLabel,
            this._listSizeStatusLabel,
            this._listPercentageStatusLabel,
            this._listFoldersStatusLabel,
            this._listFilesStatusLabel,
            this._listBytesStatusLabel});
      this._listStatusStrip.Location = new System.Drawing.Point(0, 432);
      this._listStatusStrip.Name = "_listStatusStrip";
      this._listStatusStrip.Size = new System.Drawing.Size(760, 22);
      this._listStatusStrip.TabIndex = 1;
      this._listStatusStrip.Text = "statusStrip2";
      // 
      // _listNameStatusLabel
      // 
      this._listNameStatusLabel.AutoSize = false;
      this._listNameStatusLabel.Name = "_listNameStatusLabel";
      this._listNameStatusLabel.Size = new System.Drawing.Size(37, 17);
      this._listNameStatusLabel.Text = "name";
      this._listNameStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _listSizeStatusLabel
      // 
      this._listSizeStatusLabel.AutoSize = false;
      this._listSizeStatusLabel.Name = "_listSizeStatusLabel";
      this._listSizeStatusLabel.Size = new System.Drawing.Size(26, 17);
      this._listSizeStatusLabel.Text = "size";
      this._listSizeStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // _listPercentageStatusLabel
      // 
      this._listPercentageStatusLabel.AutoSize = false;
      this._listPercentageStatusLabel.Name = "_listPercentageStatusLabel";
      this._listPercentageStatusLabel.Size = new System.Drawing.Size(66, 17);
      this._listPercentageStatusLabel.Text = "percentage";
      this._listPercentageStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // _listFoldersStatusLabel
      // 
      this._listFoldersStatusLabel.AutoSize = false;
      this._listFoldersStatusLabel.Name = "_listFoldersStatusLabel";
      this._listFoldersStatusLabel.Size = new System.Drawing.Size(43, 17);
      this._listFoldersStatusLabel.Text = "folders";
      this._listFoldersStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // _listFilesStatusLabel
      // 
      this._listFilesStatusLabel.AutoSize = false;
      this._listFilesStatusLabel.Name = "_listFilesStatusLabel";
      this._listFilesStatusLabel.Size = new System.Drawing.Size(28, 17);
      this._listFilesStatusLabel.Text = "files";
      this._listFilesStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // _listBytesStatusLabel
      // 
      this._listBytesStatusLabel.AutoSize = false;
      this._listBytesStatusLabel.Name = "_listBytesStatusLabel";
      this._listBytesStatusLabel.Size = new System.Drawing.Size(35, 17);
      this._listBytesStatusLabel.Text = "bytes";
      this._listBytesStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // _topStatusStrip
      // 
      this._topStatusStrip.Dock = System.Windows.Forms.DockStyle.Top;
      this._topStatusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
      this._topStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openButton,
            this._refreshToolButtpn,
            this.toolStripStatusLabel1,
            this._upToolButton});
      this._topStatusStrip.Location = new System.Drawing.Point(0, 0);
      this._topStatusStrip.Name = "_topStatusStrip";
      this._topStatusStrip.ShowItemToolTips = true;
      this._topStatusStrip.Size = new System.Drawing.Size(1142, 38);
      this._topStatusStrip.SizingGrip = false;
      this._topStatusStrip.TabIndex = 2;
      // 
      // _openButton
      // 
      this._openButton.Image = global::MOBZize.Properties.Resources.folder_open_o;
      this._openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._openButton.Name = "_openButton";
      this._openButton.ShowDropDownArrow = false;
      this._openButton.Size = new System.Drawing.Size(106, 36);
      this._openButton.Text = "Open folder";
      this._openButton.Click += new System.EventHandler(this._openButton_Click);
      // 
      // _refreshToolButtpn
      // 
      this._refreshToolButtpn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this._refreshToolButtpn.Enabled = false;
      this._refreshToolButtpn.Image = ((System.Drawing.Image)(resources.GetObject("_refreshToolButtpn.Image")));
      this._refreshToolButtpn.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._refreshToolButtpn.Name = "_refreshToolButtpn";
      this._refreshToolButtpn.ShowDropDownArrow = false;
      this._refreshToolButtpn.Size = new System.Drawing.Size(50, 36);
      this._refreshToolButtpn.Text = "Refresh";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(923, 33);
      this.toolStripStatusLabel1.Spring = true;
      // 
      // _upToolButton
      // 
      this._upToolButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this._upToolButton.AutoSize = false;
      this._upToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this._upToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._upToolButton.Name = "_upToolButton";
      this._upToolButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
      this._upToolButton.ShowDropDownArrow = false;
      this._upToolButton.Size = new System.Drawing.Size(17, 36);
      this._upToolButton.Text = "..";
      this._upToolButton.ToolTipText = "Go to parent folder";
      this._upToolButton.Click += new System.EventHandler(this._upToolButton_Click);
      // 
      // _loadingStatusStrip
      // 
      this._loadingStatusStrip.Dock = System.Windows.Forms.DockStyle.Top;
      this._loadingStatusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
      this._loadingStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cancelButton,
            this._loadingLabel});
      this._loadingStatusStrip.Location = new System.Drawing.Point(0, 38);
      this._loadingStatusStrip.Name = "_loadingStatusStrip";
      this._loadingStatusStrip.Size = new System.Drawing.Size(1142, 22);
      this._loadingStatusStrip.SizingGrip = false;
      this._loadingStatusStrip.TabIndex = 4;
      this._loadingStatusStrip.Text = "Loading...";
      // 
      // _cancelButton
      // 
      this._cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._cancelButton.Name = "_cancelButton";
      this._cancelButton.ShowDropDownArrow = false;
      this._cancelButton.Size = new System.Drawing.Size(47, 20);
      this._cancelButton.Text = "Cancel";
      this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
      // 
      // _loadingLabel
      // 
      this._loadingLabel.Name = "_loadingLabel";
      this._loadingLabel.Size = new System.Drawing.Size(1080, 17);
      this._loadingLabel.Spring = true;
      this._loadingLabel.Text = "...";
      this._loadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
      this.toolStripMenuItem1.Text = "Show in &Explorer";
      this.toolStripMenuItem1.Click += new System.EventHandler(this.showItemInExplorerMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(162, 22);
      this.toolStripMenuItem2.Text = "&Open in Explorer";
      this.toolStripMenuItem2.Click += new System.EventHandler(this.openItemInExplorerMenuItem_Click);
      // 
      // MOBZizeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1142, 514);
      this.Controls.Add(this._splitContainer);
      this.Controls.Add(this._loadingStatusStrip);
      this.Controls.Add(this._topStatusStrip);
      this.Name = "MOBZizeForm";
      this.Text = "MOBZize";
      this.Load += new System.EventHandler(this.MobZecForm_Load);
      this._splitContainer.Panel1.ResumeLayout(false);
      this._splitContainer.Panel1.PerformLayout();
      this._splitContainer.Panel2.ResumeLayout(false);
      this._splitContainer.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
      this._splitContainer.ResumeLayout(false);
      this._treeViewContextMenu.ResumeLayout(false);
      this._treeStatusStrip.ResumeLayout(false);
      this._treeStatusStrip.PerformLayout();
      this._listViewContextMenu.ResumeLayout(false);
      this._listStatusStrip.ResumeLayout(false);
      this._listStatusStrip.PerformLayout();
      this._topStatusStrip.ResumeLayout(false);
      this._topStatusStrip.PerformLayout();
      this._loadingStatusStrip.ResumeLayout(false);
      this._loadingStatusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private SplitContainer _splitContainer;
    private TreeView _treeView;
    private ListView _listView;
    private ContextMenuStrip _treeViewContextMenu;
    private ToolStripMenuItem _showInExplorerMenuItem;
    private ColumnHeader _bytesColumn;
    private ToolStripMenuItem _openInExplorerMenuItem;
    private ContextMenuStrip _listViewContextMenu;
    private ToolStripMenuItem showItemInExplorerMenuItem;
    private ToolStripMenuItem openItemInExplorerMenuItem;
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
        private ToolStripDropDownButton _refreshToolButtpn;
        private StatusStrip _loadingStatusStrip;
        private ToolStripDropDownButton _cancelButton;
        private ToolStripStatusLabel _loadingLabel;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripDropDownButton _upToolButton;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
    }
}