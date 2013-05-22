using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AGWorldMapPanel : AGContainer
{
    private WorldMap _worldMap;

    public Model2D Model { get; set; }
    private Model2D FrameModel { get; set; }

    public event Action<MapInfo> SelectMap;

    private bool _isMoving = false;
    private int _mapWidth;
    private int _mapHeight;
    private float _viewX;
    private float _viewY;

    #region drag
    private Point2D _storedMousePos;
    private Point2D _storedPos;
    #endregion

    public AGWorldMapPanel(int worldMapId, Point2D pos, Size2D size)
    {
        _worldMap = DATUtility.GetWorldMap(worldMapId);
        Model = _worldMap.Model;
        FrameModel = DATUtility.GetModel(16);

        Frame2D frame = Model.GetFrame(0x01, 0x01, 0x01);
        _mapWidth = frame.Width;
        _mapHeight = frame.Height;

        Pos = new Point2D(pos.X, pos.Y);
        Size = size;

        if (_mapWidth < Size.W)
        {
            Pos.X = (Size.W - _mapWidth) / 2;
            Size.W = _mapWidth;
        }
        if (_mapHeight < Size.H)
        {
            Size.H = _mapHeight;
        }

        _viewX = 0;
        _viewY = _mapHeight - Size.H;

        for (int mapIndex = 0; mapIndex < _worldMap.StagesPosList.Count; mapIndex++)
        {
            MapInfo info = DATUtility.GetMapInfo(_worldMap.StagesPosList[mapIndex].MapId);
            AGStageMarker button = new AGStageMarker(info,
                new Point2D(_worldMap.StagesPosList[mapIndex].Pos.X - _viewX, _worldMap.StagesPosList[mapIndex].Pos.Y - _viewY));
            button.Click += new EventHandler(button_Click);

            this.AddChildren(button);
        }
    }

    void button_Click(object sender, EventArgs e)
    {
        if (SelectMap != null)
        {
            AGStageMarker button = sender as AGStageMarker;
            SelectMap(button.Stage);
        }
    }

    protected override void OnRender(IGDI gdi)
    {
        Frame2D frame = Model.GetFrame(0x01, 0x01, 0x01);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)),
            Pos.X,
            Pos.Y,
            Size.W,
            Size.H,
            _viewX,
            _viewY,
            Size.W,
            Size.H);

        base.OnRender(gdi);

        Frame2D topFrame = FrameModel.GetFrame(0x01, 1, 1);
        Frame2D bottomFrame = FrameModel.GetFrame(0x01, 1, 2);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(topFrame.Data)), 0, 0, 800, topFrame.Height, topFrame.Width, topFrame.Height);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(bottomFrame.Data)), 0, 550, 800, bottomFrame.Height, bottomFrame.Width, bottomFrame.Height);
    }

    public override bool OnInputEvent(MouseMessage mouse)
    {
        if (base.OnInputEvent(mouse))
        {
            return true;
        }

        if (mouse.IsLBDown())
        {
            if (!_isMoving)
            {
                _storedMousePos = new Point2D(mouse.X, mouse.Y);
                _storedPos = new Point2D(_viewX, _viewY);
                _isMoving = true;
            }
            else
            {
                float deltaX = mouse.X - _storedMousePos.X;
                float deltaY = mouse.Y - _storedMousePos.Y;

                _viewX -= deltaX;
                _viewY -= deltaY;

                if (_viewX <= 0)
                {
                    _viewX = 0;
                }
                else if (_viewX > _mapWidth - Size.W)
                {
                    _viewX = _mapWidth - Size.W;
                }

                if (_viewY < 0)
                {
                    _viewY = 0;
                }
                else if (_viewY > _mapHeight - Size.H)
                {
                    _viewY = _mapHeight - Size.H;
                }


                for (int mapIndex = 0; mapIndex < _worldMap.StagesPosList.Count; mapIndex++)
                {
                    AGStageMarker button = this.Controls[mapIndex] as AGStageMarker;

                    button.SetMapPos(new Point2D(
                        _worldMap.StagesPosList[mapIndex].Pos.X - _viewX,
                        _worldMap.StagesPosList[mapIndex].Pos.Y - _viewY));

                }

                _storedMousePos = new Point2D(mouse.X, mouse.Y);
                _storedPos = new Point2D(_viewX, _viewY);
            }
        }
        else
        {
            _isMoving = false;
        }
        return true;
    }
}