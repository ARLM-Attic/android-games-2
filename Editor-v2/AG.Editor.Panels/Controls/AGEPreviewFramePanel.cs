using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Data;

namespace AG.Editor.ModelUI.Controls
{
    public partial class AGEPreviewFramePanel : UserControl
    {
        private AGFrame _frame;

        public AGEPreviewFramePanel(AGFrame frame)
        {
            InitializeComponent();

            _frame = frame;

            string modelFolder = AG.Editor.Core.AGEContext.Current.EProject.GetFolder(frame.Direction.Action.Model);
            string frameFilePath = string.Format("{0}\\{1}", modelFolder, frame.ImageFileName);
            PictureBox picBox = new PictureBox();
            picBox.Image = new Bitmap(frameFilePath);
            picBox.SizeMode = PictureBoxSizeMode.Normal;

            this.Controls.Add(picBox);
        }
    }
}
