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
    public partial class CampWindow : Form
    {
        private Map2D _map;

        public CampWindow(Map2D map)
        {
            InitializeComponent();

            _map = map;
            BindCamps();
        }

        private void BindCamps()
        {
            listView1.Items.Clear();
            for (int index = 0; index < _map.Camps.Count; index++)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = _map.Camps[index];
                item.Text = _map.Camps[index].Id.ToString();
                item.SubItems.Add(_map.Camps[index].Caption);
                listView1.Items.Add(item);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CreateCampWindow window = new CreateCampWindow();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _map.Camps.Add(window.Camp);
                BindCamps();
            }
        }
    }
}
