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
        public byte[] Data { get; set; }

        public MapWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MapRow = Convert.ToInt32(textBox1.Text);
                MapCol = Convert.ToInt32(textBox2.Text);

                MapId = Convert.ToInt32(textBox3.Text);
                MapCaption = textBox4.Text;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.bmp|*.bmp";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Data = System.IO.File.ReadAllBytes(dlg.FileName);
                linkLabel1.Text = dlg.FileName;
            }
        }
    }
}
