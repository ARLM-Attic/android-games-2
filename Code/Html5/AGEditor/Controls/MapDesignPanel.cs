using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace AGEditer
{
    public partial class MapDesignPanel : UserControl
    {
        Timer _timer;
        Graphics _graphics;
        Graphics _mGraphics;
        Image _mImage;

        Map2D _map;
        Model2D _model;
        Unit2D _unit;
        Camp _camp;
        Terrain _terrain;
        Point _curPoint;

        private DesignState _state;

        private float _zeroY;

        public MapDesignPanel()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

           
        }

        public void SetMap(Map2D map)
        {
            _map = map;


            this.Width = (int)MapCoordinate.CalculateSize(_map.Row, _map.Col).Width;
            this.Height = (int)MapCoordinate.CalculateSize(_map.Row, _map.Col).Height;

            _mImage = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _mGraphics = Graphics.FromImage(_mImage);

            _zeroY = _map.Col * MapCell.Height / 2;
        }

        public void SelectCamp(Camp camp)
        {
            _camp = camp;
        }

        public void SelectUnit(DesignState state, Unit2D unit, Camp camp)
        {
            _state = state;
            _unit = unit;
            _model = _unit.Model;
            _camp = camp;
        }

        public void SelectUnit(DesignState state, Model2D model, Camp camp)
        {
            _state = state;
            _model = model;
            _camp = camp;
        }

        public void SelectTerrain(DesignState state, Unit2D unit, Terrain terrain)
        {
            _state = state;
            _unit = unit;
            _terrain = terrain;
        }

        public void ReleaseUnit()
        {
            _unit = null;
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
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                return;
            }

            Point2D pt = new Point2D(e.Location.X, e.Location.Y);
            pt.Y -= this._zeroY;
            MapPos pos = MapCoordinate.MapPtToPos(pt);
            MapCell cell = _map.GetCell(pos);
            if (cell != null)
            {
                if (_state == DesignState.ADD_OBJECT)
                {
                    AGSUtility.CreateObject(_map, _camp, _unit, "unknown", new Point2D(pt.X, pt.Y), cell.MapPos, Direction2DDef.South.Id);
                }
                else if (_state == DesignState.ADD_CAMP_STARTPOS)
                {
                    AGSUtility.SetStartPos(_camp, cell.MapPos);
                }
                else if (_state == DesignState.SET_TERRAIN)
                {
                    AGSUtility.SetTerrain(_map, cell.MapPos, _terrain);
                }
            }

            //base.OnMouseClick(e);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if(_map!=null)
            {
                int offsetX = (this.Parent as Panel).HorizontalScroll.Value;
                int offsetY = (this.Parent as Panel).VerticalScroll.Value;

                _graphics = Graphics.FromHwnd(this.Handle);

                _mGraphics.FillRectangle(Brushes.Gray, 0, 0, _mImage.Width, _mImage.Height);

                if (_map.Background != null)
                {
                    Bitmap bgImage = new Bitmap(new MemoryStream(_map.Background.GetFrame(1,1,1).Data));
                    _mGraphics.DrawImage(bgImage, 0, 0, bgImage.Width, bgImage.Height);
                }

                for (int row = 0; row < _map.Row; row++)
                {
                    for (int col = 0; col < _map.Col; col++)
                    {
                        MapCell cell = _map.GetCell(new MapPos(row, col));
                        if (_map.Background == null)
                        {
                            //Terrain terrain = DATUtility.GetTerrain(cell.TerrainId);
                            //Frame2D frame = terrain.Model.GetFrame(1, 1, cell.TerrainIndex);
                            //Bitmap terrainImage = new Bitmap(new MemoryStream(frame.Data));
                            //_mGraphics.DrawImage(terrainImage, col * MapCell.Width, row * MapCell.Height, MapCell.Width, MapCell.Height);
                        }

                        Point2D pt = MapCoordinate.PosToMapPt(new MapPos(row, col));
                        pt.Y = pt.Y + this._zeroY;

                        pt.X -= offsetX;
                        pt.Y -= offsetY;
                        if (cell.Type == 0)
                        {
                            Point[] pts = new Point[4];
                            pts[0] = new Point((int)pt.X, (int)pt.Y);
                            pts[1] = new Point((int)pt.X + MapCell.Width / 2, (int)pt.Y - MapCell.Height / 2);
                            pts[2] = new Point((int)pt.X + MapCell.Width, (int)pt.Y);
                            pts[3] = new Point((int)pt.X + MapCell.Width/2, (int)pt.Y + MapCell.Height / 2);
                            _mGraphics.DrawPolygon(Pens.Green, pts);
                        }
                        else
                        {
                            Point[] pts = new Point[4];
                            pts[0] = new Point((int)pt.X, (int)pt.Y);
                            pts[1] = new Point((int)pt.X + MapCell.Width / 2, (int)pt.Y - MapCell.Height / 2);
                            pts[2] = new Point((int)pt.X + MapCell.Width, (int)pt.Y);
                            pts[3] = new Point((int)pt.X + MapCell.Width / 2, (int)pt.Y + MapCell.Height / 2);
                            _mGraphics.DrawPolygon(Pens.Red, pts);
                        }
                    }
                }

                foreach (var item in _map.Widgets)
                {
                    //item.Update();
                    Model2D model = item.Unit.Model;
                    Frame2D frame = item.Unit.Model.GetFrame(item.ActionId, item.DirectionId, item.FrameIndex);

                    int x = (int)(item.CurrentPoint.X - offsetX);
                    int y = (int)(item.CurrentPoint.Y + this._zeroY - offsetY);

                    Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                    ImageAttributes ImgAttr = new ImageAttributes();
                    ImgAttr.SetColorKey(image.GetPixel(0, 0), image.GetPixel(0, 0));
                    Rectangle rect = new Rectangle(
                        (int)(x - frame.OffsetX),
                        (int)(y - frame.offsetY),
                        frame.Width, 
                        frame.Height);
                    _mGraphics.DrawImage(image, rect, 0, 0, frame.Width, frame.Height, GraphicsUnit.Pixel, ImgAttr);
                }

                foreach (var camp in _map.Camps)
                {
                    if (camp.StartPos != null)
                    {
                        Model2D model = DATUtility.GetModel(camp.Id);
                        Frame2D frame = model.GetFrame(Action2DDef.Stand.Id, Direction2DDef.South.Id, 0x01);

                        Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                        ImageAttributes ImgAttr = new ImageAttributes();
                        ImgAttr.SetColorKey(image.GetPixel(0, 0), image.GetPixel(0, 0));
                        Rectangle rect = new Rectangle((int)(camp.StartPos.Center.X - frame.OffsetX), (int)(camp.StartPos.Center.Y - frame.offsetY), frame.Width, frame.Height);
                        _mGraphics.DrawImage(image, rect, 0, 0, frame.Width, frame.Height, GraphicsUnit.Pixel, ImgAttr);
                    }
                }

                if (_model != null)
                {

                    Point2D pt = new Point2D(_curPoint.X, _curPoint.Y);
                    pt.Y -= this._zeroY;

                    MapPos pos = MapCoordinate.MapPtToPos(pt);
                    MapCell cell = _map.GetCell(pos);
                    if (cell != null)
                    {
                        Frame2D frame = _model.GetFrame(0x01, 0x01, 1);

                        Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                        ImageAttributes ImgAttr = new ImageAttributes();
                        ImgAttr.SetColorKey(image.GetPixel(0,0), image.GetPixel(0,0));

                        pt.X -= offsetX;
                        pt.Y += this._zeroY;
                        pt.Y -= offsetY;
                        Rectangle rect = new Rectangle(
                            (int)(pt.X - frame.OffsetX),
                            (int)(pt.Y - frame.offsetY),
                            frame.Width, 
                            frame.Height);
                        _mGraphics.DrawImage(image, rect, 0, 0, frame.Width, frame.Height, GraphicsUnit.Pixel, ImgAttr);
                    }
                }

                _mGraphics.DrawString((this.Parent as Panel).VerticalScroll.Value.ToString(), DefaultFont, Brushes.Red, 800, 440);
                _mGraphics.DrawString((this.Parent as Panel).HorizontalScroll.Value.ToString(), DefaultFont, Brushes.Red, 800, 480);

                ImageAttributes ia = new ImageAttributes();
                ia.SetColorKey(Color.Black, Color.Black);
                _graphics.DrawImage(_mImage,
                    new Rectangle((this.Parent as Panel).HorizontalScroll.Value, (this.Parent as Panel).VerticalScroll.Value, _mImage.Width, _mImage.Height),
                    0, 0, _mImage.Width, _mImage.Height, GraphicsUnit.Pixel, ia);
            }
        }
    }
}
