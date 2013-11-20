namespace AG.Editor.ModelUI.Windows
{
    partial class AGESetAudioWindow
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
            this.ctlTreeAudio = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.ctlBtnOK = new System.Windows.Forms.Button();
            this.ctlBtnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ctlListAction = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ctlListFrame = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ctlTreeAudio
            // 
            this.ctlTreeAudio.Location = new System.Drawing.Point(13, 70);
            this.ctlTreeAudio.Name = "ctlTreeAudio";
            this.ctlTreeAudio.Size = new System.Drawing.Size(348, 336);
            this.ctlTreeAudio.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择音效:";
            // 
            // ctlBtnOK
            // 
            this.ctlBtnOK.Location = new System.Drawing.Point(108, 412);
            this.ctlBtnOK.Name = "ctlBtnOK";
            this.ctlBtnOK.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnOK.TabIndex = 2;
            this.ctlBtnOK.Text = "确定";
            this.ctlBtnOK.UseVisualStyleBackColor = true;
            this.ctlBtnOK.Click += new System.EventHandler(this.ctlBtnOK_Click);
            // 
            // ctlBtnCancel
            // 
            this.ctlBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlBtnCancel.Location = new System.Drawing.Point(189, 412);
            this.ctlBtnCancel.Name = "ctlBtnCancel";
            this.ctlBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.ctlBtnCancel.TabIndex = 3;
            this.ctlBtnCancel.Text = "取消";
            this.ctlBtnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "动作:";
            // 
            // ctlListAction
            // 
            this.ctlListAction.FormattingEnabled = true;
            this.ctlListAction.Location = new System.Drawing.Point(53, 6);
            this.ctlListAction.Name = "ctlListAction";
            this.ctlListAction.Size = new System.Drawing.Size(308, 20);
            this.ctlListAction.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "帧:";
            // 
            // ctlListFrame
            // 
            this.ctlListFrame.FormattingEnabled = true;
            this.ctlListFrame.Location = new System.Drawing.Point(53, 32);
            this.ctlListFrame.Name = "ctlListFrame";
            this.ctlListFrame.Size = new System.Drawing.Size(308, 20);
            this.ctlListFrame.TabIndex = 7;
            // 
            // AGESelectAudioWindow
            // 
            this.AcceptButton = this.ctlBtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ctlBtnCancel;
            this.ClientSize = new System.Drawing.Size(373, 449);
            this.Controls.Add(this.ctlListFrame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctlListAction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctlBtnCancel);
            this.Controls.Add(this.ctlBtnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctlTreeAudio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AGESelectAudioWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGESelectAudioWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ctlTreeAudio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ctlBtnOK;
        private System.Windows.Forms.Button ctlBtnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ctlListAction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ctlListFrame;
    }
}