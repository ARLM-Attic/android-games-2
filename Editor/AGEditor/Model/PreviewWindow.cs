using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGEditer
{
    public partial class PreviewWindow : Form
    {
        List<Bitmap> _images = new List<Bitmap>();
        List<Point> _locations = new List<Point>();

        Timer _timer;
        int _index = 0;

        int _curActionId;
        int _curDirectionId;
        Model2D _model;
        public PreviewWindow(Model2D model)
        {
            InitializeComponent();

            _model = model;

            foreach (var item in Action2DDef.GetDefs())
            {
                comboBox1.Items.Add(item);
            }

            foreach (var item in Direction2DDef.GetDefs())
            {
                comboBox2.Items.Add(item);
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            _curActionId = Action2DDef.GetDefs()[0].Id;
            _curDirectionId = Direction2DDef.GetDefs()[0].Id;

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;


            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

        public PreviewWindow(Model2D model, Action2D action)
        {
            InitializeComponent();

            _model = model;

            foreach (var item in Action2DDef.GetDefs())
            {
                int index = comboBox1.Items.Add(item);
                if (item.Id == action.Id)
                {
                    comboBox1.SelectedItem = item;
                }
            }

            foreach (var item in Direction2DDef.GetDefs())
            {
                comboBox2.Items.Add(item);
            }

            comboBox2.SelectedIndex = 0;

            _curActionId = action.Id;
            _curDirectionId = Direction2DDef.GetDefs()[0].Id;

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

        public PreviewWindow(Model2D model, Action2D action, Direction2D direction)
        {
            InitializeComponent();

            _model = model;

            foreach (var item in Action2DDef.GetDefs())
            {
                int index = comboBox1.Items.Add(item);
                if (item.Id == action.Id)
                {
                    comboBox1.SelectedItem = item;
                }
            }

            foreach (var item in Direction2DDef.GetDefs())
            {
                comboBox2.Items.Add(item);
                if (item.Id == direction.Id)
                {
                    comboBox2.SelectedItem = item;
                }
            }

            _curActionId = action.Id;
            _curDirectionId = direction.Id;

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Action2DDef actionDef = comboBox1.SelectedItem as Action2DDef;
            _curActionId = actionDef.Id;

            Reset();
        }

        void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Direction2DDef directionDef = comboBox2.SelectedItem as Direction2DDef;
            _curDirectionId = directionDef.Id;

            Reset();
        }

        private void Reset()
        {
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
                            foreach (var frame in direction.Frames)
                            {
                                Bitmap bmp = new Bitmap(new System.IO.MemoryStream(frame.Data));
                                _images.Add(bmp);

                                _locations.Add(CaculateLocation(frame));
                            }
                        }
                    }
                    break;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            if (_timer == null)
            {
                Reset();
                _timer = new Timer();
                _timer.Interval = 300;
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }

            base.OnShown(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
            }

            base.OnClosing(e);
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

        private Point CaculateLocation(Frame2D frame)
        {
            int cw = panel1.Width / 2;
            int ch = panel1.Height / 2;

            int oriX = cw - frame.Width / 2;
            int oriY = ch - frame.Height / 2;

            int finalX = oriX - (frame.OffsetX - frame.Width / 2);
            int finalY = oriY - (frame.offsetY - frame.Height / 2);

            return new Point(finalX, finalY);
        }
    }
}
