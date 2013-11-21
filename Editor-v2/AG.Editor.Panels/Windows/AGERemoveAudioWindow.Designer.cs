namespace AG.Editor.ModelUI.Windows
{
    partial class AGERemoveAudioWindow
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
            this.ctlListAudio = new System.Windows.Forms.ListBox();
            this.ctlBtnOK = new System.Windows.Forms.Button();
            this.ctlBtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "以引用的音效:";
            // 
            // ctlListAudio
            // 
            this.ctlListAudio.FormattingEnabled = true;
            this.ctlListAudio.ItemHeight = 12;
            this.ctlListAudio.Location = new System.Drawing.Point(12, 28);
            this.ctlListAudio.Name = "ctlListAudio";
            this.ctlListAudio.Size = new System.Drawing.Size(403, 256);
            this.ctlListAudio.TabIndex = 1;
            // 
            // ctlBtnOK
            // 
            this.ctlBtnOK.Location = new System.Drawing.Point(135, 290);
            this.ctlBtnOK.Name = "ctlBtnOK";
            this.ctlBtnOK.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnOK.TabIndex = 2;
            this.ctlBtnOK.Text = "删除";
            this.ctlBtnOK.UseVisualStyleBackColor = true;
            this.ctlBtnOK.Click += new System.EventHandler(this.ctlBtnOK_Click);
            // 
            // ctlBtnCancel
            // 
            this.ctlBtnCancel.Location = new System.Drawing.Point(216, 290);
            this.ctlBtnCancel.Name = "ctlBtnCancel";
            this.ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnCancel.TabIndex = 3;
            this.ctlBtnCancel.Text = "取消";
            this.ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // AGERemoveAudioWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 336);
            this.Controls.Add(this.ctlBtnCancel);
            this.Controls.Add(this.ctlBtnOK);
            this.Controls.Add(this.ctlListAudio);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AGERemoveAudioWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGERemoveAudioWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ctlListAudio;
        private System.Windows.Forms.Button ctlBtnOK;
        private System.Windows.Forms.Button ctlBtnCancel;
    }
}