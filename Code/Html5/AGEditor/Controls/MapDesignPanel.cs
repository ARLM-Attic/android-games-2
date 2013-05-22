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

        public MapDesignPanel()
        {
            InitializeComponent();
        }

        public void SetMap(Map2D map)
        {
            _map = map;

            _mImage = new Bitmap(map.Col * MapCell.Width, map.Row * MapCell.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _mGraphics = Graphics.FromImage(_mImage);

            this.Width = map.Col * MapCell.Width;
            this.Height = map.Row * MapCell.Height;
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
            MapCell cell = _map.GetCell(new MapPos(e.Location.Y / MapCell.Height, e.Location.X / MapCell.Width));
            if (cell != null)
            {
                if (_state == DesignState.ADD_OBJECT)
                {
                    AGSUtility.CreateObject(_map, _camp, _unit, "unknown", cell.MapPos, Direction2DDef.South.Id);
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

            base.OnMouseClick(e);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if(_map!=null)
            {
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

                        if (cell.Value == 0)
                        {
                            _mGraphics.DrawRectangle(Pens.Green, col * MapCell.Width, row * MapCell.Height, MapCell.Width, MapCell.Height);
                        }
                        else
                        {
                            _mGraphics.FillRectangle(Brushes.Red, col * MapCell.Width, row * MapCell.Height, MapCell.Width, MapCell.Height);
                        }
                    }
                }

                foreach (var item in _map.Widgets)
                {
                    //item.Update();
                    Model2D model = item.Unit.Model;
                    Frame2D frame = item.Unit.Model.GetFrame(item.ActionId, item.DirectionId, item.FrameIndex);

                    Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                    ImageAttributes ImgAttr = new ImageAttributes();
                    ImgAttr.SetColorKey(image.GetPixel(0, 0), image.GetPixel(0, 0));
                    Rectangle rect = new Rectangle((int)(item.SitePos.Center.X - frame.OffsetX), (int)(item.SitePos.Center.Y - frame.offsetY), frame.Width, frame.Height);
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
                    MapCell cell = _map.GetCell(new MapPos(_curPoint.Y / MapCell.Height, _curPoint.X / MapCell.Width));
                    if (cell != null)
                    {
                        Frame2D frame = _model.GetFrame(0x01, 0x01, 1);

                        Bitmap image = new Bitmap(new MemoryStream(frame.Data));
                        ImageAttributes ImgAttr = new ImageAttributes();
                        ImgAttr.SetColorKey(image.GetPixel(0,0), image.GetPixel(0,0));
                        Rectangle rect = new Rectangle((int)(cell.Center.X - frame.OffsetX), (int)(cell.Center.Y - frame.offsetY), frame.Width, frame.Height);
                        _mGraphics.DrawImage(image, rect, 0, 0, frame.Width, frame.Height, GraphicsUnit.Pixel, ImgAttr);
                    }
                }

                ImageAttributes ia = new ImageAttributes();
                ia.SetColorKey(Color.Black, Color.Black);
                _graphics.DrawImage(_mImage, new Rectangle(0, 0, _mImage.Width, _mImage.Height), 0, 0, _mImage.Width, _mImage.Height, GraphicsUnit.Pixel, ia);
            }
        }
    }
}
