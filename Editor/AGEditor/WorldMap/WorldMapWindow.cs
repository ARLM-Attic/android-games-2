using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditer
{
    public partial class WorldMapWindow : Form
    {
        private WorldMap _map;
        private WorldMapDesignPanel _designPanel;

        public WorldMapWindow()
        {
            InitializeComponent();

            _designPanel = new WorldMapDesignPanel();

            splitContainer1.Panel2.AutoScroll = true;
            splitContainer1.Panel2.Controls.Add(_designPanel);

            List<MapInfo> maps = DATUtility.GetMaps();
            foreach (var item in maps)
            {
                TreeNode tnModel = new TreeNode();
                tnModel.Text = item.ToString();
                tnModel.Tag = item;
                treeView1.Nodes.Add(tnModel);
            }
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int selMapId = (e.Node.Tag as MapInfo).Id;

            for (int index = 0; index < _map.StagesPosList.Count; index++)
            {
                if (_map.StagesPosList[index].MapId == selMapId)
                {
                    return;
                }
            }

            _designPanel.SelectMap(selMapId, DATUtility.GetModel(14));
        }

        private void _ctlBtnCreate_Click(object sender, EventArgs e)
        {
            CreateWorldMapWindow window = new CreateWorldMapWindow();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _map = window.WorldMap;
                _designPanel.SetMap(_map);
            }
        }

        private void publishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DATUtility.SaveWorldMap(_map))
            {
                MessageBox.Show("发布成功!");
            }
            else
            {
                MessageBox.Show("发布失败!");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectWorldMapWindow window = new SelectWorldMapWindow();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WorldMap map = DATUtility.GetWorldMap(window.SelectedMapId);
                _map = map;
                _designPanel.SetMap(_map);
            }
        }
    }
}
