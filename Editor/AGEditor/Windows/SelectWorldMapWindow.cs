using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditor
{
    public partial class SelectWorldMapWindow : Form
    {
        public int SelectedMapId { get; set; }

        public SelectWorldMapWindow()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            List<int> maps = DATUtility.GetWorldMaps();
            foreach (var item in maps)
            {
                TreeNode tnModel = new TreeNode();
                tnModel.Text = item.ToString();
                tnModel.Tag = item;
                treeView1.Nodes.Add(tnModel);
            }

            base.OnShown(e);
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo tvHit = treeView1.HitTest(e.X, e.Y);
            if (tvHit.Node != null)
            {
                SelectedMapId = Convert.ToInt32(tvHit.Node.Tag);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode != null)
            {
                SelectedMapId = Convert.ToInt32(selNode.Tag);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
