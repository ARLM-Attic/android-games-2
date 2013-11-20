using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Camera
{
    /// <summary>
    /// 摄像机范围顶点在窗口坐标系中的位置
    /// </summary>
    public Rect2D RectInWin
    {
        get
        {
            return new Rect2D(
                CenterPos.X - Width / 2,
                CenterPos.Y - Height / 2,
                Width,
                Height);
        }
    }

    /// <summary>
    /// 摄像机范围顶点在地图坐标系上的坐标信息
    /// </summary>
    public Rect2D RectInMap { get; set; }

    public float Width { get; set; }

    public float Height { get; set; }

    // 摄像机中心所在的位置，相对于地图的位置，
    public Point2D CenterTargetPos { get; set; }

    // 中心位于窗口的位置
    public Point2D CenterPos { get; set; }

    /// <summary>
    /// 摄像机的高度，越高可看见的面积越大
    /// </summary>
    public float ZDistance { get; set; }

    public float Zoom
    {
        get;
        private set;
    }

    public Size2D MapSize
    {
        get
        {
            return new Size2D(_map.Col * MapCell.Width * Zoom, _map.Row * MapCell.Height * Zoom);
        }
    }

    private Map2D _map;

    private int _speedX = 16;

    public Point2D ZeroPoint { get; set; }

    public Camera(float width, float height, Point2D centerPos)
    {
        Zoom = 1;
        ZDistance = 1;

        Width = width;
        Height = height;
        CenterPos = centerPos;
        ZeroPoint = new Point2D(0.0f, 0.0f);
    }

    public void Attach(Map2D map, Point2D centerTargetPos)
    {
        _map = map;

        CenterTargetPos = centerTargetPos;
        UpdateCoordinateSystem();
    }

    public void Target(MapPos pos)
    {
        CenterTargetPos = pos.Center;

        UpdateCoordinateSystem();
    }

    public void Far()
    {
        ZDistance = ZDistance + 0.1f;
        if (ZDistance > 1.5d)
        {
            ZDistance = 1.5f;
        }
        SetZoom(2 - ZDistance);
    }

    public void Near()
    {
        ZDistance = ZDistance - 0.1f;
        if (ZDistance < 0)
        {
            ZDistance = 0;
        }
        SetZoom(2 - ZDistance);
    }

    public void SetZoom(float zoom)
    {
        float lastZoom = Zoom;
        Zoom = zoom;
        float newTargetPosX = this.CenterTargetPos.X * Zoom / lastZoom;
        float newTargetPosY = this.CenterTargetPos.Y * Zoom / lastZoom;
        SetCenterTargetPos(new Point2D(newTargetPosX, newTargetPosY));
    }

    public void MoveUp()
    {
        CenterTargetPos = new Point2D(CenterTargetPos.X, CenterTargetPos.Y - MapCell.Height * Zoom / _speedX);
        //if (CenterTargetPos.Y < _m / 2)
        //{
        //    CenterTargetPos = new Point(CenterTargetPos.X, this.Height / 2);
        //}

        UpdateCoordinateSystem();
    }

    public void MoveDown()
    {
        CenterTargetPos = new Point2D(CenterTargetPos.X, CenterTargetPos.Y + MapCell.Height * Zoom / _speedX);
        //if (CenterTargetPos.Y > _map.Size.Height - this.Height / 2)
        //{
        //    CenterTargetPos = new Point(CenterTargetPos.X, _map.Size.Height - this.Height / 2);
        //}
        UpdateCoordinateSystem();
    }

    public void MoveLeft()
    {
        CenterTargetPos = new Point2D(CenterTargetPos.X - MapCell.Width * Zoom / _speedX, CenterTargetPos.Y);
        //if (CenterTargetPos.X < this.Width / 2)
        //{
        //    CenterTargetPos = new Point(this.Width / 2, CenterTargetPos.Y);
        //}
        UpdateCoordinateSystem();
    }

    public void MoveRight()
    {
        CenterTargetPos = new Point2D(CenterTargetPos.X + MapCell.Width * Zoom / _speedX, CenterTargetPos.Y);
        //if (CenterTargetPos.X > _map.Size.Width - this.Width / 2)
        //{
        //    CenterTargetPos = new Point(_map.Size.Width - this.Width / 2, CenterTargetPos.Y);
        //}
        UpdateCoordinateSystem();
    }

    public void Move(float dx, float dy)
    {
        CenterTargetPos = new Point2D(CenterTargetPos.X + dx, CenterTargetPos.Y + dy);
        //if (CenterTargetPos.X > _map.Size.Width - this.Width / 2)
        //{
        //    CenterTargetPos = new Point(_map.Size.Width - this.Width / 2, CenterTargetPos.Y);
        //}
        UpdateCoordinateSystem();
    }


    public void MoveTo(float x, float y)
    {
        CenterTargetPos = new Point2D(x, y);
        //if (CenterTargetPos.X > _map.Size.Width - this.Width / 2)
        //{
        //    CenterTargetPos = new Point(_map.Size.Width - this.Width / 2, CenterTargetPos.Y);
        //}
        UpdateCoordinateSystem();
    }

    public void SetCenterTargetPos(Point2D pos)
    {
        CenterTargetPos = new Point2D(pos.X, pos.Y);
        UpdateCoordinateSystem();
    }

    // 更新坐标系
    internal void UpdateCoordinateSystem()
    {
        ValidateRange();

        // 计算新的原点位置
        ZeroPoint = new Point2D(
            this.CenterPos.X - this.CenterTargetPos.X,
            this.CenterPos.Y - this.CenterTargetPos.Y);

        RectInMap = new Rect2D(
            this.CenterTargetPos.X - Width / 2,
            this.CenterTargetPos.Y - Height / 2,
            Width,
            Height);
    }

    private void ValidateRange()
    {
        if (CenterTargetPos.X < Width / 2)
        {
            CenterTargetPos.X += Width / 2 - CenterTargetPos.X;
        }
        else if (MapSize.W - CenterTargetPos.X < Width / 2)
        {
            CenterTargetPos.X -= Width / 2 - (MapSize.W - CenterTargetPos.X);
        }

        if (CenterTargetPos.Y < Height / 2)
        {
            CenterTargetPos.Y += Height / 2 - CenterTargetPos.Y;
        }
        else if (MapSize.H - CenterTargetPos.Y < Height / 2)
        {
            CenterTargetPos.Y -= Height / 2 - (MapSize.H - CenterTargetPos.Y);
        }
    }

    /// <summary>
    /// 将地图坐标转换为位置信息
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public MapPos GetMapPos(float x, float y)
    {
        float curW = MapCell.Width * Zoom;
        float curH = MapCell.Height * Zoom;
        int row = (int)(y / curH);
        int col = (int)(x / curW);
        return new MapPos(row, col);
    }
}