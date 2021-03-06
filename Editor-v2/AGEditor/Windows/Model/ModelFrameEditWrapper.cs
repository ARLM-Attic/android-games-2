﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditor.Windows.Model
{
    public partial class ModelFrameEditWrapper : UserControl, IFrameEditObserver
    {
        private ModelFrameEditPanel _editPanel;

        public ModelFrameEditWrapper(ModelFrameEditPanel editPanel)
        {
            InitializeComponent();

            _editPanel = editPanel;
            _editPanel.Dock = DockStyle.Fill;
            this.Controls.Add(_editPanel);
            _editPanel.AttachObserver(this);
        }

        public void OnFrameChanged(Frame2D frame)
        {
            this._ctlBtnSetOffsetX.Text = frame.OffsetX.ToString();
            this._ctlBtnSetOffsetY.Text = frame.offsetY.ToString();
        }

        private void _ctlBtnSetOffsetX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.AGEInputIntegerWindow inputWindow = new Common.AGEInputIntegerWindow();
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                _editPanel.SetOffset(inputWindow.ReturnValue, _editPanel.GetOffset().Y);
            }
        }

        private void _ctlBtnSetOffsetY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.AGEInputIntegerWindow inputWindow = new Common.AGEInputIntegerWindow();
            if (inputWindow.ShowDialog() == DialogResult.OK)
            {
                _editPanel.SetOffset(_editPanel.GetOffset().X, inputWindow.ReturnValue);
            }
        }

        private void _ctlBtnX1_Click(object sender, EventArgs e)
        {

        }

        private void _ctlBtnX2_Click(object sender, EventArgs e)
        {

        }
    }
}
