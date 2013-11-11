using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGEditor.Windows.Model;

namespace AGEditor
{
    public partial class MainWindow2 : Form
    {
        public MainWindow2()
        {
            InitializeComponent();

            InitStatusbar();

            this._ctlBtnModelList.Text = AGEContext.Current.GetModelCount().ToString();
        }

        private void InitStatusbar()
        {
            this._ctlTxtProjName.Text = AGEContext.Current.Config.Workspace.Name;
            this._ctlTxtProjName.ToolTipText = AGEContext.Current.Config.Workspace.Path;
            this._ctlTxtProjName.AutoToolTip = true;
        }

        private void _ctlBtnSwitchModel_Click(object sender, EventArgs e)
        {
            ModelListWindow window = new ModelListWindow();
            window.ShowDialog();
        }

        private void _ctlBtnModelList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModelListWindow window = new ModelListWindow();
            window.ShowDialog();
        }
    }
}
