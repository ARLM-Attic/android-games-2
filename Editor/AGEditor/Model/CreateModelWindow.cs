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
    public partial class CreateModelWindow : Form
    {
        public int ModelId { get; set; }
        public string ModelCaption { get; set; }
        public ModelCategory ModelCategory { get; set; }

        public CreateModelWindow()
        {
            InitializeComponent();

            List<ModelCategory> categories = ModelCategory.GetDefs();
            foreach (var item in categories)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ModelId = Convert.ToInt32(textBox1.Text);
                ModelCaption = textBox2.Text.Trim();
                if (ModelId > 9999)
                {
                    return;
                }
                else if (string.IsNullOrEmpty(ModelCaption))
                {
                    return;
                }

                ModelCategory = comboBox1.SelectedItem as ModelCategory;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("id must be a integer!");
            }

        }
    }
}
