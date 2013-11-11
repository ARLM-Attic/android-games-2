namespace AGEditor.Windows.Model
{
    partial class ModelFrameEditWrapper
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._ctlBtnSetOffsetX = new System.Windows.Forms.LinkLabel();
            this._ctlBtnSetOffsetY = new System.Windows.Forms.LinkLabel();
            this._ctlBtnX1 = new System.Windows.Forms.Button();
            this._ctlBtnX2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this._ctlBtnX2);
            this.panel1.Controls.Add(this._ctlBtnX1);
            this.panel1.Controls.Add(this._ctlBtnSetOffsetY);
            this.panel1.Controls.Add(this._ctlBtnSetOffsetX);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 39);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "offset-x:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "offset-y:";
            // 
            // _ctlBtnSetOffsetX
            // 
            this._ctlBtnSetOffsetX.AutoSize = true;
            this._ctlBtnSetOffsetX.Location = new System.Drawing.Point(80, 9);
            this._ctlBtnSetOffsetX.Name = "_ctlBtnSetOffsetX";
            this._ctlBtnSetOffsetX.Padding = new System.Windows.Forms.Padding(5);
            this._ctlBtnSetOffsetX.Size = new System.Drawing.Size(21, 22);
            this._ctlBtnSetOffsetX.TabIndex = 4;
            this._ctlBtnSetOffsetX.TabStop = true;
            this._ctlBtnSetOffsetX.Text = "0";
            this._ctlBtnSetOffsetX.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._ctlBtnSetOffsetX_LinkClicked);
            // 
            // _ctlBtnSetOffsetY
            // 
            this._ctlBtnSetOffsetY.AutoSize = true;
            this._ctlBtnSetOffsetY.Location = new System.Drawing.Point(200, 9);
            this._ctlBtnSetOffsetY.Name = "_ctlBtnSetOffsetY";
            this._ctlBtnSetOffsetY.Padding = new System.Windows.Forms.Padding(5);
            this._ctlBtnSetOffsetY.Size = new System.Drawing.Size(21, 22);
            this._ctlBtnSetOffsetY.TabIndex = 5;
            this._ctlBtnSetOffsetY.TabStop = true;
            this._ctlBtnSetOffsetY.Text = "0";
            this._ctlBtnSetOffsetY.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._ctlBtnSetOffsetY_LinkClicked);
            // 
            // _ctlBtnX1
            // 
            this._ctlBtnX1.Location = new System.Drawing.Point(341, 8);
            this._ctlBtnX1.Name = "_ctlBtnX1";
            this._ctlBtnX1.Size = new System.Drawing.Size(75, 23);
            this._ctlBtnX1.TabIndex = 6;
            this._ctlBtnX1.Text = "X1";
            this._ctlBtnX1.UseVisualStyleBackColor = true;
            this._ctlBtnX1.Click += new System.EventHandler(this._ctlBtnX1_Click);
            // 
            // _ctlBtnX2
            // 
            this._ctlBtnX2.Location = new System.Drawing.Point(422, 8);
            this._ctlBtnX2.Name = "_ctlBtnX2";
            this._ctlBtnX2.Size = new System.Drawing.Size(75, 23);
            this._ctlBtnX2.TabIndex = 7;
            this._ctlBtnX2.Text = "X2";
            this._ctlBtnX2.UseVisualStyleBackColor = true;
            this._ctlBtnX2.Click += new System.EventHandler(this._ctlBtnX2_Click);
            // 
            // ModelFrameEditWrapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ModelFrameEditWrapper";
            this.Size = new System.Drawing.Size(616, 345);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _ctlBtnSetOffsetY;
        private System.Windows.Forms.LinkLabel _ctlBtnSetOffsetX;
        private System.Windows.Forms.Button _ctlBtnX2;
        private System.Windows.Forms.Button _ctlBtnX1;
    }
}
