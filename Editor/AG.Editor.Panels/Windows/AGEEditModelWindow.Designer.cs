﻿
namespace AG.Editor.ModelUI.Windows
{
    partial class AGEEditModelWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AGEEditModelWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ctlBtnSave = new System.Windows.Forms.ToolStripButton();
            this.ctlBtnAddFrame = new System.Windows.Forms.ToolStripButton();
            this.ctlBtnRemoveFrame = new System.Windows.Forms.ToolStripButton();
            this.ctlBtnCopyRef = new System.Windows.Forms.ToolStripButton();
            this.ctlBtnAddAudio = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctlTreeModel = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlBtnRemoveAudio = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctlBtnSave,
            this.ctlBtnAddFrame,
            this.ctlBtnRemoveFrame,
            this.ctlBtnCopyRef,
            this.ctlBtnAddAudio,
            this.ctlBtnRemoveAudio});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(555, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ctlBtnSave
            // 
            this.ctlBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("ctlBtnSave.Image")));
            this.ctlBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlBtnSave.Name = "ctlBtnSave";
            this.ctlBtnSave.Size = new System.Drawing.Size(49, 22);
            this.ctlBtnSave.Text = "保存";
            this.ctlBtnSave.Click += new System.EventHandler(this.ctlBtnSave_Click);
            // 
            // ctlBtnAddFrame
            // 
            this.ctlBtnAddFrame.Image = ((System.Drawing.Image)(resources.GetObject("ctlBtnAddFrame.Image")));
            this.ctlBtnAddFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlBtnAddFrame.Name = "ctlBtnAddFrame";
            this.ctlBtnAddFrame.Size = new System.Drawing.Size(61, 22);
            this.ctlBtnAddFrame.Text = "添加帧";
            this.ctlBtnAddFrame.Click += new System.EventHandler(this.ctlBtnAddFrame_Click);
            // 
            // ctlBtnRemoveFrame
            // 
            this.ctlBtnRemoveFrame.Image = ((System.Drawing.Image)(resources.GetObject("ctlBtnRemoveFrame.Image")));
            this.ctlBtnRemoveFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlBtnRemoveFrame.Name = "ctlBtnRemoveFrame";
            this.ctlBtnRemoveFrame.Size = new System.Drawing.Size(61, 22);
            this.ctlBtnRemoveFrame.Text = "删除帧";
            this.ctlBtnRemoveFrame.Click += new System.EventHandler(this.ctlBtnRemoveFrame_Click);
            // 
            // ctlBtnCopyRef
            // 
            this.ctlBtnCopyRef.Image = ((System.Drawing.Image)(resources.GetObject("ctlBtnCopyRef.Image")));
            this.ctlBtnCopyRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlBtnCopyRef.Name = "ctlBtnCopyRef";
            this.ctlBtnCopyRef.Size = new System.Drawing.Size(73, 22);
            this.ctlBtnCopyRef.Text = "拷贝引用";
            this.ctlBtnCopyRef.Click += new System.EventHandler(this.OnBtnCopyRefClick);
            // 
            // ctlBtnAddAudio
            // 
            this.ctlBtnAddAudio.Image = ((System.Drawing.Image)(resources.GetObject("ctlBtnAddAudio.Image")));
            this.ctlBtnAddAudio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlBtnAddAudio.Name = "ctlBtnAddAudio";
            this.ctlBtnAddAudio.Size = new System.Drawing.Size(73, 22);
            this.ctlBtnAddAudio.Text = "设置音效";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 354);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(555, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ctlTreeModel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(555, 329);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 2;
            // 
            // ctlTreeModel
            // 
            this.ctlTreeModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlTreeModel.Location = new System.Drawing.Point(0, 0);
            this.ctlTreeModel.Name = "ctlTreeModel";
            this.ctlTreeModel.Size = new System.Drawing.Size(185, 329);
            this.ctlTreeModel.TabIndex = 0;
            this.ctlTreeModel.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ctlTreeModel_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 329);
            this.panel1.TabIndex = 0;
            // 
            // ctlBtnRemoveAudio
            // 
            this.ctlBtnRemoveAudio.Image = ((System.Drawing.Image)(resources.GetObject("ctlBtnRemoveAudio.Image")));
            this.ctlBtnRemoveAudio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlBtnRemoveAudio.Name = "ctlBtnRemoveAudio";
            this.ctlBtnRemoveAudio.Size = new System.Drawing.Size(73, 22);
            this.ctlBtnRemoveAudio.Text = "删除音效";
            // 
            // AGEEditModelWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 376);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AGEEditModelWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGEEditModelWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView ctlTreeModel;
        private System.Windows.Forms.ToolStripButton ctlBtnAddFrame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton ctlBtnSave;
        private System.Windows.Forms.ToolStripButton ctlBtnCopyRef;
        private System.Windows.Forms.ToolStripButton ctlBtnRemoveFrame;
        private System.Windows.Forms.ToolStripButton ctlBtnAddAudio;
        private System.Windows.Forms.ToolStripButton ctlBtnRemoveAudio;
    }
}