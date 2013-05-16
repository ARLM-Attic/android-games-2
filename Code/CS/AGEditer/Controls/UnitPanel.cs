using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditer
{
    public partial class UnitPanel : UserControl
    {
        public Unit2D Unit { get; private set; }

        public UnitPanel()
        {
            InitializeComponent();
        }

        public void SetUnit(Unit2D unit)
        {
            Unit = unit;
            BindCategoryList();
            BindStirpsList();
            BindUnit();
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

        private void BindUnit()
        {
            _ctlEditId.Text = Unit.Id.ToString();
            _ctlEditCaption.Text = Unit.Caption;
            foreach (var item in _ctlListCategory.Items)
            {
                if ((item as UnitCategory).Id == Unit.Category.Id)
                {
                    _ctlListCategory.SelectedItem = item;
                    break;
                }
            }
            foreach (var item in comboBox1.Items)
            {
                if ((item as UnitStirps).Id == Unit.Stirps.Id)
                {
                    comboBox1.SelectedItem = item;
                    break;
                }
            }
            _ctlBtnModel.Text = Unit.Model.Caption;
            _ctlBtnModel.Tag = Unit.Model;

            this._ctlBtnIcon.Text = Unit.IconModel.Caption;
            this._ctlBtnIcon.Tag = Unit.IconModel;

            _ctlEditScale.Text = Unit.Scale.ToString();

            _ctlEditHP.Text = Unit.MaxHP.ToString();
            _ctlEditMP.Text = Unit.MaxMP.ToString();
            _ctlEditMSpeed.Text = Unit.MSpeed.ToString();
            _ctlEditAD.Text = Unit.AD.ToString();
            this._ctlEditASpeed.Text = Unit.ADSpeed.ToString();
            _ctlEditADDEF.Text = Unit.ADDEF.ToString();

            _ctlEditCostM.Text = Unit.CostM.ToString();
            _ctlEditCostP.Text = Unit.CostP.ToString();
            _ctlEditSize.Text = Unit.Size.ToString();

            _ctlEditCritProbability.Text = Unit.CritProbability.ToString();
            _ctlEditDefProbability.Text = Unit.DefProbability.ToString();

            _ctlEditBuildCD.Text = Unit.BuildCoolDown.ToString();
        }

        private void _ctlBtnSave_Click(object sender, EventArgs e)
        {
            Unit.Id = Convert.ToInt32(_ctlEditId.Text);
            Unit.Caption = _ctlEditCaption.Text;
            Unit.Category = (_ctlListCategory.SelectedItem as UnitCategory);
            Unit.Stirps = (comboBox1.SelectedItem as UnitStirps);
            Unit.Model = (_ctlBtnModel.Tag as Model2D);
            Unit.IconModel = (this._ctlBtnIcon.Tag as Model2D);
            Unit.Scale = Convert.ToSingle(_ctlEditScale.Text);

            Unit.MaxHP = Convert.ToInt32(_ctlEditHP.Text);
            Unit.MaxMP = Convert.ToInt32(_ctlEditMP.Text);
            Unit.MSpeed = Convert.ToInt32(_ctlEditMSpeed.Text);
            Unit.AD = Convert.ToInt32(_ctlEditAD.Text);
            Unit.ADSpeed = Convert.ToInt32(_ctlEditASpeed.Text);
            Unit.ADDEF = Convert.ToInt32(_ctlEditADDEF.Text);

            Unit.CostM = Convert.ToInt32(_ctlEditCostM.Text);
            Unit.CostP = Convert.ToInt32(_ctlEditCostP.Text);
            Unit.Size = Convert.ToInt32(_ctlEditSize.Text);

            Unit.CritProbability = Convert.ToInt32(this._ctlEditCritProbability.Text);
            Unit.DefProbability = Convert.ToInt32(this._ctlEditDefProbability.Text);

            Unit.BuildCoolDown = Convert.ToInt32(this._ctlEditBuildCD.Text);

            if (DATUtility.SaveUnit(Unit))
            {
                MessageBox.Show("save unit success!");
            }
            else
            {
                MessageBox.Show("save unit failure!");
            }
        }

        private void _ctlBtnIcon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectModelWindow window = new SelectModelWindow();
            if (window.ShowDialog() == DialogResult.OK)
            {
                Unit.IconModel = window.SelectedModel;
                this._ctlBtnIcon.Text = Unit.IconModel.Caption;
                this._ctlBtnIcon.Tag = Unit.IconModel;
            }
        }

        private void _ctlBtnModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectModelWindow window = new SelectModelWindow();
            if (window.ShowDialog() == DialogResult.OK)
            {
                Unit.Model = window.SelectedModel;
                this._ctlBtnModel.Text = Unit.Model.Caption;
                this._ctlBtnModel.Tag = Unit.Model;
            }
        }
    }
}
