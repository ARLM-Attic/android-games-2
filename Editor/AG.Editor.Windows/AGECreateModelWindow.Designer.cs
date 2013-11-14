namespace AG.Editor.Windows
{
    partial class AGECreateModelWindow
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
            this.ctlListCategory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ctlBtnCancel = new System.Windows.Forms.Button();
            this.ctlBtnOK = new System.Windows.Forms.Button();
            this.ctlEditCaption = new System.Windows.Forms.TextBox();
            this.ctlEditId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctlListCategory
            // 
            this.ctlListCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlListCategory.FormattingEnabled = true;
            this.ctlListCategory.Location = new System.Drawing.Point(76, 67);
            this.ctlListCategory.Name = "ctlListCategory";
            this.ctlListCategory.Size = new System.Drawing.Size(121, 20);
            this.ctlListCategory.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "类别:";
            // 
            // ctlBtnCancel
            // 
            this.ctlBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlBtnCancel.Location = new System.Drawing.Point(375, 104);
            this.ctlBtnCancel.Name = "ctlBtnCancel";
            this.ctlBtnCancel.Size = new System.Drawing.Size(75, 21);
            this.ctlBtnCancel.TabIndex = 18;
            this.ctlBtnCancel.Text = "取消";
            this.ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // ctlBtnOK
            // 
            this.ctlBtnOK.Location = new System.Drawing.Point(294, 104);
            this.ctlBtnOK.Name = "ctlBtnOK";
            this.ctlBtnOK.Size = new System.Drawing.Size(75, 21);
            this.ctlBtnOK.TabIndex = 17;
            this.ctlBtnOK.Text = "创建";
            this.ctlBtnOK.UseVisualStyleBackColor = true;
            this.ctlBtnOK.Click += new System.EventHandler(this.ctlBtnOK_Click);
            // 
            // ctlEditCaption
            // 
            this.ctlEditCaption.Location = new System.Drawing.Point(76, 43);
            this.ctlEditCaption.Name = "ctlEditCaption";
            this.ctlEditCaption.Size = new System.Drawing.Size(374, 21);
            this.ctlEditCaption.TabIndex = 16;
            // 
            // ctlEditId
            // 
            this.ctlEditId.Location = new System.Drawing.Point(76, 19);
            this.ctlEditId.Name = "ctlEditId";
            this.ctlEditId.Size = new System.Drawing.Size(374, 21);
            this.ctlEditId.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "编号:";
            // 
            // AGECreateModelWindow
            // 
            this.AcceptButton = this.ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(483, 150);
            this.Controls.Add(this.ctlListCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ctlBtnCancel);
            this.Controls.Add(this.ctlBtnOK);
            this.Controls.Add(this.ctlEditCaption);
            this.Controls.Add(this.ctlEditId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AGECreateModelWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGECreateModelWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ctlListCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ctlBtnCancel;
        private System.Windows.Forms.Button ctlBtnOK;
        private System.Windows.Forms.TextBox ctlEditCaption;
        private System.Windows.Forms.TextBox ctlEditId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}