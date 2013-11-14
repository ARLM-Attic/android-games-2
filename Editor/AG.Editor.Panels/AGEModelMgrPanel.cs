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

namespace AG.Editor.Panels
{
    public partial class AGEModelMgrPanel : UserControl
    {
        AGEMainMenuMidiator _menuMidiator;

        public AGEModelMgrPanel(AGEMainMenuMidiator menuMidiator)
        {
            InitializeComponent();

            _menuMidiator = menuMidiator;

            BindModelTree();
        }

        private void BindModelTree()
        {
            foreach (var modelCategory in AGEContext.Current.EProject.TProject.ModelCategories)
            {
                TreeNode tnCategory = new TreeNode();
                tnCategory.Text = modelCategory.Caption;
                this.ctlTreeModels.Nodes.Add(tnCategory);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                _menuMidiator.Clear();
                ToolStripMenuItem m1 = _menuMidiator.AddMenu(new ToolStripMenuItem("模型管理"));

                ToolStripMenuItem miCreateModel = new ToolStripMenuItem("创建模型");
                miCreateModel.Click += new EventHandler(miCreateModel_Click);
                m1.DropDownItems.Add(miCreateModel);

                ToolStripMenuItem miRemoveModel = new ToolStripMenuItem("删除模型");
                miRemoveModel.Click += new EventHandler(miRemoveModel_Click);
                m1.DropDownItems.Add(miRemoveModel);
            }

            base.OnVisibleChanged(e);
        }

        void miRemoveModel_Click(object sender, EventArgs e)
        {
        }

        void miCreateModel_Click(object sender, EventArgs e)
        {
            AGEEditModelWindow window = new AGEEditModelWindow();
            if (window.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
