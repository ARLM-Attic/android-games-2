using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BigMapPanel : AGControl
{
    private int[] _xarr = new int[] { 127, 86, 143, 107, 320 };
    private int[] _yarr = new int[] { 530, 458, 366, 294, 220 };
    private int[] _maparr = new int[] { 100, 101, 102, 100, 100 };

    public Model2D Model { get; set; }
    private Model2D FrameModel { get; set; }

    private List<AGControl> _controls;

    public event Action<int> SelectMap;

    private bool _isMoving = false;
    private int _mapVY;
    private int _mapWidth;
    private int _mapHeight;

    public BigMapPanel(Model2D model, Point2D pos, Size2D size)
    {
        Model = model;
        FrameModel = DATUtility.GetModel(16);

        _controls = new List<AGControl>();

        List<int> maps = DATUtility.GetMaps();
        for (int mapIndex = 0; mapIndex < _xarr.Length; mapIndex++)
        {
            AGStageMarker button = new AGStageMarker(
                _maparr[mapIndex],
                _maparr[mapIndex].ToString(),
                new Point2D(_xarr[mapIndex], _yarr[mapIndex]),
                new Size2D(50, 150));
            button.Click += new EventHandler(button_Click);
            _controls.Add(button);
            button.Parent = this;
        }

        Frame2D frame = Model.GetFrame(0x01, 0x01, 0x01);
        _mapWidth = frame.Width;
        _mapHeight = frame.Height;
        Pos = pos;
        Size = size;
    }

    void button_Click(object sender, EventArgs e)
    {
        if (SelectMap != null)
        {
            AGStageMarker button = sender as AGStageMarker;
            SelectMap(button.MapId);
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
            0,
            -_mapVY,
            frame.Width,
            Size.H);

        for (int ctlIndex = 0; ctlIndex < _controls.Count; ctlIndex++)
        {
            _controls[ctlIndex].Pos.X = _xarr[ctlIndex];
            _controls[ctlIndex].Pos.Y = _yarr[ctlIndex] + _mapVY;
        }

        for (int ctlIndex = 0; ctlIndex < _controls.Count; ctlIndex++)
        {
            _controls[ctlIndex].Render(gdi);
        }

        Frame2D topFrame = FrameModel.GetFrame(0x01, 1, 1);
        Frame2D bottomFrame = FrameModel.GetFrame(0x01, 1, 2);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(topFrame.Data)), 0, 0, topFrame.Width, topFrame.Height, topFrame.Width, topFrame.Height);
        gdi.DrawImage(new System.Drawing.Bitmap(new System.IO.MemoryStream(bottomFrame.Data)), 0, this.Pos.Y + this.Size.H, bottomFrame.Width, bottomFrame.Height, bottomFrame.Width, bottomFrame.Height);
    }

    public override bool OnInputEvent(MouseMessage mouse)
    {
        for (int ctlIndex = 0; ctlIndex < _controls.Count; ctlIndex++)
        {
            if (_controls[ctlIndex].InRect(mouse.X, mouse.Y))
            {
                if (_controls[ctlIndex].OnInputEvent(mouse))
                {
                    return true;
                }
            }
        }

        if (mouse.IsLBDown())
        {
            if (_isMoving)
            {
                if (Math.Abs(mouse.DeltaY) > 20)
                {
                    _mapVY += mouse.DeltaY % 10;
                }
                else
                {
                    _mapVY += mouse.DeltaY;
                }

                if (_mapVY < -(Model.GetFrame(0x01, 0x01, 0x01).Height - Size.H))
                {
                    _mapVY = -(Model.GetFrame(0x01, 0x01, 0x01).Height - (int)Size.H);
                }
                else if (_mapVY > 0)
                {
                    _mapVY = 0;
                }
            }
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        return true;
    }
}