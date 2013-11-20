using System;
using System.ComponentModel;
using System.Windows.Forms;
using AG.Editor.Core;
using AG.Editor.UI;

namespace AGEditor
{
    public partial class MainWindow3 : Form
    {
        AGEMainMenuMidiator _menuMidiator;

        public MainWindow3()
        {
            InitializeComponent();
            ctlTxtProjectState.Text = "没有更改";

            _menuMidiator = new AGEMainMenuMidiator(menuStrip1);

            TabPage tabModelMgr = new TabPage("模型管理");
            AG.Editor.ModelUI.AGEModelMgrPanel modelMgrPanel = new AG.Editor.ModelUI.AGEModelMgrPanel();
            modelMgrPanel.Dock = DockStyle.Fill;
            tabModelMgr.Controls.Add(modelMgrPanel);
            tabModelMgr.Tag = modelMgrPanel;
            ctlTabControl.TabPages.Add(tabModelMgr);

            TabPage tabAudioMgr = new TabPage("音频管理");
            AG.Editor.AudioUI.AGEAudioMgrPanel audioMgrPanel = new AG.Editor.AudioUI.AGEAudioMgrPanel();
            audioMgrPanel.Dock = DockStyle.Fill;
            tabAudioMgr.Controls.Add(audioMgrPanel);
            tabAudioMgr.Tag = audioMgrPanel;
            ctlTabControl.TabPages.Add(tabAudioMgr);

            ctlTabControl.SelectedIndex = 0;
            modelMgrPanel.OnActived(_menuMidiator);
            ctlTabControl.SelectedIndexChanged += new EventHandler(ctlTabControl_SelectedIndexChanged);

            toolStripStatusLabel1.Text = AG.Editor.Core.AGEContext.Current.EProject.DateVersion;
            AG.Editor.Core.AGEContext.Current.EProject.SaveComplete();
            AG.Editor.Core.AGEContext.Current.EProject.PropertyChanged += new PropertyChangedEventHandler(EProject_PropertyChanged);
        }

        void ctlTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selTab = ctlTabControl.SelectedTab;
            IAGEMainComponent component = selTab.Tag as IAGEMainComponent;
            component.OnActived(_menuMidiator);
        }

        void EProject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AG.Editor.Core.Data.AGEProject project = sender as AG.Editor.Core.Data.AGEProject;
            if (e.PropertyName == "HasChanged")
            {
                if (project.HasChanged)
                {
                    ctlTxtProjectState.Text = "已更改，需要保存";
                }
                else
                {
                    ctlTxtProjectState.Text = "没有更改";
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (AG.Editor.Core.AGEContext.Current.EProject.HasChanged)
            {
                if (MessageBox.Show("项目信息有修改，是否要保存?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    // 保存
                    AGECache.Current.EProjectStore.SaveEProject(AG.Editor.Core.AGEContext.Current.EProject);
                }
            }

            base.OnClosing(e);
        }
    }
}
