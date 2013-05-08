//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public class MapRender : Map45Render
//{
//    private Canvas _canvas;

//    private UIElement[] _elements;
//    private Canvas[] _canvasMapCells;

//    private List<UIElement> _widgetElements = new List<UIElement>();
//    private Camera _camera;

//    public Camera Camera
//    {
//        get
//        {
//            return _camera;
//        }
//    }

//    public Model PreviewModel { get; set; }

//    public MapRender(Map45 map, Canvas canvas)
//        : base(map)
//    {
//        Map.ZeroPoint = new Point(0, 0);
//        this._canvas = canvas;

//        _camera = new Camera();
//        _camera.Width = 800;// canvas.ActualWidth;
//        _camera.Height = 600;// canvas.ActualHeight;
//        _camera.CenterPos = new Point(_camera.Width / 2, _camera.Height / 2);
//        _camera.Attach(map);

//        int curCellWidth = (int)(MapCell.Width * this._camera.Zoom);
//        int curCellHeight = (int)(MapCell.Height * this._camera.Zoom);

//        Point pt = Map45Util.TransformDiamondPts(Map.ZeroPoint, Map.CameraTargetPos.Row, Map.CameraTargetPos.Col, curCellWidth, curCellHeight);
//        _camera.SetCenterTargetPos(new Point(pt.X, pt.Y));

//        _elements = new UIElement[Map.Row * Map.Col];
//        _canvasMapCells = new Canvas[Map.Row * Map.Col];

//        for (int row = 0; row < Map.Row; row++)
//        {
//            for (int col = 0; col < Map.Col; col++)
//            {
//                Polygon polygon = new Polygon();

//                _elements[row * Map.Col + col] = polygon;
//                this._canvas.Children.Add(polygon);

//                Canvas canvasCell = new Canvas();
//                Image image = new Image();
//                image.Stretch = Stretch.UniformToFill;
//                image.Opacity = 0.9d;
//                string url = string.Format(
//                    "{0}ClientBin/Datas/Grounds/0001/0001-0000.png",
//                    App.HostUrl);
//                image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
//                canvasCell.Children.Add(image);

//                this._canvas.Children.Add(canvasCell);
//                this._canvasMapCells[row * Map.Col + col] = canvasCell;
//            }
//        }
//    }

//    /// <summary>
//    /// 窗口坐标转换为世界坐标
//    /// </summary>
//    /// <param name="ptInWindow"></param>
//    /// <returns></returns>
//    public MapPos Transform(Point ptInWindow)
//    {
//        Point ptInMap = new Point(ptInWindow.X - Map.ZeroPoint.X,
//            ptInWindow.Y - Map.ZeroPoint.Y);
//        int col = (int)(ptInMap.X / (MapCell.Width * _camera.Zoom) - ptInMap.Y / (MapCell.Height * _camera.Zoom));
//        int row = (int)(ptInMap.X / (MapCell.Width * _camera.Zoom) + ptInMap.Y / (MapCell.Height * _camera.Zoom));

//        if (col < 0 || row < 0 || col >= Map.Col || row >= Map.Row)
//        {
//            return null;
//        }
//        return new MapPos(row, col);
//    }

//    public override void Render()
//    {
//        // 裁减
//        // 设置裁剪区域
//        RectangleGeometry geometry = new RectangleGeometry();
//        geometry.Rect = _camera.Rect;
//        this._canvas.Clip = geometry;

//        int curCellWidth = (int)(MapCell.Width * this._camera.Zoom);
//        int curCellHeight = (int)(MapCell.Height * this._camera.Zoom);

//        for (int row = 0; row < Map.Row; row++)
//        {
//            for (int col = 0; col < Map.Col; col++)
//            {
//                MapCell cell = Map.GetCell(new MapPos(row, col));

//                Polygon c = _elements[row * Map.Col + col] as Polygon;
//                Point pt = Map45Util.TransformDiamondPts(Map.ZeroPoint, row, col, curCellWidth, curCellHeight);

//                if (pt.Y - curCellHeight > _camera.Rect.Bottom || pt.Y + curCellHeight < _camera.Rect.Top
//                    || pt.X + curCellWidth < _camera.Rect.Left || pt.X > _camera.Rect.Right)
//                {
//                    c.Visibility = Visibility.Collapsed;
//                    continue;
//                }

//                c.Points = new PointCollection();
//                c.Points.Add(pt);
//                c.Points.Add(new Point(pt.X + curCellWidth / 2, pt.Y - curCellHeight / 2));
//                c.Points.Add(new Point(pt.X + curCellWidth, pt.Y));
//                c.Points.Add(new Point(pt.X + curCellWidth / 2, pt.Y + curCellHeight / 2));
//                c.Stroke = new SolidColorBrush(Colors.Green);
//                c.StrokeThickness = 0;
//                c.Visibility = Visibility.Visible;

//                if (cell.Value != 0)
//                {
//                    c.Fill = new SolidColorBrush(Colors.Red);
//                }
//                else
//                {
//                    c.Fill = null;
//                }

//                Canvas canvasCell = this._canvasMapCells[row * Map.Col + col];
//                (canvasCell.Children[0] as Image).Width = curCellWidth;
//                (canvasCell.Children[0] as Image).Height = curCellHeight;
//                Canvas.SetLeft(canvasCell, pt.X);
//                Canvas.SetTop(canvasCell, pt.Y - curCellHeight / 2);
//            }
//        }

//        foreach (UIElement element in _widgetElements)
//        {
//            this._canvas.Children.Remove(element);
//        }

//        for (int i = 0; i < Map.Widgets.Count; i++)
//        {
//            Canvas canvas = new Canvas();
//            Image image = new Image();
//            MapPos mapPos = Map.Widgets[i].SitePos;
//            Point pt = Map45Util.TransformDiamondPts(Map.ZeroPoint, mapPos.Row, mapPos.Col, curCellWidth, curCellHeight);

//            Model model = Map.Widgets[i].GetModel();
//            if (model != null)
//            {
//                Frame frame = Map.Widgets[i].GetModel().GetFrame();
//                if (image.Source == null)
//                {
//                    string url = string.Format(
//                        "{0}ClientBin/Datas/Models/{1:d4}/{2}",
//                        App.HostUrl,
//                        Map.Widgets[i].GetModel().ID,
//                        frame.FileName);
//                    image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
//                    image.Stretch = Stretch.Fill;
//                    image.Width = frame.Width * _camera.Zoom;
//                    image.Height = frame.Height * _camera.Zoom;
//                }
//                Canvas.SetLeft(canvas, pt.X - frame.OffsetX * _camera.Zoom);
//                Canvas.SetTop(canvas, pt.Y - frame.OffsetY * _camera.Zoom);
//                Canvas.SetZIndex(canvas, 100000 - i);
//                canvas.Children.Add(image);
//            }


//            _widgetElements.Add(canvas);
//            this._canvas.Children.Add(canvas);
//        }
//    }
//}