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
        private AGModel _model;

        /// <summary>
        /// 选中的音效
        /// </summary>
        public AGAudio SelectedAudio { get; private set; }

        public AGESetAudioWindow(AGModel model)
        {
            InitializeComponent();

            _model = model;

            BindAudioTree();
            BindListActions();
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

        private void BindListActions()
        {
            AGAction defaultAction = _model.GetAction(1);
            AGDirection defaultDirection = defaultAction.GetDirection(1);

            foreach (var action in _model.Actions)
            {
                ctlListAction.Items.Add(action);
            }

            for (int index = 0; index < defaultDirection.Frames.Count; index++)
            {
                ctlListFrame.Items.Add(string.Format("frame-{0}",defaultDirection.Frames[index].Id));
            }
        }
    }
}
