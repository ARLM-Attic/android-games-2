using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Metadata;
using AG.Editor.Core.Data;

namespace AG.Editor.Windows
{
    public partial class AGECreateModelWindow : Form
    {
        public AGModel CreatedModel { get; set; }

        public AGECreateModelWindow()
        {
            InitializeComponent();

            ctlListCategory.DisplayMember = "Caption";
            ctlListCategory.DataSource = AG.Editor.Core.AGEContext.Current.EProject.TProject.ModelCategories;
        }

        private void ctlBtnOK_Click(object sender, EventArgs e)
        {
            AGModelCategory selectedItem = ctlListCategory.SelectedItem as AGModelCategory;
            if (selectedItem == null)
            {
                return;
            }

            int id = 0;
            if (!int.TryParse(ctlEditId.Text, out id))
            {
                return;
            }

            AGModel model = new AGModel();
            model.Id = id;
            model.Caption = ctlEditCaption.Text;
            model.CategoryId = selectedItem.Id;
            model.Category = selectedItem;

            foreach (var actionItem in selectedItem.Actions)
            {
                AGAction action = new AGAction();
                action.Id = actionItem.Id;
                action.Caption = actionItem.Caption;

                foreach (var dirItem in actionItem.Directions)
                {
                    AGDirection direction = new AGDirection();
                    direction.Id = dirItem.Id;
                    direction.Caption = dirItem.Caption;
                    action.AddDirection(direction);
                }

                model.AddAction(action);
            }

            CreatedModel = model;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
