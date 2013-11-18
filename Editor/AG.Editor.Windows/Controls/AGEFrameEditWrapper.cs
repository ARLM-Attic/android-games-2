using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Data;

namespace AG.Editor.Windows.Controls
{
    public partial class AGEFrameEditWrapper : UserControl, IFrameEditObserver
    {
        private AGEFrameEditPanel _editPanel;

        public AGEFrameEditWrapper(List<AGFrame> frames, AGFrame curFrame, AGEFrameEditPanel editPanel)
        {
            InitializeComponent();

            _editPanel = editPanel;
            _editPanel.Dock = DockStyle.Fill;
            this.Controls.Add(_editPanel);
            _editPanel.BringToFront();
            _editPanel.AttachObserver(this);

            foreach (var frame in frames)
            {
                if (frame == curFrame)
                {
                    //this.checkedListBox1.Items.Add(frame, true);
                }
                else
                {
                    this.checkedListBox1.Items.Add(frame, false);
                }
            }
        }

        public void OnFrameChanged(AGFrame frame)
        {
            this._ctlBtnSetOffsetX.Text = frame.AnchorPointX.ToString();
            this._ctlBtnSetOffsetY.Text = frame.AnchorPointY.ToString();
        }

        private void _ctlBtnSetOffsetX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*
            Common.AGEInputIntegerWindow inputWindow = new Common.AGEInputIntegerWindow();
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                _editPanel.SetOffset(inputWindow.ReturnValue, _editPanel.GetOffset().Y);
            }
             * */
        }

        private void _ctlBtnSetOffsetY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*
            Common.AGEInputIntegerWindow inputWindow = new Common.AGEInputIntegerWindow();
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                _editPanel.SetOffset(_editPanel.GetOffset().X, inputWindow.ReturnValue);
            }
             * */
        }

        private void _ctlBtnX1_Click(object sender, EventArgs e)
        {

        }

        private void _ctlBtnX2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this._editPanel.Frames != null)
            {
                int index = e.Index;
                if (e.NewValue == CheckState.Unchecked)
                {
                    // unchecked;
                    this._editPanel.SetVisible(this._editPanel.Frames[index], false);
                }
                else if (e.NewValue == CheckState.Checked)
                {
                    // checked;
                    this._editPanel.SetVisible(this._editPanel.Frames[index], true);
                }
            }
        }
    }
}
