using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditor.Windows.Common
{
    public partial class AGEInputIntegerWindow : Form
    {
        public int ReturnValue { get; private set; }

        public AGEInputIntegerWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int v = Convert.ToInt32(textBox1.Text);
                ReturnValue = v;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
            }
        }
    }
}
