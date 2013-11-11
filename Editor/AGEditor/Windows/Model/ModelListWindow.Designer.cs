namespace AGEditor.Windows.Model
{
    partial class ModelListWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelListWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._ctlBtnQuit = new System.Windows.Forms.ToolStripButton();
            this._ctlBtnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 318);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(580, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(580, 293);
            this.treeView1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this._ctlBtnQuit,
            this._ctlBtnEdit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(580, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton1.Text = "创建";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // _ctlBtnQuit
            // 
            this._ctlBtnQuit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._ctlBtnQuit.Image = ((System.Drawing.Image)(resources.GetObject("_ctlBtnQuit.Image")));
            this._ctlBtnQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ctlBtnQuit.Name = "_ctlBtnQuit";
            this._ctlBtnQuit.Size = new System.Drawing.Size(49, 22);
            this._ctlBtnQuit.Text = "退出";
            this._ctlBtnQuit.Click += new System.EventHandler(this._ctlBtnQuit_Click);
            // 
            // _ctlBtnEdit
            // 
            this._ctlBtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("_ctlBtnEdit.Image")));
            this._ctlBtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ctlBtnEdit.Name = "_ctlBtnEdit";
            this._ctlBtnEdit.Size = new System.Drawing.Size(49, 22);
            this._ctlBtnEdit.Text = "修改";
            this._ctlBtnEdit.Click += new System.EventHandler(this._ctlBtnEdit_Click);
            // 
            // ModelListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 340);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ModelListWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModelListWindow";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton _ctlBtnQuit;
        private System.Windows.Forms.ToolStripButton _ctlBtnEdit;
    }
}