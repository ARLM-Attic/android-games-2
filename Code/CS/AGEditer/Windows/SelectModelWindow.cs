using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGEditer
{
    public partial class SelectModelWindow : Form
    {
        public Model2D SelectedModel { get; set; }

        public SelectModelWindow()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
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

            base.OnShown(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode != null)
            {
                SelectedModel = selNode.Tag as Model2D;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo tvHit = treeView1.HitTest(e.X, e.Y);
            if (tvHit.Node != null && tvHit.Node.Tag != null)
            {
                SelectedModel = tvHit.Node.Tag as Model2D;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
