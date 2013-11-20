using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core;
using AG.Editor.Core.Data;

namespace AG.Editor.ModelUI.Windows
{
    public partial class AGESetAudioWindow : Form
    {
        /// <summary>
        /// 选中的音效
        /// </summary>
        public AGAudio SelectedAudio { get; private set; }

        public AGESetAudioWindow()
        {
            InitializeComponent();

            BindAudioTree();
        }

        private void ctlBtnOK_Click(object sender, EventArgs e)
        {
            if (ctlTreeAudio.SelectedNode == null)
            {
                return;
            }

            AGAudio audio = ctlTreeAudio.SelectedNode.Tag as AGAudio;
            if (audio != null)
            {
                SelectedAudio = audio;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
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
    }
}
