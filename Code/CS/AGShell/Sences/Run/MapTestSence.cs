﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class MapTestSence : Sence
    {
        private Map2D _map;
        private Camera _camera;

        private Point2D _storedCameraPos;
        private Point2D _storedPos;
        private Point2D _currentPos;
        private bool _moveCamera = false;

        public MapTestSence(IEngine engine, Map2D map)
            : base(engine)
        {
            _map = map;
            _camera = new Camera(MainWindow.Width, MainWindow.Height, new Point2D(MainWindow.Width / 2, (MainWindow.Height) / 2));
            _camera.Attach(_map, new Point2D(0, 0));
            _camera.Target(_map.Camps[0].StartPos);

            _map.Camps[0].Result = new GameResult();
            _map.Camps[0].Result.MapId = _map.ID;

        }

        protected override HUD CreateHUD()
        {
            return new TestHUD(_engine, _map);
        }

        protected override void OnRender(IGDI gdi)
        {
            if(_map.State == GState.Running)
            {
                _map.GameTime++;

                if (_map.GameTime % 30 == 0)
                {
                    for (int iCamp = 0; iCamp < _map.Camps.Count; iCamp++)
                    {
                        _map.Camps[iCamp].Income += _map.Camps[iCamp].IncomePreSec;

                        if (_map.Camps[iCamp].Result != null)
                        {
                            _map.Camps[iCamp].Result.GameTime++;
                        }
                    }
                }

                AI.Run(_map, _map.Camps[1]);

                MapRender.Render(_engine, gdi, _map, _camera);

                gdi.DrawText(string.Format("map:{0} Time:{1} {2}", _map.ID,  _map.GameTime, _map.GameTime / 30), 250, 0);

                if (AGSUtility.CheckVictory(_map))
                {
                    AGSUtility.Victory(_map, _map.Camps[0]);
                }
                else if (AGSUtility.CheckDefeat(_map))
                {
                    AGSUtility.Defeat(_map, _map.Camps[0]);
                }
            }
            else if (_map.State != GState.Running)
            {
                _engine.SwitchSence(new ResultSence(_engine, _map, _map.Camps[0].Result));
                _engine.ADI.PlayBGM(22);
            }
        }

        public override void InputEvent(int msg, int lParam, int wParam)
        {
            if (_camera != null)
            {
                if (msg == 1)
                {
                    #region key-camera
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
                    #endregion
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

        protected override void OnMouseInput(MouseMessage mouse)
        {
            if (_camera == null)
            {
                return;
            }

            if (_map.PlayerSkill.IsPrepare)
            {
                if (mouse.IsLBDown())
                {
                    _map.Camps[0].TargetPos = new MapPos(
                        mouse.Y / MapCell.Height,
                        mouse.X / MapCell.Width);
                    _map.PlayerSkill.IsPrepare = false;
                    return;
                }
            }

            if (mouse.IsLBDown())
            {
                _storedCameraPos = _camera.CenterTargetPos;
                _moveCamera = true;
            }
            else
            {
                _moveCamera = false;
            }

            if (_moveCamera)
            {
                _storedCameraPos = _camera.CenterTargetPos;
                _camera.MoveTo(_storedCameraPos.X + mouse.DeltaX, _storedCameraPos.Y + mouse.DeltaY);
            }
        }
    }
}
