namespace AGEditer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ctlBtnOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._ctlBtnPublish = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnSetCamp = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ctlBtnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.模型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ctlBtnLaunchModelWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.单位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ctlBtnLaunchUnitWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._ctlTreeUnit = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._ctlTreeTerrain = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._treeCamp = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this._listCurrentCamp = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._ctlBtnAutoModeling = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 306);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.模型ToolStripMenuItem,
            this.单位ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(661, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this._ctlBtnOpen,
            this.toolStripSeparator1,
            this._ctlBtnPublish});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // _ctlBtnOpen
            // 
            this._ctlBtnOpen.Name = "_ctlBtnOpen";
            this._ctlBtnOpen.Size = new System.Drawing.Size(148, 22);
            this._ctlBtnOpen.Text = "打开地图";
            this._ctlBtnOpen.Click += new System.EventHandler(this._ctlBtnOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // _ctlBtnPublish
            // 
            this._ctlBtnPublish.Name = "_ctlBtnPublish";
            this._ctlBtnPublish.Size = new System.Drawing.Size(148, 22);
            this._ctlBtnPublish.Text = "发布到资源库";
            this._ctlBtnPublish.Click += new System.EventHandler(this._ctlBtnPublish_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnSetCamp});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // _btnSetCamp
            // 
            this._btnSetCamp.Name = "_btnSetCamp";
            this._btnSetCamp.Size = new System.Drawing.Size(124, 22);
            this._btnSetCamp.Text = "阵营设置";
            this._btnSetCamp.Click += new System.EventHandler(this._btnSetCamp_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ctlBtnDelete});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // _ctlBtnDelete
            // 
            this._ctlBtnDelete.Name = "_ctlBtnDelete";
            this._ctlBtnDelete.Size = new System.Drawing.Size(100, 22);
            this._ctlBtnDelete.Text = "删除";
            this._ctlBtnDelete.Click += new System.EventHandler(this._ctlBtnDelete_Click);
            // 
            // 模型ToolStripMenuItem
            // 
            this.模型ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ctlBtnLaunchModelWindow,
            this._ctlBtnAutoModeling});
            this.模型ToolStripMenuItem.Name = "模型ToolStripMenuItem";
            this.模型ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.模型ToolStripMenuItem.Text = "模型";
            // 
            // _ctlBtnLaunchModelWindow
            // 
            this._ctlBtnLaunchModelWindow.Name = "_ctlBtnLaunchModelWindow";
            this._ctlBtnLaunchModelWindow.Size = new System.Drawing.Size(152, 22);
            this._ctlBtnLaunchModelWindow.Text = "编辑";
            this._ctlBtnLaunchModelWindow.Click += new System.EventHandler(this._ctlBtnLaunchModelWindow_Click);
            // 
            // 单位ToolStripMenuItem
            // 
            this.单位ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ctlBtnLaunchUnitWindow});
            this.单位ToolStripMenuItem.Name = "单位ToolStripMenuItem";
            this.单位ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.单位ToolStripMenuItem.Text = "单位";
            // 
            // _ctlBtnLaunchUnitWindow
            // 
            this._ctlBtnLaunchUnitWindow.Name = "_ctlBtnLaunchUnitWindow";
            this._ctlBtnLaunchUnitWindow.Size = new System.Drawing.Size(100, 22);
            this._ctlBtnLaunchUnitWindow.Text = "编辑";
            this._ctlBtnLaunchUnitWindow.Click += new System.EventHandler(this._ctlBtnLaunchUnitWindow_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(661, 306);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(220, 306);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._ctlTreeUnit);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(212, 280);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "单位列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _ctlTreeUnit
            // 
            this._ctlTreeUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ctlTreeUnit.Location = new System.Drawing.Point(3, 3);
            this._ctlTreeUnit.Name = "_ctlTreeUnit";
            this._ctlTreeUnit.Size = new System.Drawing.Size(206, 274);
            this._ctlTreeUnit.TabIndex = 0;
            this._ctlTreeUnit.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._ctlTreeUnit_AfterSelect);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._ctlTreeTerrain);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(212, 280);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "地形";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _ctlTreeTerrain
            // 
            this._ctlTreeTerrain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ctlTreeTerrain.Location = new System.Drawing.Point(3, 3);
            this._ctlTreeTerrain.Name = "_ctlTreeTerrain";
            this._ctlTreeTerrain.Size = new System.Drawing.Size(206, 274);
            this._ctlTreeTerrain.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._treeCamp);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(212, 280);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "阵营";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _treeCamp
            // 
            this._treeCamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeCamp.Location = new System.Drawing.Point(3, 3);
            this._treeCamp.Name = "_treeCamp";
            this._treeCamp.Size = new System.Drawing.Size(206, 274);
            this._treeCamp.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripLabel3,
            this._listCurrentCamp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(661, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(112, 22);
            this.toolStripButton1.Text = "释放已选中单位";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(104, 22);
            this.toolStripLabel1.Text = "鼠标点击放置对象";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel2.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel3.Text = "当前阵营:";
            // 
            // _listCurrentCamp
            // 
            this._listCurrentCamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._listCurrentCamp.Name = "_listCurrentCamp";
            this._listCurrentCamp.Size = new System.Drawing.Size(121, 25);
            this._listCurrentCamp.SelectedIndexChanged += new System.EventHandler(this._listCurrentCamp_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 356);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(661, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _ctlBtnAutoModeling
            // 
            this._ctlBtnAutoModeling.Name = "_ctlBtnAutoModeling";
            this._ctlBtnAutoModeling.Size = new System.Drawing.Size(152, 22);
            this._ctlBtnAutoModeling.Text = "自动打包";
            this._ctlBtnAutoModeling.Click += new System.EventHandler(this._ctlBtnAutoModeling_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 378);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图编辑器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView _ctlTreeUnit;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnPublish;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnOpen;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _btnSetCamp;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView _treeCamp;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox _listCurrentCamp;
        private System.Windows.Forms.TreeView _ctlTreeTerrain;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnDelete;
        private System.Windows.Forms.ToolStripMenuItem 模型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnLaunchModelWindow;
        private System.Windows.Forms.ToolStripMenuItem 单位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnLaunchUnitWindow;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnAutoModeling;
    }
}

