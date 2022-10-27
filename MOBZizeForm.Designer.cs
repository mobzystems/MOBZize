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
      this._statusBar = new System.Windows.Forms.StatusStrip();
      this._infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._splitContainer = new System.Windows.Forms.SplitContainer();
      this._treeView = new System.Windows.Forms.TreeView();
      this._treeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._showInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._openInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._listView = new System.Windows.Forms.ListView();
      this._bytesColumn = new System.Windows.Forms.ColumnHeader();
      this._topPanel = new System.Windows.Forms.Panel();
      this._statusLabel = new System.Windows.Forms.Label();
      this._cancelButton = new System.Windows.Forms.Button();
      this._openPanel = new System.Windows.Forms.Panel();
      this._openButton = new System.Windows.Forms.Button();
      this._listViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.showItemInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openItemInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      _nameColumn = new System.Windows.Forms.ColumnHeader();
      _sizeColumn = new System.Windows.Forms.ColumnHeader();
      _percentageColumn = new System.Windows.Forms.ColumnHeader();
      _filesColumn = new System.Windows.Forms.ColumnHeader();
      _directoriesColumn = new System.Windows.Forms.ColumnHeader();
      this._statusBar.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
      this._splitContainer.Panel1.SuspendLayout();
      this._splitContainer.Panel2.SuspendLayout();
      this._splitContainer.SuspendLayout();
      this._treeViewContextMenu.SuspendLayout();
      this._topPanel.SuspendLayout();
      this._openPanel.SuspendLayout();
      this._listViewContextMenu.SuspendLayout();
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
      // _statusBar
      // 
      this._statusBar.ImageScalingSize = new System.Drawing.Size(32, 32);
      this._statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._infoLabel});
      this._statusBar.Location = new System.Drawing.Point(0, 492);
      this._statusBar.Name = "_statusBar";
      this._statusBar.Size = new System.Drawing.Size(1142, 22);
      this._statusBar.TabIndex = 2;
      this._statusBar.Text = "_statusStrip";
      // 
      // _infoLabel
      // 
      this._infoLabel.Name = "_infoLabel";
      this._infoLabel.Size = new System.Drawing.Size(1127, 17);
      this._infoLabel.Spring = true;
      this._infoLabel.Text = "Ready";
      this._infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _splitContainer
      // 
      this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this._splitContainer.Location = new System.Drawing.Point(0, 32);
      this._splitContainer.Name = "_splitContainer";
      // 
      // _splitContainer.Panel1
      // 
      this._splitContainer.Panel1.Controls.Add(this._treeView);
      // 
      // _splitContainer.Panel2
      // 
      this._splitContainer.Panel2.Controls.Add(this._listView);
      this._splitContainer.Size = new System.Drawing.Size(1142, 460);
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
      this._treeView.Size = new System.Drawing.Size(378, 460);
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
      this._listView.Size = new System.Drawing.Size(760, 460);
      this._listView.TabIndex = 0;
      this._listView.UseCompatibleStateImageBehavior = false;
      this._listView.View = System.Windows.Forms.View.Details;
      this._listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._listView_ColumnClick);
      this._listView.DoubleClick += new System.EventHandler(this._listView_DoubleClick);
      // 
      // _bytesColumn
      // 
      this._bytesColumn.Text = "Bytes";
      this._bytesColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this._bytesColumn.Width = 150;
      // 
      // _topPanel
      // 
      this._topPanel.Controls.Add(this._statusLabel);
      this._topPanel.Controls.Add(this._cancelButton);
      this._topPanel.Controls.Add(this._openPanel);
      this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this._topPanel.Location = new System.Drawing.Point(0, 0);
      this._topPanel.Name = "_topPanel";
      this._topPanel.Size = new System.Drawing.Size(1142, 32);
      this._topPanel.TabIndex = 1;
      // 
      // _statusLabel
      // 
      this._statusLabel.AutoEllipsis = true;
      this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this._statusLabel.Location = new System.Drawing.Point(180, 0);
      this._statusLabel.Name = "_statusLabel";
      this._statusLabel.Size = new System.Drawing.Size(962, 32);
      this._statusLabel.TabIndex = 2;
      this._statusLabel.Text = "...";
      this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._statusLabel.UseMnemonic = false;
      // 
      // _cancelButton
      // 
      this._cancelButton.Dock = System.Windows.Forms.DockStyle.Left;
      this._cancelButton.Location = new System.Drawing.Point(105, 0);
      this._cancelButton.Name = "_cancelButton";
      this._cancelButton.Size = new System.Drawing.Size(75, 32);
      this._cancelButton.TabIndex = 1;
      this._cancelButton.Text = "Cancel";
      this._cancelButton.UseVisualStyleBackColor = true;
      this._cancelButton.Visible = false;
      this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
      // 
      // _openPanel
      // 
      this._openPanel.Controls.Add(this._openButton);
      this._openPanel.Dock = System.Windows.Forms.DockStyle.Left;
      this._openPanel.Location = new System.Drawing.Point(0, 0);
      this._openPanel.Name = "_openPanel";
      this._openPanel.Size = new System.Drawing.Size(105, 32);
      this._openPanel.TabIndex = 3;
      // 
      // _openButton
      // 
      this._openButton.Dock = System.Windows.Forms.DockStyle.Left;
      this._openButton.Location = new System.Drawing.Point(0, 0);
      this._openButton.Name = "_openButton";
      this._openButton.Size = new System.Drawing.Size(104, 32);
      this._openButton.TabIndex = 0;
      this._openButton.Text = "Open...";
      this._openButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._openButton.Click += new System.EventHandler(this._openButton_Click);
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
      // MOBZizeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1142, 514);
      this.Controls.Add(this._splitContainer);
      this.Controls.Add(this._topPanel);
      this.Controls.Add(this._statusBar);
      this.Name = "MOBZizeForm";
      this.Text = "MOBZize";
      this.Load += new System.EventHandler(this.MobZecForm_Load);
      this._statusBar.ResumeLayout(false);
      this._statusBar.PerformLayout();
      this._splitContainer.Panel1.ResumeLayout(false);
      this._splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
      this._splitContainer.ResumeLayout(false);
      this._treeViewContextMenu.ResumeLayout(false);
      this._topPanel.ResumeLayout(false);
      this._openPanel.ResumeLayout(false);
      this._listViewContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private SplitContainer _splitContainer;
    private TreeView _treeView;
    private ListView _listView;
    private Panel _topPanel;
    private Button _openButton;
    private Label _statusLabel;
    private Button _cancelButton;
    private Panel _openPanel;
    private ContextMenuStrip _treeViewContextMenu;
    private ToolStripMenuItem _showInExplorerMenuItem;
    private ToolStripStatusLabel _infoLabel;
    private ColumnHeader _bytesColumn;
    private ToolStripMenuItem _openInExplorerMenuItem;
    private ContextMenuStrip _listViewContextMenu;
    private ToolStripMenuItem showItemInExplorerMenuItem;
    private ToolStripMenuItem openItemInExplorerMenuItem;
        private StatusStrip _statusBar;
    }
}