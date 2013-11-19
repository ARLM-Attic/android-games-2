namespace AG.Editor.AudioUI.Windows
{
    partial class AGEEditAudioWindow
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
            this.label2 = new System.Windows.Forms.Label();
            this.ctlLinkFile = new System.Windows.Forms.LinkLabel();
            this.ctlBtnOK = new System.Windows.Forms.Button();
            this.ctlBtnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ctlListCategory = new System.Windows.Forms.ComboBox();
            this.ctlEditId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "文件:";
            // 
            // ctlLinkFile
            // 
            this.ctlLinkFile.AutoSize = true;
            this.ctlLinkFile.Location = new System.Drawing.Point(56, 72);
            this.ctlLinkFile.Name = "ctlLinkFile";
            this.ctlLinkFile.Size = new System.Drawing.Size(29, 12);
            this.ctlLinkFile.TabIndex = 2;
            this.ctlLinkFile.TabStop = true;
            this.ctlLinkFile.Text = "none";
            this.ctlLinkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ctlLinkFile_LinkClicked);
            // 
            // ctlBtnOK
            // 
            this.ctlBtnOK.Location = new System.Drawing.Point(150, 103);
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
            this.ctlBtnCancel.Location = new System.Drawing.Point(231, 103);
            this.ctlBtnCancel.Name = "ctlBtnCancel";
            this.ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnCancel.TabIndex = 5;
            this.ctlBtnCancel.Text = "取消";
            this.ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "类型:";
            // 
            // ctlListCategory
            // 
            this.ctlListCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlListCategory.FormattingEnabled = true;
            this.ctlListCategory.Location = new System.Drawing.Point(56, 12);
            this.ctlListCategory.Name = "ctlListCategory";
            this.ctlListCategory.Size = new System.Drawing.Size(201, 20);
            this.ctlListCategory.TabIndex = 7;
            // 
            // ctlEditId
            // 
            this.ctlEditId.Location = new System.Drawing.Point(56, 38);
            this.ctlEditId.Name = "ctlEditId";
            this.ctlEditId.Size = new System.Drawing.Size(388, 21);
            this.ctlEditId.TabIndex = 8;
            // 
            // AGEEditAudioWindow
            // 
            this.AcceptButton = this.ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(456, 140);
            this.Controls.Add(this.ctlEditId);
            this.Controls.Add(this.ctlListCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctlBtnCancel);
            this.Controls.Add(this.ctlBtnOK);
            this.Controls.Add(this.ctlLinkFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AGEEditAudioWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGEEditAudioWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel ctlLinkFile;
        private System.Windows.Forms.Button ctlBtnOK;
        private System.Windows.Forms.Button ctlBtnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ctlListCategory;
        private System.Windows.Forms.TextBox ctlEditId;
    }
}