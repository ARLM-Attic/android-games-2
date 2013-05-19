using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AGShell
{
    public class AGEngine : IEngine
    {
        public Sence CurrentSence { get; private set; }

        private System.Windows.Forms.Form _form;
        private Thread _thread;
        private Thread _inputThread;
        private EventWaitHandle _waitHanle;
        private EventWaitHandle _waitHanleInput;
        private bool _isRunning;

        #region fps
        private long _ticks;
        private long _lastTicks;
        private long _deltaTicks;
        private long _invant = 20 * 10000;
        private long _fpsTicks;
        private byte _fps;
        private byte _fpsCounter;
        #endregion

        private AGGDI _gdi;
        private AGADI _adi;
        private AGIDI _idi;

        public IADI ADI { get { return _adi; } }
        public IIDI IDI { get { return _idi; } }
        public IGDI GDI { get { return _gdi; } }

        private MouseMessage _mouse;
        public Map2D CurrentMap { get; set; }

        public void Init(System.Windows.Forms.Form form)
        {
            _form = form;

            AGRES.Load();

            _gdi = new AGGDI();
            _gdi.Init(form);

            _adi = new AGADI();
            _adi.Init(form);

            _idi = new AGIDI();
            _idi.Init(form);

            _thread = new Thread(Run);
            _inputThread = new Thread(InputRunning);
            _waitHanle = new EventWaitHandle(false, EventResetMode.AutoReset, "ag.loop");
            _waitHanleInput = new EventWaitHandle(false, EventResetMode.AutoReset, "ag.input");

            Debug.WriteLine("AGEngine Init!");
        }

        public void Start()
        {
            _isRunning = true;
            _inputThread.Start();
            _thread.Start();


            Debug.WriteLine("AGEngine Start!");
        }

        public void Stop()
        {
            _isRunning = false;
            Debug.WriteLine("AGEngine Running False!");

            _waitHanleInput.WaitOne();
            Debug.WriteLine("_waitHanleInput.WaitOne()!");
            _waitHanle.WaitOne();
            Debug.WriteLine("_waitHanle.WaitOne()!");

            //WaitHandle[] handles = new WaitHandle[] { _waitHanle, _waitHanleInput };
            //if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            //{
            //    // 使用foreach，在多线程中等待每一个句柄
            //    foreach (WaitHandle handle in handles)
            //    {
            //        WaitHandle.WaitAny(new WaitHandle[] { handle });
            //    }
            //}
            //else
            //{
            //    WaitHandle.WaitAll(handles);
            //}

            Debug.WriteLine("AGEngine Stop!");
        }

        private void Run()
        {
            Debug.WriteLine("AGEngine Loop Thread Start!");

            PlayerData.Current = PlayerDataUtil.Load(1);
            SwitchSence(new SplashSence(this));

            while (_isRunning)
            {
                _ticks = DateTime.Now.Ticks;
                if (_ticks - _lastTicks > _invant)
                {
                    #region 计算fps
                    _fpsCounter++;
                    if (_ticks - _fpsTicks > 1000 * 10000)
                    {
                        _fps = _fpsCounter;
                        _fpsCounter = 0;
                        _fpsTicks = _ticks;
                    }
                    #endregion
                    _lastTicks = _ticks;

                    #region 获取鼠标信息
                    if (_mouse != null)
                    {
                        _idi.Mouse.Copy(_mouse);
                    }
                    #endregion

                    // 更新数据
                    Loop();

                    // 渲染画面
                    Render();
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
            Debug.WriteLine("_waitHanle.Set()!");
            _waitHanle.Set();
            Debug.WriteLine("AGEngine Loop Thread Exist!");
        }

        private void InputRunning()
        {
            Debug.WriteLine("AGEngine Input Thread Start!");
            while (_isRunning)
            {
                Thread.Sleep(20);
                if (CurrentSence == null)
                {
                    continue;
                }

                _mouse = _idi.Update();

                if (_mouse == null)
                {
                    continue;
                }
            }
            _waitHanleInput.Set();
            Debug.WriteLine("AGEngine Input Thread Exist!");
        }

        private void Loop()
        {
            if (_idi.Mouse != null && CurrentSence != null)
            {
                CurrentSence.Loop(this);
            }
        }

        private void Render()
        {
            _gdi.Clear();
            if (CurrentSence != null)
            {
                CurrentSence.Render(_gdi);
            }

            _gdi.DrawText(string.Format("fps:{0}", _fps), 0, 50);
            _gdi.DrawText(string.Format("mouse:{0},{1}", _idi.Mouse.X, _idi.Mouse.Y), 0, 70);
            _gdi.Flush();
        }

        public void LoadMap(int mapId)
        {
            CurrentMap = null;
            Thread thread = new Thread(Loading);
            thread.Start(mapId);
        }

        private void Loading(object p)
        {
            int mapId = (int)p;

            List<int> units = DATUtility.GetUnits();
            foreach (int unitId in units)
            {
                DATUtility.GetUnit(unitId);
            }

            Map2D map = DATUtility.GetMap(mapId);

            //CurrentMap.Camps[0].AvailableUnitList.Add(DATUtility.GetUnit(300));
            Camp playerCamp = null;
            for (int index = 0; index < map.Camps.Count; index++)
            {
                Camp camp = map.Camps[index];

                if (camp.Type == CampType.Player)
                {
                    playerCamp = camp;
                    map.Camps[index].TargetPos = map.Camps[1].StartPos;
                    map.Camps[1].TargetPos = map.Camps[0].StartPos;
                    break;
                }
            }
            for (int index = 0; index < map.Camps.Count; index++)
            {
                Camp camp = map.Camps[index];

                if (camp.Type == CampType.Computer)
                {
                    playerCamp.TargetPos = camp.StartPos;
                    camp.TargetPos = playerCamp.StartPos;
                    camp.AvailableUnitList.Add(DATUtility.GetUnit(301));
                }
            }

            #region 加载地图切片材质
            for (int i = 0; i < map.Cells.Length; i++)
            {
                Model2D terrainModel = DATUtility.GetTerrain(map.Cells[i].TerrainId).Model;
                Frame2D frame = terrainModel.GetFrame(1, 1, map.Cells[i].TerrainIndex);
                Texture2D texture = new Texture2D();
                texture.Width = frame.Width;
                texture.Height = frame.Height;
                texture.Data = _gdi.CraeteSurface(new System.Drawing.Bitmap(new System.IO.MemoryStream(frame.Data)), System.Drawing.Color.Black);

                map.Cells[i].Texture2D = texture;
            }
            #endregion

            CurrentMap = map;
        }

        public void SwitchSence(Sence sence)
        {
            CurrentSence = sence;
            CurrentSence.Init();
        }

        public void Exit()
        {
            _form.BeginInvoke(new Action(delegate { _form.Close(); }));
        }
    }
}
