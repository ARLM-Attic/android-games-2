using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace AGEditer
{
    public partial class WorldMapDesignPanel : UserControl
    {
        Timer _timer;
        Graphics _graphics;
        Graphics _mGraphics;
        Image _mImage;

        private WorldMap _map;
        private Model2D _model;
        private int _mapId;

        Point _curPoint;

        public WorldMapDesignPanel()
        {
            InitializeComponent();
        }

        public void SetMap(WorldMap map)
        {
            _map = map;
            this.Width = _map.Model.GetFrame(1, 1, 1).Width;
            this.Height = _map.Model.GetFrame(1, 1, 1).Height;

            _mImage = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _mGraphics = Graphics.FromImage(_mImage);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = 200;
                _timer.Tick += _timer_Tick;
                _timer.Start();

            }

            base.OnLoad(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _curPoint = e.Location;

            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (_model != null)
            {
                StagesPos pos = new StagesPos();
                pos.MapId = _mapId;
                pos.Pos = new Point2D(_curPoint.X, _curPoint.Y);
                _map.StagesPosList.Add(pos);

                _model = null;
            }

            base.OnMouseClick(e);
        }

        public void SelectMap(int mapId, Model2D model)
        {
            _mapId = mapId;
            _model = model;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_map != null)
            {
                _graphics = Graphics.FromHwnd(this.Handle);

                _mGraphics.FillRectangle(Brushes.Gray, 0, 0, _mImage.Width, _mImage.Height);

                Bitmap bgImage = new Bitmap(new MemoryStream(_map.Model.GetFrame(1, 1, 1).Data));
                _mGraphics.DrawImage(bgImage, 0, 0, bgImage.Width, bgImage.Height);

                for (int index = 0; index < _map.StagesPosList.Count; index++)
                {
                    Frame2D frame = DATUtility.GetModel(14).GetFrame(0x01, 0x01, 1);

                    Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                    ImageAttributes ImgAttr = new ImageAttributes();
                    ImgAttr.SetColorKey(image.GetPixel(0, 0), image.GetPixel(0, 0));
                    Rectangle rect = new Rectangle(
                        (int)(_map.StagesPosList[index].Pos.X - frame.OffsetX),
                        (int)(_map.StagesPosList[index].Pos.Y - frame.offsetY),
                        frame.Width,
                        frame.Height);
                    _mGraphics.DrawImage(image, rect, 0, 0, frame.Width, frame.Height, GraphicsUnit.Pixel, ImgAttr);
                }

                if (_model != null)
                {
                    Frame2D frame = _model.GetFrame(0x01, 0x01, 1);

                    Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                    ImageAttributes ImgAttr = new ImageAttributes();
                    ImgAttr.SetColorKey(image.GetPixel(0, 0), image.GetPixel(0, 0));
                    Rectangle rect = new Rectangle(
                        (int)(this._curPoint.X - frame.OffsetX),
                        (int)(this._curPoint.Y - frame.offsetY),
                        frame.Width,
                        frame.Height);
                    _mGraphics.DrawImage(image, rect, 0, 0, frame.Width, frame.Height, GraphicsUnit.Pixel, ImgAttr);
                }

                ImageAttributes ia = new ImageAttributes();
                ia.SetColorKey(Color.Black, Color.Black);
                _graphics.DrawImage(_mImage, new Rectangle(0, 0, _mImage.Width, _mImage.Height), 0, 0, _mImage.Width, _mImage.Height, GraphicsUnit.Pixel, ia);
            }
        }
    }
}
