using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Data;

namespace AG.Editor.ModelUI.Windows
{
    public partial class AGERemoveAudioWindow : Form
    {
        public AGAudioRef SelectedAudioRef { get; private set; }

        private AGModel _model;

        public AGERemoveAudioWindow(AGModel model)
        {
            InitializeComponent();

            _model = model;
            for (int index = 0; index < _model.AudioRefs.Count; index++)
            {
                AGAction action = model.GetAction(_model.AudioRefs[index].ActionId);
                string frameName = string.Format("{0}-{1}",
                    action.Caption,
                    _model.GetFrame(action.Id, _model.AudioRefs[index].FrameIndex).ImageFileName);
                ctlListAudio.Items.Add(frameName);
            }
        }

        private void ctlBtnOK_Click(object sender, EventArgs e)
        {
            int selectedIndex = ctlListAudio.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }

            this.SelectedAudioRef = _model.AudioRefs[selectedIndex];
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
