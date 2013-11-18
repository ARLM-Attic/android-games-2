﻿using System;
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
            ctlTxtProjectState.Text = "没有更改";

            _menuMidiator = new AGEMainMenuMidiator(menuStrip1);

            TabPage tabModelMgr = new TabPage("模型管理");
            AG.Editor.Panels.AGEModelMgrPanel modelMgrPanel = new AG.Editor.Panels.AGEModelMgrPanel(_menuMidiator);
            modelMgrPanel.Dock = DockStyle.Fill;
            tabModelMgr.Controls.Add(modelMgrPanel);
            ctlTabControl.TabPages.Add(tabModelMgr);

            //ctlTabControl.TabPages.Add(new TabPage("a1"));
            //ctlTabControl.TabPages.Add(new TabPage("a2"));
            //ctlTabControl.TabPages.Add(new TabPage("a3"));

            toolStripStatusLabel1.Text = AG.Editor.Core.AGEContext.Current.EProject.DateVersion;
            AG.Editor.Core.AGEContext.Current.EProject.SaveComplete();
            AG.Editor.Core.AGEContext.Current.EProject.PropertyChanged += new PropertyChangedEventHandler(EProject_PropertyChanged);
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
