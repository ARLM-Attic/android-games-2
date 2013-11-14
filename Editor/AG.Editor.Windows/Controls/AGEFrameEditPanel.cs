using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AG.Editor.Core.Data;

namespace AG.Editor.Windows.Controls
{
    public partial class AGEFrameEditPanel : UserControl
    {
        private List<IFrameEditObserver> _observers;

        List<AGFrame> _frames;
        Dictionary<AGFrame, bool> _dictVisible;
        AGFrame _curFrame;
        List<Image> _images;
        Image _curImage;

        Point _sitePos;
        Timer _timer;
        Graphics _graphics;
        Graphics _mGraphics;
        Image _mImage;
        bool _hasResized;

        bool _isMoveFlag;
        Point _moveBeginPos;
        Point _moveBeginFrameOffset;

        public List<AGFrame> Frames
        {
            get
            {
                return _frames;
            }
        }

        public AGEFrameEditPanel()
        {
            InitializeComponent();

            _images = new List<Image>();
            _observers = new List<IFrameEditObserver>();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            _sitePos = new Point(this.Width / 2, this.Height / 2);
            _hasResized = true;

            base.OnSizeChanged(e);
        }

        public void Settings(List<AGFrame> frames, AGFrame curFrame)
        {
            _dictVisible = new Dictionary<AGFrame, bool>();
            _frames = frames;
            _curFrame = curFrame;

            _curImage = null;
            _images.Clear();

            Image curImage = null;
            foreach (var frame in _frames)
            {
                Image image = new Bitmap(AG.Editor.Core.AGEContext.Current.EProject.GetFrameFilePath(frame));
                _images.Add(image);
                if (_curFrame == frame)
                {
                    curImage = image;
                    _dictVisible.Add(frame, true);
                }
                else
                {
                    _dictVisible.Add(frame, false);
                }
            }
            _curImage = curImage;

            if (_timer == null)
            {
                _graphics = Graphics.FromHwnd(this.Handle);
                _mImage = new Bitmap(Width, Height);
                _mGraphics = Graphics.FromImage(_mImage);

                _timer = new Timer();
                _timer.Interval = 100;
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
                Debug.WriteLine("model frame edit panel timer dispose!");
            }
            base.OnHandleDestroyed(e);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _mGraphics.FillRectangle(Brushes.Black, 0, 0, this.Width, this.Height);
            
            if (_hasResized)
            {
                _mImage = new Bitmap(Width, Height);
                _mGraphics = Graphics.FromImage(_mImage);
                _hasResized = false;
            }

            if (this._curImage != null)
            {
                for (int index = 0; index < _images.Count; index++)
                {
                    AGFrame frame = _frames[index];
                    if (_dictVisible[frame])
                    {
                        var image = _images[index];
                        if (image != _curImage)
                        {
                            _mGraphics.DrawImage(image, _sitePos.X - _frames[index].AnchorPointX, _sitePos.Y - _frames[index].AnchorPointY);
                        }
                    }
                }
                _mGraphics.DrawImage(this._curImage, _sitePos.X - _curFrame.AnchorPointX, _sitePos.Y - _curFrame.AnchorPointY);
            }
            
            _mGraphics.DrawLine(Pens.Green, new Point(0, _sitePos.Y), new Point(this.Width, _sitePos.Y));
            _mGraphics.DrawLine(Pens.Green, new Point(_sitePos.X, 0), new Point(_sitePos.X, this.Height));

            _graphics.DrawImage(_mImage, 0, 0);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isMoveFlag)
            {
                _curFrame.AnchorPointX = _moveBeginFrameOffset.X - (e.X - _moveBeginPos.X);
                _curFrame.AnchorPointY = _moveBeginFrameOffset.Y - (e.Y - _moveBeginPos.Y);

                RaiseObserver();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {

            if (_curFrame!=null && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _isMoveFlag = true;
                _moveBeginPos = new Point(e.X, e.Y);
                _moveBeginFrameOffset = new Point(_curFrame.AnchorPointX, _curFrame.AnchorPointY);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_curFrame != null && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _isMoveFlag = false;
            }

            base.OnMouseUp(e);
        }

        private void RaiseObserver()
        {
            foreach (var observer in _observers)
            {
                observer.OnFrameChanged(_curFrame);
            }
        }

        #region interface
        public void SetVisible(AGFrame frame, bool visible)
        {
            _dictVisible[frame] = visible;
        }

        public void SetOffset(int AnchorPointX, int AnchorPointY)
        {
            _curFrame.AnchorPointX = AnchorPointX;
            _curFrame.AnchorPointY = AnchorPointY;
            RaiseObserver();
        }
        public Point GetOffset()
        {
            return new Point(_curFrame.AnchorPointX, _curFrame.AnchorPointY);
        }
        public void AttachObserver(IFrameEditObserver observer)
        {
            this._observers.Add(observer);
        }
        #endregion
    }
}
