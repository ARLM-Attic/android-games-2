using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.UI;
using AG.Editor.AudioUI.Windows;
using AG.Editor.Core;
using AG.Editor.Core.Data;

namespace AG.Editor.AudioUI
{
    public partial class AGEAudioMgrPanel : UserControl, IAGEMainComponent
    {
        public AGEAudioMgrPanel()
        {
            InitializeComponent();

            BindAudioTree();
        }

        public void OnActived(AGEMainMenuMidiator mmm)
        {
            mmm.Clear();

            ToolStripMenuItem mFile = mmm.AddMenu(new ToolStripMenuItem("文件"));
            ToolStripMenuItem miSaveProject = new ToolStripMenuItem("保存项目");
            //miSaveProject.Click += new EventHandler(miSaveProject_Click);
            mFile.DropDownItems.Add(miSaveProject);
            ToolStripMenuItem miClose = new ToolStripMenuItem("关闭");
            //miClose.Click += new EventHandler(miClose_Click);
            mFile.DropDownItems.Add(miClose);

            ToolStripMenuItem m1 = mmm.AddMenu(new ToolStripMenuItem("音频管理"));

            ToolStripMenuItem miCreateModel = new ToolStripMenuItem("添加音频文件");
            miCreateModel.Click += new EventHandler(miCreateModel_Click);
            m1.DropDownItems.Add(miCreateModel);

            ToolStripMenuItem miEditModel = new ToolStripMenuItem("修改音频文件");
            //miEditModel.Click += new EventHandler(miEditModel_Click);
            m1.DropDownItems.Add(miEditModel);

            ToolStripMenuItem miRemoveModel = new ToolStripMenuItem("删除音频文件");
            //miRemoveModel.Click += new EventHandler(miRemoveModel_Click);
            m1.DropDownItems.Add(miRemoveModel);
        }

        private void BindAudioTree()
        {
            ctlTreeAudio.Nodes.Clear();
            foreach (var category in AGEContext.Current.EProject.TProject.AudioCateogries)
            {
                TreeNode tnCategory = new TreeNode();
                tnCategory.Text = category.Caption;
                tnCategory.Tag = category;
                this.ctlTreeAudio.Nodes.Add(tnCategory);

                List<AGAudio> audios = AGEContext.Current.EProject.Audios.Where(p => p.CategoryId == category.Id).ToList();
                for (int iModel = 0; iModel < audios.Count; iModel++)
                {
                    AGAudio model = audios[iModel];
                    TreeNode tnModel = new TreeNode();
                    tnModel.Text = model.ToString();
                    tnModel.Tag = model;
                    tnCategory.Nodes.Add(tnModel);
                }
            }
            ctlTreeAudio.ExpandAll();
        }

        /// <summary>
        /// 添加音频按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void miCreateModel_Click(object sender, EventArgs e)
        {
            AGEEditAudioWindow window = new AGEEditAudioWindow();
            if (window.ShowDialog() == DialogResult.OK)
            {
                AG.Editor.Core.AGEContext.Current.EProject.AddAudio(window.Audio);
                BindAudioTree();
            }
        }
    }
}
