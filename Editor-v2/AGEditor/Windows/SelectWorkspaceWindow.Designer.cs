namespace AGEditor.Windows
{
    partial class SelectWorkspaceWindow
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
            this._ctlBtnCurrentWS = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._ctlListHWS = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前工作空间:";
            // 
            // _ctlBtnCurrentWS
            // 
            this._ctlBtnCurrentWS.AutoSize = true;
            this._ctlBtnCurrentWS.Location = new System.Drawing.Point(38, 42);
            this._ctlBtnCurrentWS.Name = "_ctlBtnCurrentWS";
            this._ctlBtnCurrentWS.Size = new System.Drawing.Size(29, 12);
            this._ctlBtnCurrentWS.TabIndex = 1;
            this._ctlBtnCurrentWS.TabStop = true;
            this._ctlBtnCurrentWS.Text = "None";
            this._ctlBtnCurrentWS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._ctlBtnCurrentWS_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "历史:";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(303, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 200);
            this.label3.TabIndex = 3;
            // 
            // _ctlListHWS
            // 
            this._ctlListHWS.FormattingEnabled = true;
            this._ctlListHWS.ItemHeight = 12;
            this._ctlListHWS.Location = new System.Drawing.Point(338, 33);
            this._ctlListHWS.Name = "_ctlListHWS";
            this._ctlListHWS.Size = new System.Drawing.Size(299, 184);
            this._ctlListHWS.TabIndex = 4;
            // 
            // SelectWorkspaceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 240);
            this.Controls.Add(this._ctlListHWS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._ctlBtnCurrentWS);
            this.Controls.Add(this.label1);
            this.Name = "SelectWorkspaceWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectWorkspaceWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _ctlBtnCurrentWS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox _ctlListHWS;
    }
}