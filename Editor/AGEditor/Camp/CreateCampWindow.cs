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
    public partial class CreateCampWindow : Form
    {
        public Camp Camp { get; private set; }

        public CreateCampWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Camp = new Camp();
            Camp.Id = Convert.ToInt32(textBox1.Text);
            Camp.Caption = textBox2.Text;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
