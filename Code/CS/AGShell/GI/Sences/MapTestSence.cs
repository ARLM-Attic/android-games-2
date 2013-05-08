using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell.GI
{
    public class MapTestSence : Sence
    {
        private Map2D _map;
        private Camera _camera;
        private HUD _hud;

        private Point2D _storedCameraPos;
        private Point2D _storedPos;
        private Point2D _currentPos;
        private bool _moveCamera = false;

        public MapTestSence(AGEngine engine, Map2D map)
            : base(engine)
        {
            _map = map;
            _camera = new Camera(MainWindow.Width, MainWindow.Height - 20 - 50, new Point2D(MainWindow.Width / 2, 20 + (MainWindow.Height - 20 - 50) / 2));
            _camera.Attach(_map, new Point2D(0, 0));
            _camera.Target(_map.Camps[0].StartPos);
            _hud = new TestHUD(_map);
        }

        protected override void OnRender(AGGDI gdi)
        {
            if(_map.State == GState.Running)
            {
                _map.GameTime++;
                if (_map.GameTime % 30 == 0)
                {
                    for (int iCamp = 0; iCamp < _map.Camps.Count; iCamp++)
                    {
                        _map.Camps[iCamp].Income += _map.Camps[iCamp].IncomePreSec;
                    }
                }

                AI.Run(_map, _map.Camps[1]);

                MapRender.Render(_engine, gdi, _map, _camera);
                _hud.Render(gdi);

                gdi.DrawText(string.Format("map:{0} Time:{1}  {2}", _map.ID,  _map.GameTime, _map.GameTime / 30), 50, 0);

                if (AGSUtility.CheckVictory(_map))
                {
                    AGSUtility.Victory(_map, _map.Camps[0]);
                }
                else if (AGSUtility.CheckDefeat(_map))
                {
                    AGSUtility.Defeat(_map, _map.Camps[0]);
                }
            }
            else if (_map.State == GState.Victory)
            {
                _engine.SwitchSence(new VictorySence(_engine, _map));
            }
            else if (_map.State == GState.Defeat)
            {
                _engine.SwitchSence(new DefeatSence(_engine, _map));
            }
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            if (_camera != null)
            {
                if (_moveCamera)
                {
                    _camera.MoveTo(_storedCameraPos.X - (_currentPos.X - _storedPos.X), _storedCameraPos.Y - (_currentPos.Y - _storedPos.Y));
                }

                _hud.InputEvent(msg, lParam, wParam);
                if (msg == 1)
                {
                    //if (lParam == 81)
                    //{
                    //    _camera.Far();
                    //}
                    //else if (lParam == 69)
                    //{
                    //    _camera.Near();
                    //}
                    //else if (lParam == 87)
                    //{
                    //    _camera.MoveUp();
                    //}
                    //else if (lParam == 65)
                    //{
                    //    _camera.MoveLeft();
                    //}
                    //else if (lParam == 83)
                    //{
                    //    _camera.MoveDown();
                    //}
                    //else if (lParam == 68)
                    //{
                    //    _camera.MoveRight();
                    //}
                    //else if (lParam == 49)
                    //{
                    //    Object2D obj = AGSUtility.CreateObject(_map, _map.Camps[0], DATUtility.GetUnit(300), "unknown", _map.Camps[0].StartPos, Direction2DDef.South.Id);
                    //    AGSUtility.MoveTo(obj, _map.Camps[1].StartPos);
                    //}
                    //else if (lParam == 50)
                    //{
                    //    Object2D obj = AGSUtility.CreateObject(_map, _map.Camps[1], DATUtility.GetUnit(300), "unknown", _map.Camps[1].StartPos, Direction2DDef.South.Id);
                    //    AGSUtility.MoveTo(obj, _map.Camps[0].StartPos);
                    //}
                }
                else if (msg == 3)
                {
                    _storedCameraPos = _camera.CenterTargetPos;
                    _storedPos = new Point2D(lParam, wParam);
                    _currentPos = new Point2D(lParam, wParam);
                    _moveCamera = true;
                }
                else if (msg == 4)
                {
                    _currentPos = new Point2D(lParam, wParam);
                    _moveCamera = false;
                }
                else if (msg == 5)
                {
                    _currentPos = new Point2D(lParam, wParam);
                }
            }
        }
    }
}
