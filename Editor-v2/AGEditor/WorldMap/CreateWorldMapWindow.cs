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
    public partial class CreateWorldMapWindow : Form
    {
        public global::WorldMap WorldMap { get; set; }

        public CreateWorldMapWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorldMap = new WorldMap();
            WorldMap.Id = Convert.ToInt32(textBox1.Text);
            WorldMap.Caption = textBox2.Text;
            WorldMap.Model = (linkLabel1.Tag as Model2D);

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectModelWindow window = new SelectModelWindow();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var action in window.SelectedModel.Actions)
                {
                    foreach (var direction in action.Directions)
                    {
                        foreach (var frame in direction.Frames)
                        {
                            frame.Data = ResourceLoader.GetFrameData(window.SelectedModel.Id, action.Id, direction.Id, frame.Index);
                        }
                    }
                }

                linkLabel1.Text = (window.SelectedModel.Caption);
                linkLabel1.Tag = window.SelectedModel;
            }
        }
    }
}
