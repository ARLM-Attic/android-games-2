using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Windows
{
    public partial class AGESelectModelCategoryWindow : Form
    {
        public AGESelectModelCategoryWindow(List<AGModelCategory> modelCategories)
        {
            InitializeComponent();

            ctlList.DisplayMember = "Name";
            ctlList.DataSource = modelCategories;
        }

        private void ctlBtnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
