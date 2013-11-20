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
    public partial class AGEPreviewModelPanel : UserControl
    {

        List<Bitmap> _images = new List<Bitmap>();
        List<Point> _locations = new List<Point>();

        Timer _timer;
        int _index = 0;

        int _curActionId;
        int _curDirectionId;
        AGModel _model;

        AGAction _selAction;
        AGDirection _selDirection;
        int _selDirectionIndex;
        
        public AGEPreviewModelPanel()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

        public void SetModel(AGModel model)
        {
            _model = model;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (var item in _model.Actions)
            {
                comboBox1.Items.Add(item);
            }

            comboBox1.SelectedIndex = 0;

            //_curActionId = Action2DDef.GetDefs()[0].Id;
            //_curDirectionId = Direction2DDef.GetDefs()[0].Id;
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AGAction actionDef = comboBox1.SelectedItem as AGAction;
            _curActionId = actionDef.Id;

            _selAction = actionDef;
            foreach (var item in actionDef.Directions)
            {
                comboBox2.Items.Add(item);
            }
            comboBox2.SelectedIndex = _selDirectionIndex;

            Reset();
        }

        void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AGDirection directionDef = comboBox2.SelectedItem as AGDirection;
            _curDirectionId = directionDef.Id;

            _selDirection = directionDef;
            _selDirectionIndex = comboBox2.SelectedIndex;

            Reset();
        }

        private void Reset()
        {
            if (_model == null)
            {
                return;
            }

            _index = 0;
            foreach (var action in _model.Actions)
            {
                if (action.Id == _curActionId)
                {
                    foreach (var direction in action.Directions)
                    {
                        if (direction.Id == _curDirectionId)
                        {
                            _images.Clear();
                            _locations.Clear();

                            List<AGFrame> frames = direction.GetFrames();
                            foreach (var frame in frames)
                            {
                                string modelFolder = AG.Editor.Core.AGEContext.Current.EProject.GetFolder(frame.Direction.Action.Model);
                                string frameFilePath = string.Format("{0}\\{1}", modelFolder, frame.ImageFileName);
                                Bitmap bmp = new Bitmap(frameFilePath);
                                _images.Add(bmp);

                                _locations.Add(CaculateLocation(frame));
                            }
                        }
                    }
                    break;
                }
            }
        }

        protected override void  OnLoad(EventArgs e)
        {
            if (_timer == null)
            {
                Reset();
                _timer = new Timer();
                _timer.Interval = 300;
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
 	         base.OnLoad(e);
        }

        protected override void  OnHandleDestroyed(EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
 	        base.OnHandleDestroyed(e);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_images.Count > 0)
            {
                pictureBox1.Image = _images[_index];
                pictureBox1.Location = _locations[_index];
                _index++;
                if (_index >= _images.Count)
                {
                    _index = 0;
                }
            }
        }

        private Point CaculateLocation(AGFrame frame)
        {
            int cw = panel1.Width / 2;
            int ch = panel1.Height / 2;

            int oriX = cw - frame.Width / 2;
            int oriY = ch - frame.Height / 2;

            int finalX = oriX - (frame.AnchorPointX - frame.Width / 2);
            int finalY = oriY - (frame.AnchorPointY - frame.Height / 2);

            return new Point(finalX, finalY);
        }
    }
}
