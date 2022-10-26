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
      System.Windows.Forms.StatusStrip statusStrip1;
      this._infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this._splitContainer = new System.Windows.Forms.SplitContainer();
      this._treeView = new System.Windows.Forms.TreeView();
      this._treeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._showInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._listView = new System.Windows.Forms.ListView();
      this._nameColumn = new System.Windows.Forms.ColumnHeader();
      this._rightsColumn = new System.Windows.Forms.ColumnHeader();
      this._perentageColumn = new System.Windows.Forms.ColumnHeader();
      this._filesColumn = new System.Windows.Forms.ColumnHeader();
      this._directoriesColumn = new System.Windows.Forms.ColumnHeader();
      this._topPanel = new System.Windows.Forms.Panel();
      this._statusLabel = new System.Windows.Forms.Label();
      this._cancelButton = new System.Windows.Forms.Button();
      this._openPanel = new System.Windows.Forms.Panel();
      this._openButton = new System.Windows.Forms.Button();
      statusStrip1 = new System.Windows.Forms.StatusStrip();
      statusStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
      this._splitContainer.Panel1.SuspendLayout();
      this._splitContainer.Panel2.SuspendLayout();
      this._splitContainer.SuspendLayout();
      this._treeViewContextMenu.SuspendLayout();
      this._topPanel.SuspendLayout();
      this._openPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._infoLabel});
      statusStrip1.Location = new System.Drawing.Point(0, 428);
      statusStrip1.Name = "statusStrip1";
      statusStrip1.Size = new System.Drawing.Size(800, 22);
      statusStrip1.TabIndex = 2;
      statusStrip1.Text = "_statusStrip";
      // 
      // _infoLabel
      // 
      this._infoLabel.Name = "_infoLabel";
      this._infoLabel.Size = new System.Drawing.Size(785, 17);
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
      this._splitContainer.Size = new System.Drawing.Size(800, 396);
      this._splitContainer.SplitterDistance = 266;
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
      this._treeView.Size = new System.Drawing.Size(266, 396);
      this._treeView.TabIndex = 0;
      this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeView_AfterSelect);
      this._treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._treeView_NodeMouseClick);
      // 
      // _treeViewContextMenu
      // 
      this._treeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._showInExplorerMenuItem});
      this._treeViewContextMenu.Name = "_treeViewContextMenu";
      this._treeViewContextMenu.Size = new System.Drawing.Size(163, 26);
      // 
      // _showInExplorerMenuItem
      // 
      this._showInExplorerMenuItem.Name = "_showInExplorerMenuItem";
      this._showInExplorerMenuItem.Size = new System.Drawing.Size(162, 22);
      this._showInExplorerMenuItem.Text = "Show in &Explorer";
      this._showInExplorerMenuItem.Click += new System.EventHandler(this._showInExplorerMenuItem_Click);
      // 
      // _listView
      // 
      this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._nameColumn,
            this._rightsColumn,
            this._perentageColumn,
            this._filesColumn,
            this._directoriesColumn});
      this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
      this._listView.FullRowSelect = true;
      this._listView.HideSelection = true;
      this._listView.Location = new System.Drawing.Point(0, 0);
      this._listView.Name = "_listView";
      this._listView.Size = new System.Drawing.Size(530, 396);
      this._listView.TabIndex = 0;
      this._listView.UseCompatibleStateImageBehavior = false;
      this._listView.View = System.Windows.Forms.View.Details;
      this._listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._listView_ColumnClick);
      // 
      // _nameColumn
      // 
      this._nameColumn.Text = "Name";
      this._nameColumn.Width = 250;
      // 
      // _rightsColumn
      // 
      this._rightsColumn.Text = "Size";
      this._rightsColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this._rightsColumn.Width = 100;
      // 
      // _perentageColumn
      // 
      this._perentageColumn.Text = "%";
      this._perentageColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this._perentageColumn.Width = 50;
      // 
      // _filesColumn
      // 
      this._filesColumn.Text = "Files";
      this._filesColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this._filesColumn.Width = 100;
      // 
      // _directoriesColumn
      // 
      this._directoriesColumn.Text = "Folders";
      this._directoriesColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this._directoriesColumn.Width = 100;
      // 
      // _topPanel
      // 
      this._topPanel.Controls.Add(this._statusLabel);
      this._topPanel.Controls.Add(this._cancelButton);
      this._topPanel.Controls.Add(this._openPanel);
      this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this._topPanel.Location = new System.Drawing.Point(0, 0);
      this._topPanel.Name = "_topPanel";
      this._topPanel.Size = new System.Drawing.Size(800, 32);
      this._topPanel.TabIndex = 1;
      // 
      // _statusLabel
      // 
      this._statusLabel.AutoEllipsis = true;
      this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this._statusLabel.Location = new System.Drawing.Point(180, 0);
      this._statusLabel.Name = "_statusLabel";
      this._statusLabel.Size = new System.Drawing.Size(620, 32);
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
      // MOBZizeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this._splitContainer);
      this.Controls.Add(this._topPanel);
      this.Controls.Add(statusStrip1);
      this.Name = "MOBZizeForm";
      this.Text = "MOBZize";
      this.Load += new System.EventHandler(this.MobZecForm_Load);
      statusStrip1.ResumeLayout(false);
      statusStrip1.PerformLayout();
      this._splitContainer.Panel1.ResumeLayout(false);
      this._splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
      this._splitContainer.ResumeLayout(false);
      this._treeViewContextMenu.ResumeLayout(false);
      this._topPanel.ResumeLayout(false);
      this._openPanel.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private SplitContainer _splitContainer;
    private TreeView _treeView;
    private ListView _listView;
    private ColumnHeader _nameColumn;
    private Panel _topPanel;
    private Button _openButton;
    private ColumnHeader _rightsColumn;
    private Label _statusLabel;
    private Button _cancelButton;
    private Panel _openPanel;
    private ContextMenuStrip _treeViewContextMenu;
    private ToolStripMenuItem _showInExplorerMenuItem;
    private ToolStripStatusLabel _infoLabel;
    private ColumnHeader _perentageColumn;
    private ColumnHeader _filesColumn;
    private ColumnHeader _directoriesColumn;
  }
}