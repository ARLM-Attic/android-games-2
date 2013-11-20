using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AGEditor.Windows.Workspace;

namespace AGEditor
{
    public partial class MainWindow : Form
    {
        private MapDesignPanel _panel;

        private Map2D _map;
        private object _selectedObject;

        public MainWindow()
        {
            InitializeComponent();

            InitStatusbar();

            _panel = new MapDesignPanel();
            //_panel.Dock = DockStyle.Fill;

            this.panel1.Controls.Add(_panel);
            this.panel1.AutoScroll = true;

            _treeCamp.AfterSelect += _treeCamp_AfterSelect;
            _ctlTreeTerrain.AfterSelect += _ctlTreeTerrain_AfterSelect;
        }

        private void InitStatusbar()
        {
            this._ctlLabelWS.Text = AGEContext.Current.Config.Workspace.Name;
            this._ctlLabelWS.ToolTipText = AGEContext.Current.Config.Workspace.Path;
            this._ctlLabelWS.AutoToolTip = true;
        }

        private void BindUnitTree()
        {
            _ctlTreeUnit.Nodes.Clear();
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

                        tnCategory.Nodes.Add(tnUnit);
                    }

                    tnStirps.Nodes.Add(tnCategory);
                }

                _ctlTreeUnit.Nodes.Add(tnStirps);
            }
        }

        private void BindTerrainTree()
        {
            _ctlTreeTerrain.Nodes.Clear();

            for (int i = 0; i <= 3; i++)
            {
                Terrain block = new Terrain();
                block.Value = i;
                block.Caption = "障碍" + i.ToString();

                TreeNode tnTerrain = new TreeNode();
                tnTerrain.Text = block.Caption;
                tnTerrain.Tag = block;
                _ctlTreeTerrain.Nodes.Add(tnTerrain);
            }
        }

        private void BindCampTree()
        {
            _treeCamp.Nodes.Clear();
            _listCurrentCamp.Items.Clear();

            foreach (var camp in _map.Camps)
            {
                TreeNode tnCamp = new TreeNode();
                tnCamp.Tag = camp;
                tnCamp.Text = camp.Caption;
                _treeCamp.Nodes.Add(tnCamp);

                TreeNode tnSetStartPos = new TreeNode();
                tnSetStartPos.Text = "设置开始位置";
                tnCamp.Nodes.Add(tnSetStartPos);

                TreeNode tnObjects = new TreeNode();
                tnObjects.Text = "单位列表";
                foreach (var item in camp.ObjList)
                {
                    TreeNode tnObj = new TreeNode();
                    tnObj.Text = string.Format("{0}({1})", item.Caption, item.ID);
                    tnObj.Tag = item;
                    tnObjects.Nodes.Add(tnObj);
                }
                tnCamp.Nodes.Add(tnObjects);

                _listCurrentCamp.Items.Add(camp);
            }

            _treeCamp.ExpandAll();
            _listCurrentCamp.SelectedIndex = 0;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapWindow dlg = new MapWindow();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _map = new Map2D();
                _map.ID = dlg.MapId;
                _map.Caption = dlg.MapCaption;
                _map.Row = dlg.MapRow;
                _map.Col = dlg.MapCol;
                _map.Background = dlg.Data;
                _map.Cells = new MapCell[dlg.MapRow * dlg.MapCol];

                Terrain selectedTerrain = DATUtility.GetTerrain(dlg.TerrainId);

                for (int row = 0; row < dlg.MapRow; row++)
                {
                    for (int col = 0; col < dlg.MapCol; col++)
                    {
                        MapCell cell = new MapCell();
                        cell.MapPos = new MapPos(row, col);

                        #region 设置地图单元的地形信息
                        cell.TerrainId = selectedTerrain.Id;
                        cell.TerrainIndex = 1;
                        #endregion

                        _map.Cells[row * dlg.MapCol + col] = cell;
                    }
                }

                Camp player = new Camp(1,"player");
                player.StartPos = new MapPos(0, 0);
                player.Type = CampType.Player;
                _map.Camps.Add(player);

                Camp computer = new Camp(2, "computer");
                computer.StartPos = new MapPos(0, 0);
                computer.Type = CampType.Computer;
                _map.Camps.Add(computer);

                _panel.SetMap(_map);

                BindUnitTree();
                BindCampTree();
                BindTerrainTree();
            }
        }

        private void _ctlTreeUnit_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = e.Node;
            if (selNode != null)
            {
                int unitId = Convert.ToInt32(selNode.Text);
                Unit2D unit = DATUtility.GetUnit(unitId);

                _panel.SelectUnit(DesignState.ADD_OBJECT, unit, _listCurrentCamp.SelectedItem as Camp);
            }
        }

        private void _ctlBtnPublish_Click(object sender, EventArgs e)
        {
            if (_map != null)
            {
                if (DATUtility.SaveMap(_map))
                {
                    MessageBox.Show("发布地图成功!");
                }
                else
                {
                    MessageBox.Show("发布地图失败!");
                }
            }
        }

        private void _ctlBtnOpen_Click(object sender, EventArgs e)
        {
            SelectMapWindow dlg = new SelectMapWindow();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _map = DATUtility.GetMap(dlg.SelectedMapId);
                _panel.SetMap(_map);

                BindUnitTree();
                BindCampTree();
                BindTerrainTree();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _panel.ReleaseUnit();
        }

        private void _btnSetCamp_Click(object sender, EventArgs e)
        {
            CampWindow window = new CampWindow(_map);
            window.ShowDialog();
            BindCampTree();
        }

        void _treeCamp_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent!=null && e.Node.Parent.Tag != null)
            {
                Camp camp = e.Node.Parent.Tag as Camp;
                _listCurrentCamp.SelectedItem = camp;

                int startPosId = camp.Id;
                if (startPosId > 2)
                {
                    startPosId = 2;
                }
                Model2D model = DATUtility.GetModel(startPosId);
                _panel.SelectUnit(DesignState.ADD_CAMP_STARTPOS, model, camp);
            }
            else if (e.Node.Tag != null && e.Node.Tag is Object2D)
            {
                _selectedObject = e.Node.Tag;
            }
        }

        void _ctlTreeTerrain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                Terrain terrain = e.Node.Tag as Terrain;

                Unit2D unit = null;// DATUtility.GetUnit(1);
                _panel.SelectTerrain(DesignState.SET_TERRAIN, unit, terrain);
            }
        }

        private void _listCurrentCamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            _panel.SelectCamp(_listCurrentCamp.SelectedItem as Camp);
        }

        private void _ctlBtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedObject != null)
            {
                if (_selectedObject is Object2D)
                {
                    AGSUtility.RemoveObject(_selectedObject as Object2D);
                    _selectedObject = null;
                    BindCampTree();
                }
            }
        }

        private void _ctlBtnLaunchModelWindow_Click(object sender, EventArgs e)
        {
            ModelWindow window = new ModelWindow();
            window.ShowDialog();
        }

        private void _ctlBtnLaunchUnitWindow_Click(object sender, EventArgs e)
        {
            UnitWindow window = new UnitWindow();
            window.ShowDialog();
        }

        private void _ctlBtnAutoModeling_Click(object sender, EventArgs e)
        {
            AutoModelingWindow window = new AutoModelingWindow();
            window.ShowDialog();
        }

        private void 设置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WorldMapWindow window = new WorldMapWindow();
            window.ShowDialog();
        }

        private void _ctlBtnPackage_Click(object sender, EventArgs e)
        {
            string appPath = DATUtility.GetAppPath();
            string path = DATUtility.GetResPath();
            string[] files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            Dictionary<string, string> srcFiles = new Dictionary<string, string>();
            for (int index = 0; index < files.Length; index++)
            {
                srcFiles.Add(files[index].Replace(path, string.Empty), files[index]);
            }
            PACKUtility.CompressFiles(srcFiles, appPath + "data.dat");
            MessageBox.Show("打包成功!");
        }
    }
}
