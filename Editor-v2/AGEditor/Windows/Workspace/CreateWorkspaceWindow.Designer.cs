namespace AGEditor.Windows.Workspace
{
    partial class CreateWorkspaceWindow
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
            this._ctlBtnBrowse = new System.Windows.Forms.LinkLabel();
            this._ctlBtnOK = new System.Windows.Forms.Button();
            this._ctlBtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工作空间:";
            // 
            // _ctlBtnBrowse
            // 
            this._ctlBtnBrowse.AutoSize = true;
            this._ctlBtnBrowse.Location = new System.Drawing.Point(230, 60);
            this._ctlBtnBrowse.Name = "_ctlBtnBrowse";
            this._ctlBtnBrowse.Size = new System.Drawing.Size(53, 12);
            this._ctlBtnBrowse.TabIndex = 1;
            this._ctlBtnBrowse.TabStop = true;
            this._ctlBtnBrowse.Text = "点击选择";
            this._ctlBtnBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._ctlBtnBrowse_LinkClicked);
            // 
            // _ctlBtnOK
            // 
            this._ctlBtnOK.Location = new System.Drawing.Point(166, 98);
            this._ctlBtnOK.Name = "_ctlBtnOK";
            this._ctlBtnOK.Size = new System.Drawing.Size(75, 23);
            this._ctlBtnOK.TabIndex = 2;
            this._ctlBtnOK.Text = "确定";
            this._ctlBtnOK.UseVisualStyleBackColor = true;
            this._ctlBtnOK.Click += new System.EventHandler(this._ctlBtnOK_Click);
            // 
            // _ctlBtnCancel
            // 
            this._ctlBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._ctlBtnCancel.Location = new System.Drawing.Point(272, 98);
            this._ctlBtnCancel.Name = "_ctlBtnCancel";
            this._ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this._ctlBtnCancel.TabIndex = 3;
            this._ctlBtnCancel.Text = "取消";
            this._ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // CreateWorkspaceWindow
            // 
            this.AcceptButton = this._ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(540, 140);
            this.Controls.Add(this._ctlBtnCancel);
            this.Controls.Add(this._ctlBtnOK);
            this.Controls.Add(this._ctlBtnBrowse);
            this.Controls.Add(this.label1);
            this.Name = "CreateWorkspaceWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建工作空间";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _ctlBtnBrowse;
        private System.Windows.Forms.Button _ctlBtnOK;
        private System.Windows.Forms.Button _ctlBtnCancel;
    }
}