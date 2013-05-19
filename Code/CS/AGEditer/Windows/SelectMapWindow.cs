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
    public partial class SelectMapWindow : Form
    {
        public int SelectedMapId { get; set; }

        public SelectMapWindow()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            List<MapInfo> maps = DATUtility.GetMaps();
            foreach (var item in maps)
            {
                TreeNode tnModel = new TreeNode();
                tnModel.Text = item.ToString();
                tnModel.Tag = item;
                treeView1.Nodes.Add(tnModel);
            }

            base.OnShown(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode != null)
            {
                SelectedMapId = (selNode.Tag as MapInfo).Id;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo tvHit = treeView1.HitTest(e.X, e.Y);
            if (tvHit.Node != null)
            {
                SelectedMapId = (tvHit.Node.Tag as MapInfo).Id;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
