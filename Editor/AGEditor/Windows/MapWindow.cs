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
    public partial class MapWindow : Form
    {
        public int MapRow { get; set; }
        public int MapCol { get; set; }
        public int MapId { get; set; }
        public string MapCaption { get; set; }
        public Model2D Data { get; set; }
        public int TerrainId { get; set; }

        public MapWindow()
        {
            InitializeComponent();

            BindTerrains();

            textBox1.Text = (600 / 40).ToString();
            textBox2.Text = (800 / 40).ToString();
        }

        private void BindTerrains()
        {
            List<Terrain> terrainList = DATUtility.GetTerrains();
            for (int index = 0; index < terrainList.Count; index++)
            {
                _ctlListTerrain.Items.Add(terrainList[index]);
            }
            _ctlListTerrain.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MapRow = Convert.ToInt32(textBox1.Text);
                MapCol = Convert.ToInt32(textBox2.Text);

                MapId = Convert.ToInt32(textBox3.Text);
                MapCaption = textBox4.Text;

                TerrainId = (_ctlListTerrain.SelectedItem as Terrain).Id;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectModelWindow window = new SelectModelWindow();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Data = window.SelectedModel;
                linkLabel1.Text = window.SelectedModel.Caption;
                linkLabel1.Tag = window.SelectedModel;
            }
        }
    }
}
