namespace AG.Editor.Windows
{
    partial class AGELaunchWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.ctlLinkLatest = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.ctlListHistoryProject = new System.Windows.Forms.ListBox();
            this.ctlBtnOK = new System.Windows.Forms.Button();
            this.ctlBtnCancel = new System.Windows.Forms.Button();
            this.ctlBtnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "上次打开:";
            // 
            // ctlLinkLatest
            // 
            this.ctlLinkLatest.AutoSize = true;
            this.ctlLinkLatest.Location = new System.Drawing.Point(78, 13);
            this.ctlLinkLatest.Name = "ctlLinkLatest";
            this.ctlLinkLatest.Size = new System.Drawing.Size(29, 12);
            this.ctlLinkLatest.TabIndex = 1;
            this.ctlLinkLatest.TabStop = true;
            this.ctlLinkLatest.Text = "none";
            this.ctlLinkLatest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ctlLinkLatest_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "历史:";
            // 
            // ctlListHistoryProject
            // 
            this.ctlListHistoryProject.FormattingEnabled = true;
            this.ctlListHistoryProject.ItemHeight = 12;
            this.ctlListHistoryProject.Location = new System.Drawing.Point(15, 59);
            this.ctlListHistoryProject.Name = "ctlListHistoryProject";
            this.ctlListHistoryProject.Size = new System.Drawing.Size(571, 136);
            this.ctlListHistoryProject.TabIndex = 3;
            // 
            // ctlBtnOK
            // 
            this.ctlBtnOK.Location = new System.Drawing.Point(227, 202);
            this.ctlBtnOK.Name = "ctlBtnOK";
            this.ctlBtnOK.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnOK.TabIndex = 4;
            this.ctlBtnOK.Text = "确定";
            this.ctlBtnOK.UseVisualStyleBackColor = true;
            this.ctlBtnOK.Click += new System.EventHandler(this.ctlBtnOK_Click);
            // 
            // ctlBtnCancel
            // 
            this.ctlBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlBtnCancel.Location = new System.Drawing.Point(308, 202);
            this.ctlBtnCancel.Name = "ctlBtnCancel";
            this.ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnCancel.TabIndex = 5;
            this.ctlBtnCancel.Text = "取消";
            this.ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // ctlBtnCreate
            // 
            this.ctlBtnCreate.Location = new System.Drawing.Point(511, 13);
            this.ctlBtnCreate.Name = "ctlBtnCreate";
            this.ctlBtnCreate.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnCreate.TabIndex = 6;
            this.ctlBtnCreate.Text = "创建";
            this.ctlBtnCreate.UseVisualStyleBackColor = true;
            this.ctlBtnCreate.Click += new System.EventHandler(this.ctlBtnCreate_Click);
            // 
            // AGELaunchWindow
            // 
            this.AcceptButton = this.ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(598, 243);
            this.Controls.Add(this.ctlBtnCreate);
            this.Controls.Add(this.ctlBtnCancel);
            this.Controls.Add(this.ctlBtnOK);
            this.Controls.Add(this.ctlListHistoryProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctlLinkLatest);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "AGELaunchWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGELaunchWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel ctlLinkLatest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ctlListHistoryProject;
        private System.Windows.Forms.Button ctlBtnOK;
        private System.Windows.Forms.Button ctlBtnCancel;
        private System.Windows.Forms.Button ctlBtnCreate;
    }
}