namespace AGEditor.Windows.Workspace
{
    partial class ConfirmWorkspaceWindow
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
            this._ctlBtnEnterWS = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this._ctlBtnOK = new System.Windows.Forms.Button();
            this._ctlBtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前工作空间:";
            // 
            // _ctlBtnEnterWS
            // 
            this._ctlBtnEnterWS.AutoSize = true;
            this._ctlBtnEnterWS.Location = new System.Drawing.Point(102, 13);
            this._ctlBtnEnterWS.Name = "_ctlBtnEnterWS";
            this._ctlBtnEnterWS.Size = new System.Drawing.Size(53, 12);
            this._ctlBtnEnterWS.TabIndex = 1;
            this._ctlBtnEnterWS.TabStop = true;
            this._ctlBtnEnterWS.Text = "点击确认";
            this._ctlBtnEnterWS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._ctlBtnEnterWS_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "历史:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(518, 244);
            this.listBox1.TabIndex = 3;
            // 
            // _ctlBtnOK
            // 
            this._ctlBtnOK.Location = new System.Drawing.Point(204, 316);
            this._ctlBtnOK.Name = "_ctlBtnOK";
            this._ctlBtnOK.Size = new System.Drawing.Size(75, 23);
            this._ctlBtnOK.TabIndex = 4;
            this._ctlBtnOK.Text = "确定";
            this._ctlBtnOK.UseVisualStyleBackColor = true;
            this._ctlBtnOK.Click += new System.EventHandler(this._ctlBtnOK_Click);
            // 
            // _ctlBtnCancel
            // 
            this._ctlBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._ctlBtnCancel.Location = new System.Drawing.Point(285, 316);
            this._ctlBtnCancel.Name = "_ctlBtnCancel";
            this._ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this._ctlBtnCancel.TabIndex = 5;
            this._ctlBtnCancel.Text = "取消";
            this._ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // ConfirmWorkspaceWindow
            // 
            this.AcceptButton = this._ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(542, 351);
            this.Controls.Add(this._ctlBtnCancel);
            this.Controls.Add(this._ctlBtnOK);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._ctlBtnEnterWS);
            this.Controls.Add(this.label1);
            this.Name = "ConfirmWorkspaceWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfirmWorkspaceWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _ctlBtnEnterWS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button _ctlBtnOK;
        private System.Windows.Forms.Button _ctlBtnCancel;
    }
}