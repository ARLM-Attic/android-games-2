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
    public partial class UnitWindow : Form
    {
        Unit2D _unit;

        private UnitPanel _unitPanel;

        public UnitWindow()
        {
            InitializeComponent();

            _unitPanel = new UnitPanel();
            //_unitPanel.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(_unitPanel);

            BindToTree(treeView1);
        }

        private void _ctlBtnCreate_Click(object sender, EventArgs e)
        {
            CreateUnitWindow window = new CreateUnitWindow();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _unit = window.Unit;
                DATUtility.SaveUnit(_unit);

                BindToTree(treeView1);
            }
        }

        private void BindToTree(TreeView tree)
        {
            tree.Nodes.Clear();
            List<UnitStirps> stirpsList = UnitStirps.GetDefs();
            foreach (var item in stirpsList)
            {
                TreeNode tnStirps = new TreeNode();
                tnStirps.Text = item.Caption;

                List<UnitCategory> categories = UnitCategoryDef.GetDefs();
                foreach (var category in categories)
                {
                    TreeNode tnCategory = new TreeNode();
                    tnCategory.Text = category.Caption;
                    List<int> units = DATUtility.GetUnits(item.Id, category.Id);
                    foreach (var unit in units)
                    {
                        TreeNode tnUnit = new TreeNode();
                        tnUnit.Text = unit.ToString();
                        tnUnit.Tag = unit;

                        tnCategory.Nodes.Add(tnUnit);
                    }

                    tnStirps.Nodes.Add(tnCategory);
                }

                tree.Nodes.Add(tnStirps);
            }
            tree.ExpandAll();
        
            //tree.Nodes.Clear();
            //TreeNode tnUnit = new TreeNode();
            //tnUnit.Text = unit.Caption;
            //tnUnit.Tag = unit;
            //tree.Nodes.Add(tnUnit);
        }

        private void _ctlBtnPublish_Click(object sender, EventArgs e)
        {
            if (DATUtility.SaveUnit(_unit))
            {
                MessageBox.Show("发布单位成功!");
            }
            else
            {
                MessageBox.Show("发布单位失败!");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = e.Node;
            if (selNode.Tag != null)
            {
                Unit2D unit = DATUtility.GetUnit((int)selNode.Tag);
                _unitPanel.SetUnit(unit);
                _unit = unit;
            }
        }
    }
}
