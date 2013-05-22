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
    public partial class CreateUnitWindow : Form
    {
        public Unit2D Unit { get; private set; }

        public CreateUnitWindow()
        {
            InitializeComponent();

            BindCategoryList();
            BindStirpsList();
        }

        private void BindCategoryList()
        {
            _ctlListCategory.Items.Clear();
            List<UnitCategory> list = UnitCategoryDef.GetDefs();
            foreach (var item in list)
            {
                _ctlListCategory.Items.Add(item);
            }
            _ctlListCategory.SelectedIndex = 0;
        }

        private void BindStirpsList()
        {
            this.comboBox1.Items.Clear();
            List<UnitStirps> list = UnitStirps.GetDefs();
            foreach (var item in list)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectModelWindow dlg = new SelectModelWindow();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int modelId = dlg.SelectedModel.Id;

                Model2D model = DATUtility.GetModel(modelId);

                linkLabel1.Text = model.Caption;
                linkLabel1.Tag = model;
            }
        }

        private void _ctlBtnSave_Click(object sender, EventArgs e)
        {
            if (linkLabel1.Tag == null)
            {
                return;
            }

            Unit = new Unit2D();
            Unit.Id = Convert.ToInt32(textBox1.Text);
            Unit.Category = (_ctlListCategory.SelectedItem as UnitCategory);
            Unit.Stirps = (comboBox1.SelectedItem as UnitStirps);
            Unit.Caption = textBox2.Text;
            Unit.Model = (linkLabel1.Tag as Model2D);
            Unit.IconModel = (_ctlBtnIcon.Tag as Model2D);
            Unit.AttackSound = AttackSound.AtkSound1;
            Unit.Scale = Convert.ToSingle(_ctlEditScale.Text);

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void _ctlBtnIcon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectModelWindow dlg = new SelectModelWindow();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int modelId = dlg.SelectedModel.Id;

                Model2D model = DATUtility.GetModel(modelId);

                _ctlBtnIcon.Text = model.Caption;
                _ctlBtnIcon.Tag = model;
            }
        }
    }
}
