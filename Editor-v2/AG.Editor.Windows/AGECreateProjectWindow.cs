using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Data;
using AG.Editor.Core.Metadata;
using AG.Editor.Core;
using System.IO;

namespace AG.Editor.Windows
{
    public partial class AGECreateProjectWindow : Form
    {
        public AGEProject EProject { get; private set; }

        public AGECreateProjectWindow()
        {
            InitializeComponent();

            List<AGTProjectSummary> tpList = AGECache.Current.TProjectStore.GetTProjects();
            ctlListTProject.DisplayMember = "Name";
            ctlListTProject.DataSource = tpList;

            ctlEditPath.Text = AppDomain.CurrentDomain.BaseDirectory +"output\\";
        }

        private void ctlBtnOK_Click(object sender, EventArgs e)
        {
            AGTProjectSummary selectedItem = ctlListTProject.SelectedItem as AGTProjectSummary;
            if (selectedItem == null)
            {
                return;
            }

            AGTProject project = AGECache.Current.TProjectStore.GetTProject(selectedItem.Name);

            EProject = new AGEProject();
            EProject.Path = ctlEditPath.Text.Trim();
            EProject.Name = ctlEditName.Text.Trim();
            EProject.Path = string.Format("{0}\\{1}.xml", EProject.Path, EProject.Name);
            EProject.Path = new FileInfo(EProject.Path).FullName;
            EProject.TProject = project;
            EProject.TPName = selectedItem.Name;
            EProject.TPVersion = selectedItem.Version;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
