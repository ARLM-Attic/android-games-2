using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditor.Windows.Model
{
    public partial class ModelListWindow : Form
    {
        public ModelListWindow()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            ReloadModels();

            base.OnShown(e);
        }

        private void ReloadModels()
        {
            treeView1.Nodes.Clear();
            List<ModelCategory> categories = ModelCategory.GetDefs();
            foreach (var category in categories)
            {
                TreeNode tnCategory = new TreeNode();
                tnCategory.Text = category.Caption;

                List<Model2D> models = DATUtility.GetModels(category.Id);
                foreach (var item in models)
                {
                    TreeNode tnModel = new TreeNode();
                    tnModel.Text = string.Format("{0}({1})", item.Caption, item.Id);
                    tnModel.Tag = item;
                    tnCategory.Nodes.Add(tnModel);
                }
                treeView1.Nodes.Add(tnCategory);
            }
            treeView1.ExpandAll();
        }

        private void _ctlBtnQuit_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ModelEditWindow window = new ModelEditWindow();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReloadModels();
            }
        }

        private void _ctlBtnEdit_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is Model2D)
            {
                ModelEditWindow window = new ModelEditWindow(treeView1.SelectedNode.Tag as Model2D);
                if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ReloadModels();
                }
            }
        }
    }
}
