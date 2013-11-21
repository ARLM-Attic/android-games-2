namespace AG.Editor.Windows
{
    partial class AGECreateProjectWindow
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
            this.ctlListTProject = new System.Windows.Forms.ComboBox();
            this.ctlEditName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ctlEditPath = new System.Windows.Forms.TextBox();
            this.ctlBtnBrowse = new System.Windows.Forms.Button();
            this.ctlBtnOK = new System.Windows.Forms.Button();
            this.ctlBtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "名称:";
            // 
            // ctlListTProject
            // 
            this.ctlListTProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlListTProject.FormattingEnabled = true;
            this.ctlListTProject.Location = new System.Drawing.Point(54, 10);
            this.ctlListTProject.Name = "ctlListTProject";
            this.ctlListTProject.Size = new System.Drawing.Size(462, 20);
            this.ctlListTProject.TabIndex = 2;
            // 
            // ctlEditName
            // 
            this.ctlEditName.Location = new System.Drawing.Point(54, 36);
            this.ctlEditName.Name = "ctlEditName";
            this.ctlEditName.Size = new System.Drawing.Size(462, 21);
            this.ctlEditName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "路径:";
            // 
            // ctlEditPath
            // 
            this.ctlEditPath.Location = new System.Drawing.Point(54, 63);
            this.ctlEditPath.Name = "ctlEditPath";
            this.ctlEditPath.Size = new System.Drawing.Size(381, 21);
            this.ctlEditPath.TabIndex = 5;
            // 
            // ctlBtnBrowse
            // 
            this.ctlBtnBrowse.Location = new System.Drawing.Point(441, 61);
            this.ctlBtnBrowse.Name = "ctlBtnBrowse";
            this.ctlBtnBrowse.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnBrowse.TabIndex = 6;
            this.ctlBtnBrowse.Text = "选择";
            this.ctlBtnBrowse.UseVisualStyleBackColor = true;
            // 
            // ctlBtnOK
            // 
            this.ctlBtnOK.Location = new System.Drawing.Point(188, 91);
            this.ctlBtnOK.Name = "ctlBtnOK";
            this.ctlBtnOK.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnOK.TabIndex = 7;
            this.ctlBtnOK.Text = "确定";
            this.ctlBtnOK.UseVisualStyleBackColor = true;
            this.ctlBtnOK.Click += new System.EventHandler(this.ctlBtnOK_Click);
            // 
            // ctlBtnCancel
            // 
            this.ctlBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlBtnCancel.Location = new System.Drawing.Point(269, 90);
            this.ctlBtnCancel.Name = "ctlBtnCancel";
            this.ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnCancel.TabIndex = 8;
            this.ctlBtnCancel.Text = "取消";
            this.ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // AGECreateProjectWindow
            // 
            this.AcceptButton = this.ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(532, 127);
            this.Controls.Add(this.ctlBtnCancel);
            this.Controls.Add(this.ctlBtnOK);
            this.Controls.Add(this.ctlBtnBrowse);
            this.Controls.Add(this.ctlEditPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctlEditName);
            this.Controls.Add(this.ctlListTProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AGECreateProjectWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建项目";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ctlListTProject;
        private System.Windows.Forms.TextBox ctlEditName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ctlEditPath;
        private System.Windows.Forms.Button ctlBtnBrowse;
        private System.Windows.Forms.Button ctlBtnOK;
        private System.Windows.Forms.Button ctlBtnCancel;
    }
}