using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core;
using AG.Editor.Panels;

namespace AGEditor
{
    public partial class MainWindow3 : Form
    {
        AGEMainMenuMidiator _menuMidiator;

        public MainWindow3()
        {
            InitializeComponent();

            _menuMidiator = new AGEMainMenuMidiator(menuStrip1);

            TabPage tabModelMgr = new TabPage("模型管理");
            AG.Editor.Panels.AGEModelMgrPanel modelMgrPanel = new AG.Editor.Panels.AGEModelMgrPanel(_menuMidiator);
            modelMgrPanel.Dock = DockStyle.Fill;
            tabModelMgr.Controls.Add(modelMgrPanel);
            ctlTabControl.TabPages.Add(tabModelMgr);


            ctlTabControl.TabPages.Add(new TabPage("a1"));
            ctlTabControl.TabPages.Add(new TabPage("a2"));
            ctlTabControl.TabPages.Add(new TabPage("a3"));

            toolStripStatusLabel1.Text = AG.Editor.Core.AGEContext.Current.EProject.DateVersion;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // 保存
            AGECache.Current.EProjectStore.SaveEProject(AG.Editor.Core.AGEContext.Current.EProject);

            base.OnClosing(e);
        }
    }
}
