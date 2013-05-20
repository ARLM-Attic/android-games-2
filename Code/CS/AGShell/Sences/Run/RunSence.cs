using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGShell
{
    public class RunSence : Sence
    {
        private Map2D _map;
        private Camera _camera;

        private Point2D _storedCameraPos;
        private Point2D _storedPos;
        private Point2D _currentPos;
        private bool _moveCamera = false;

        public RunSence(IEngine engine, Map2D map)
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
            return new RunHUD(_engine, _map);
        }

        protected override void OnRender(IGDI gdi)
        {
            if(_map.State == GState.Running)
            {
                MapRender.Render(_engine, gdi, _map, _camera);

                gdi.DrawText(string.Format("map:{0} Time:{1} {2}", _map.ID,  _map.GameTime, _map.GameTime / 30), 250, 0);
            }
        }

        protected override void OnLoop(IEngine engine)
        {
            if (_camera == null)
            {
                return;
            }

            if (_map.State == GState.Running)
            {
                #region 计算时间
                _map.GameTime++;

                if (_map.GameTime % 30 == 0)
                {
                    for (int iCamp = 0; iCamp < _map.Camps.Count; iCamp++)
                    {
                        _map.Camps[iCamp].Income += _map.Camps[iCamp].IncomePreSec;
                        if (_map.Camps[iCamp].Type == CampType.Player)
                        {
                            if (_map.Camps[iCamp].Income > 500)
                            {
                                _map.Camps[iCamp].Income = 500;
                            }
                        }
                        else
                        {
                            if (_map.Camps[iCamp].Income > 5000)
                            {
                                _map.Camps[iCamp].Income = 5000;
                            }
                        }

                        if (_map.Camps[iCamp].Result != null)
                        {
                            _map.Camps[iCamp].Result.GameTime++;
                        }
                    }
                }
                #endregion

                #region 更新技能
                for (int skillIndex = 0; skillIndex < _map.SkillList.Count; skillIndex++)
                {
                    _map.SkillList[skillIndex].Loop(engine, null);
                }
                #endregion

                #region 更新技能状态
                for (int skillIndex = 0; skillIndex < _map.SkillList2.Count; skillIndex++)
                {
                    if (!engine.IDI.Mouse.IsHandled)
                    {
                        _map.SkillList2[skillIndex].Loop(_engine, engine.IDI.Mouse);
                    }
                    else
                    {
                        break;
                    }
                }
                #endregion

                for (int index = 0; index < _map.Camps.Count; index++)
                {
                    if (_map.Camps[index].Type == CampType.Computer)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("ai>> c:{0}  p:{1}", _map.Camps[index].Caption, _map.Camps[index].Population));
                        AI.Run(_map, _map.Camps[index]);
                    }
                }

                #region 更新单位信息
                for (int iObj = 0; iObj < _map.Widgets.Count; iObj++)
                {
                    Object2D item = _map.Widgets[iObj];
                    item.Update(engine);
                }

                for (int iObj = 0; iObj < _map.Widgets.Count; )
                {
                    Object2D item = _map.Widgets[iObj];
                    if (item.DeadTime > 10)
                    {
                        AGSUtility.RemoveObject(item);
                    }
                    else
                    {
                        iObj++;
                    }
                }

                // 排序
                for (int iObj = 0; iObj < _map.Widgets.Count - 1; iObj++)
                {
                    Object2D nextObj = _map.Widgets[iObj + 1];
                    Object2D item = _map.Widgets[iObj];

                    if (item.CurrentPoint.Y > nextObj.CurrentPoint.Y)
                    {
                        _map.Widgets.Remove(nextObj);
                        _map.Widgets.Insert(iObj, nextObj);
                    }
                }
                #endregion

                #region 检查结果
                if (AGSUtility.CheckVictory(_map))
                {
                    AGSUtility.Victory(_map, _map.Camps[0]);
                }
                else if (AGSUtility.CheckDefeat(_map))
                {
                    AGSUtility.Defeat(_map, _map.Camps[0]);
                }
                #endregion
            }
            else if (_map.State != GState.Running)
            {
                _engine.SwitchSence(new ResultSence(_engine, _map, _map.Camps[0].Result));
                _engine.ADI.PlayBGM(22);
            }

            //if (engine.IDI.Mouse.DeltaZ > 0)
            //{
            //    _camera.Far();
            //}

            #region 移动camera操作
            if (engine.IDI.Mouse.IsLBDown())
            {
                if (!_moveCamera)
                {
                    _storedCameraPos = new Point2D(_camera.CenterTargetPos.X, _camera.CenterTargetPos.Y);
                    _storedPos = new Point2D(engine.IDI.Mouse.X, engine.IDI.Mouse.Y);
                    _moveCamera = true;
                }
            }
            else
            {
                _moveCamera = false;
            }

            if (_moveCamera)
            {
                float deltaX = _storedPos.X - engine.IDI.Mouse.X;
                float deltaY = _storedPos.Y - engine.IDI.Mouse.Y;
                _camera.MoveTo(_storedCameraPos.X + deltaX, _storedCameraPos.Y + deltaY);

                _storedCameraPos = _camera.CenterTargetPos;
                _storedPos = new Point2D(engine.IDI.Mouse.X, engine.IDI.Mouse.Y);
            }
            #endregion
        }
    }
}
