using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core;
using AG.Editor.Windows;
using AG.Editor.Core.Data;
using AG.Editor.Windows.Controls;

namespace AG.Editor.Panels
{
    public partial class AGEModelMgrPanel : UserControl
    {
        AGEMainMenuMidiator _menuMidiator;
        private AGEPreviewModelPanel _previewPanel;

        public AGEModelMgrPanel(AGEMainMenuMidiator menuMidiator)
        {
            InitializeComponent();

            _menuMidiator = menuMidiator;

            BindModelTree();

            _previewPanel = new AGEPreviewModelPanel();
            _previewPanel.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(_previewPanel);
        }

        private void BindModelTree()
        {
            ctlTreeModels.Nodes.Clear();
            foreach (var modelCategory in AGEContext.Current.EProject.TProject.ModelCategories)
            {
                TreeNode tnCategory = new TreeNode();
                tnCategory.Text = modelCategory.Caption;
                tnCategory.Tag = modelCategory;
                this.ctlTreeModels.Nodes.Add(tnCategory);

                List<AGModelRef> models = AGEContext.Current.EProject.Models.Where(p => p.CategoryId == modelCategory.Id).ToList();
                for (int iModel = 0; iModel < models.Count; iModel++)
                {
                    AGModelRef model = models[iModel];
                    TreeNode tnModel = new TreeNode();
                    tnModel.Text = model.ToString();
                    tnModel.Tag = model;
                    tnCategory.Nodes.Add(tnModel);
                }
            }
            ctlTreeModels.ExpandAll();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                _menuMidiator.Clear();

                ToolStripMenuItem mFile = _menuMidiator.AddMenu(new ToolStripMenuItem("文件"));
                ToolStripMenuItem miSaveProject = new ToolStripMenuItem("保存项目");
                miSaveProject.Click += new EventHandler(miSaveProject_Click);
                mFile.DropDownItems.Add(miSaveProject);
                ToolStripMenuItem miClose = new ToolStripMenuItem("关闭");
                miClose.Click += new EventHandler(miClose_Click);
                mFile.DropDownItems.Add(miClose);

                ToolStripMenuItem m1 = _menuMidiator.AddMenu(new ToolStripMenuItem("模型管理"));

                ToolStripMenuItem miCreateModel = new ToolStripMenuItem("创建模型");
                miCreateModel.Click += new EventHandler(miCreateModel_Click);
                m1.DropDownItems.Add(miCreateModel);

                ToolStripMenuItem miEditModel = new ToolStripMenuItem("修改模型");
                miEditModel.Click += new EventHandler(miEditModel_Click);
                m1.DropDownItems.Add(miEditModel);

                ToolStripMenuItem miRemoveModel = new ToolStripMenuItem("删除模型");
                miRemoveModel.Click += new EventHandler(miRemoveModel_Click);
                m1.DropDownItems.Add(miRemoveModel);
            }

            base.OnVisibleChanged(e);
        }

        void miClose_Click(object sender, EventArgs e)
        {
            (this.ParentForm).Close();
        }

        void miSaveProject_Click(object sender, EventArgs e)
        {
            if (AG.Editor.Core.AGEContext.Current.EProject.HasChanged)
            {
                AG.Editor.Core.AGECache.Current.EProjectStore.SaveEProject(AG.Editor.Core.AGEContext.Current.EProject);
                AG.Editor.Core.AGEContext.Current.EProject.SaveComplete();
                MessageBox.Show("保存完毕", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void miRemoveModel_Click(object sender, EventArgs e)
        {
            TreeNode selNode = ctlTreeModels.SelectedNode;
            if (selNode == null)
            {
                return;
            }

            AGModelRef modelRef = selNode.Tag as AGModelRef;
            if (modelRef == null)
            {
                return;
            }

            if (MessageBox.Show(string.Format("是否要删除模型[{0}]", modelRef.Caption), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                AGEContext.Current.EProject.RemoveModel(modelRef.Id);
                BindModelTree();
            }
        }

        /// <summary>
        /// 创建新模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void miCreateModel_Click(object sender, EventArgs e)
        {
            AGEEditModelWindow window = new AGEEditModelWindow();
            window.ShowDialog();
            if (window.SavedModel != null)
            {
                AGModelRef modelRef = new AGModelRef(window.SavedModel);
                AGEContext.Current.EProject.Models.Add(modelRef);
                BindModelTree();
            }
        }

        void miEditModel_Click(object sender, EventArgs e)
        {
            TreeNode selNode = ctlTreeModels.SelectedNode;
            if (selNode == null)
            {
                return;
            }

            AGModelRef modelRef = selNode.Tag as AGModelRef;
            if (modelRef == null)
            {
                return;
            }

            AGModel model = AGECache.Current.ModelStore.GetModel(AGEContext.Current.EProject, modelRef);

            AGEEditModelWindow window = new AGEEditModelWindow(model);
            window.ShowDialog();
        }

        private void ctlTreeModels_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selNode = e.Node;
            AGModelRef selModel = selNode.Tag as AGModelRef;

            if (selModel != null)
            {
                AGModel model = AGECache.Current.ModelStore.GetModel(AGEContext.Current.EProject, selModel);
                _previewPanel.SetModel(model);
            }
        }
    }
}
