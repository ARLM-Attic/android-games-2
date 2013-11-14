namespace AG.Editor.Windows
{
    partial class AGESelectModelCategoryWindow
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
            this.ctlList = new System.Windows.Forms.ListBox();
            this.ctlBtnOK = new System.Windows.Forms.Button();
            this.ctlBtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctlList
            // 
            this.ctlList.FormattingEnabled = true;
            this.ctlList.ItemHeight = 12;
            this.ctlList.Location = new System.Drawing.Point(13, 13);
            this.ctlList.Name = "ctlList";
            this.ctlList.Size = new System.Drawing.Size(390, 208);
            this.ctlList.TabIndex = 0;
            // 
            // ctlBtnOK
            // 
            this.ctlBtnOK.Location = new System.Drawing.Point(125, 228);
            this.ctlBtnOK.Name = "ctlBtnOK";
            this.ctlBtnOK.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnOK.TabIndex = 1;
            this.ctlBtnOK.Text = "确定";
            this.ctlBtnOK.UseVisualStyleBackColor = true;
            this.ctlBtnOK.Click += new System.EventHandler(this.ctlBtnOK_Click);
            // 
            // ctlBtnCancel
            // 
            this.ctlBtnCancel.Location = new System.Drawing.Point(206, 228);
            this.ctlBtnCancel.Name = "ctlBtnCancel";
            this.ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnCancel.TabIndex = 2;
            this.ctlBtnCancel.Text = "取消";
            this.ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // AGESelectModelCategoryWindow
            // 
            this.AcceptButton = this.ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(415, 279);
            this.Controls.Add(this.ctlBtnCancel);
            this.Controls.Add(this.ctlBtnOK);
            this.Controls.Add(this.ctlList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AGESelectModelCategoryWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGESelectModelCategoryWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ctlList;
        private System.Windows.Forms.Button ctlBtnOK;
        private System.Windows.Forms.Button ctlBtnCancel;
    }
}