using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGEditor
{
    public partial class FrameWindow : Form
    {
        public Frame2D Frame { get; set; }

        public FrameWindow()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public FrameWindow(Frame2D frame)
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            Frame = frame;
            UpdateUI();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.*|*.*|*.png|*.png";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(dlg.FileName);
                Frame = new Frame2D();
                Frame.Data = System.IO.File.ReadAllBytes(dlg.FileName);
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            //linkLabel1.Text = Frame.OrginalFile;
            //_ctlTextFile.Text = Frame.FileName;

            _ctlTextWidth.Text = Frame.Width.ToString();
            _ctlTextHeight.Text = Frame.Height.ToString();
            textBox1.Text = Frame.OffsetX.ToString();
            textBox2.Text = Frame.offsetY.ToString();

            pictureBox1.Image = new Bitmap(new MemoryStream(Frame.Data));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Frame != null)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (Frame != null)
            {
                Frame.Width = pictureBox1.Width;
                Frame.Height = pictureBox1.Height;

                UpdateUI();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Frame.OffsetX = e.X;
                Frame.offsetY = e.Y;

                UpdateUI();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X - 1, pictureBox1.Location.Y);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X + 1, pictureBox1.Location.Y);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Frame.OffsetX = Convert.ToInt32(textBox1.Text);

                UpdateUI();
            }
            catch
            {
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Frame.offsetY = Convert.ToInt32(textBox2.Text);

                UpdateUI();
            }
            catch
            {
            }
        }
    }
}
