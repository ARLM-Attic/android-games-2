using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AGEditor.Windows.Workspace
{
    public partial class CreateWorkspaceWindow : Form
    {
        /// <summary>
        /// 当前创建的工作空间
        /// </summary>
        public AGWorkspace Workspace { get; private set; }

        public CreateWorkspaceWindow()
        {
            InitializeComponent();
        }

        private void _ctlBtnBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Workspace = new AGWorkspace(new DirectoryInfo(dlg.SelectedPath).Name, dlg.SelectedPath);
                this._ctlBtnBrowse.Text = Workspace.Path;
                this._ctlBtnBrowse.Tag = Workspace;
            }
        }

        private void _ctlBtnOK_Click(object sender, EventArgs e)
        {
            if (this._ctlBtnBrowse.Tag == null)
            {
                MessageBox.Show("必须选择一个工作空间路径!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Workspace = this._ctlBtnBrowse.Tag as AGWorkspace;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
