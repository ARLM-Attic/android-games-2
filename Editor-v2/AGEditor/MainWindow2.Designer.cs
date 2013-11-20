namespace AGEditor
{
    partial class MainWindow2
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ctlBtnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._ctlTxtProjName = new System.Windows.Forms.ToolStripStatusLabel();
            this.模型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ctlBtnSwitchModel = new System.Windows.Forms.ToolStripMenuItem();
            this.音效ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this._ctlBtnModelList = new System.Windows.Forms.LinkLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.模型ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(292, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._ctlTxtProjName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 251);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(292, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建项目ToolStripMenuItem,
            this.切换项目ToolStripMenuItem,
            this.toolStripSeparator1,
            this.保存ToolStripMenuItem,
            this.toolStripSeparator2,
            this._ctlBtnExit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建项目ToolStripMenuItem
            // 
            this.新建项目ToolStripMenuItem.Name = "新建项目ToolStripMenuItem";
            this.新建项目ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建项目ToolStripMenuItem.Text = "新建项目";
            // 
            // 切换项目ToolStripMenuItem
            // 
            this.切换项目ToolStripMenuItem.Name = "切换项目ToolStripMenuItem";
            this.切换项目ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.切换项目ToolStripMenuItem.Text = "切换项目";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // _ctlBtnExit
            // 
            this._ctlBtnExit.Name = "_ctlBtnExit";
            this._ctlBtnExit.Size = new System.Drawing.Size(152, 22);
            this._ctlBtnExit.Text = "退出";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "项目:";
            // 
            // _ctlTxtProjName
            // 
            this._ctlTxtProjName.Name = "_ctlTxtProjName";
            this._ctlTxtProjName.Size = new System.Drawing.Size(29, 17);
            this._ctlTxtProjName.Text = "None";
            // 
            // 模型ToolStripMenuItem
            // 
            this.模型ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ctlBtnSwitchModel,
            this.音效ToolStripMenuItem,
            this.单位ToolStripMenuItem,
            this.地图ToolStripMenuItem});
            this.模型ToolStripMenuItem.Name = "模型ToolStripMenuItem";
            this.模型ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.模型ToolStripMenuItem.Text = "窗口";
            // 
            // _ctlBtnSwitchModel
            // 
            this._ctlBtnSwitchModel.Name = "_ctlBtnSwitchModel";
            this._ctlBtnSwitchModel.Size = new System.Drawing.Size(152, 22);
            this._ctlBtnSwitchModel.Text = "模型";
            this._ctlBtnSwitchModel.Click += new System.EventHandler(this._ctlBtnSwitchModel_Click);
            // 
            // 音效ToolStripMenuItem
            // 
            this.音效ToolStripMenuItem.Name = "音效ToolStripMenuItem";
            this.音效ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.音效ToolStripMenuItem.Text = "音效";
            // 
            // 单位ToolStripMenuItem
            // 
            this.单位ToolStripMenuItem.Name = "单位ToolStripMenuItem";
            this.单位ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.单位ToolStripMenuItem.Text = "单位";
            // 
            // 地图ToolStripMenuItem
            // 
            this.地图ToolStripMenuItem.Name = "地图ToolStripMenuItem";
            this.地图ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.地图ToolStripMenuItem.Text = "地图";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "模型";
            // 
            // _ctlBtnModelList
            // 
            this._ctlBtnModelList.AutoSize = true;
            this._ctlBtnModelList.Location = new System.Drawing.Point(48, 53);
            this._ctlBtnModelList.Name = "_ctlBtnModelList";
            this._ctlBtnModelList.Size = new System.Drawing.Size(11, 12);
            this._ctlBtnModelList.TabIndex = 3;
            this._ctlBtnModelList.TabStop = true;
            this._ctlBtnModelList.Text = "0";
            this._ctlBtnModelList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._ctlBtnModelList_LinkClicked);
            // 
            // MainWindow2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this._ctlBtnModelList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _ctlTxtProjName;
        private System.Windows.Forms.ToolStripMenuItem 模型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ctlBtnSwitchModel;
        private System.Windows.Forms.ToolStripMenuItem 音效ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _ctlBtnModelList;
    }
}