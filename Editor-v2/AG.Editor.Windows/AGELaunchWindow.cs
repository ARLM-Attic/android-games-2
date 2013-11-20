using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Data;
using AG.Editor.Core.Settings;
using AG.Editor.Core;

namespace AG.Editor.Windows
{
    public partial class AGELaunchWindow : Form
    {
        public AGEProject SelectedEProject { get; private set; }
        private AGESettings _settings;

        public AGELaunchWindow(AGESettings settings)
        {
            InitializeComponent();

            _settings = settings;

            if (string.IsNullOrEmpty(_settings.LatestEProjectPath))
            {
                ctlLinkLatest.Enabled = false;
            }
            else
            {
                ctlLinkLatest.Text = _settings.LatestEProjectPath;
            }
        }

        private void ctlBtnOK_Click(object sender, EventArgs e)
        {
            if (!ctlLinkLatest.Enabled)
            {
                MessageBox.Show("没有可以使用的项目!");
                return;
            }
            
        }

        private void ctlLinkLatest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filePath = ctlLinkLatest.Text;
            SelectedEProject = AGECache.Current.EProjectStore.GetEProject(filePath);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ctlBtnCreate_Click(object sender, EventArgs e)
        {
            AGECreateProjectWindow createWindow = new AGECreateProjectWindow();
            if (createWindow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedEProject = createWindow.EProject;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
