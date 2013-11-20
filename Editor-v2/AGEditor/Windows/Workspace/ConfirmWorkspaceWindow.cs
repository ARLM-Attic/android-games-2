using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditor.Windows.Workspace
{
    public partial class ConfirmWorkspaceWindow : Form
    {
        public AGWorkspace Workspace { get; private set; }

        public ConfirmWorkspaceWindow(AGEditorConfig config)
        {
            InitializeComponent();

            this._ctlBtnEnterWS.Text = string.Format("{0}({1})", config.Workspace.Name, config.Workspace.Path);
            this._ctlBtnEnterWS.Tag = config.Workspace;

            listBox1.DataSource = config.HistoryWorkspace;
        }

        private void _ctlBtnOK_Click(object sender, EventArgs e)
        {
            Workspace = this._ctlBtnEnterWS.Tag as AGWorkspace;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void _ctlBtnEnterWS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Workspace = this._ctlBtnEnterWS.Tag as AGWorkspace;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
