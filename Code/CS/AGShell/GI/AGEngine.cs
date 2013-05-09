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

        public Map2D CurrentMap { get; private set; }

        public void Init(System.Windows.Forms.Form form)
        {
            CurrentSence = new SplashSence(this);

            _gdi = new AGGDI();
            _gdi.Init(form);

            _adi = new AGADI();
            _adi.Init(form);

            _idi = new AGIDI();
            _idi.Init(form);

            _thread = new Thread(Run);
            _inputThread = new Thread(InputRunning);
            _waitHanle = new EventWaitHandle(false, EventResetMode.ManualReset);
            _waitHanleInput = new EventWaitHandle(false, EventResetMode.ManualReset);

            Debug.WriteLine("AGEngine Init!");
        }

        public void Start()
        {
            _isRunning = true;
            _thread.Start();
            _inputThread.Start();


            Debug.WriteLine("AGEngine Start!");
        }

        public void Stop()
        {
            _isRunning = false;
            Debug.WriteLine("AGEngine Running False!");

            WaitHandle[] handles = new WaitHandle[] { _waitHanle, _waitHanleInput };
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                // 使用foreach，在多线程中等待每一个句柄
                foreach (WaitHandle handle in handles)
                {
                    WaitHandle.WaitAny(new WaitHandle[] { handle });
                }
            }
            else
            {
                WaitHandle.WaitAll(handles);
            }

            Debug.WriteLine("AGEngine Stop!");
        }

        private void Run()
        {
            Debug.WriteLine("AGEngine Loop Thread Start!");
            while (_isRunning)
            {
                _ticks = DateTime.Now.Ticks;
                if (_ticks - _lastTicks > _invant)
                {
                    _fpsCounter++;
                    if (_ticks - _fpsTicks > 1000*10000)
                    {
                        _fps = _fpsCounter;
                        _fpsCounter = 0;
                        _fpsTicks = _ticks;
                    }
                    _lastTicks = _ticks;

                    _gdi.Clear();
                    Render(_gdi);
                    _gdi.DrawText(string.Format("fps:{0})", _fps), 0, 0);
                    //_gdi.DrawText(string.Format("fps:{0} p({1},{2})", _fps, _idi.MousePoint.X, _idi.MousePoint.Y), 0, 0);
                    _gdi.Flush();
                }
            }
            _waitHanle.Set();
            Debug.WriteLine("AGEngine Loop Thread Exist!");
        }

        private void InputRunning()
        {
            Debug.WriteLine("AGEngine Input Thread Start!");
            while (_isRunning)
            {
                //Thread.Sleep(200);
                if (CurrentSence == null)
                {
                    continue;
                }

                byte[] buttons;
                int x;
                int y;
                int z;
                _idi.Update(out buttons, out x, out y, out z);

                if (buttons == null)
                {
                    continue;
                }

                if (0 != buttons[0])
                {
                    CurrentSence.MouseInput(0, 1, x, y, z, _idi.MousePoint.X, _idi.MousePoint.Y);
                }
                else
                {
                    CurrentSence.MouseInput(0, 0, x, y, z, _idi.MousePoint.X, _idi.MousePoint.Y);
                }
                if (0 != buttons[1])
                {
                    CurrentSence.MouseInput(1, 1, x, y, z, _idi.MousePoint.X, _idi.MousePoint.Y);
                }
                else
                {
                    CurrentSence.MouseInput(1, 0, x, y, z, _idi.MousePoint.X, _idi.MousePoint.Y);
                }
                if (0 != buttons[2])
                {
                    CurrentSence.MouseInput(2, 1, x, y, z, _idi.MousePoint.X, _idi.MousePoint.Y);
                }
                else
                {
                    CurrentSence.MouseInput(2, 0, x, y, z, _idi.MousePoint.X, _idi.MousePoint.Y);
                }
            }
            _waitHanleInput.Set();
            Debug.WriteLine("AGEngine Input Thread Exist!");
        }

        private void Render(AGGDI gdi)
        {
            if (CurrentSence != null)
            {
                CurrentSence.Render(gdi);
            }
        }

        internal void InputEvent(int msg, int lParam, int wParam)
        {
            if (CurrentSence != null)
            {
                CurrentSence.InputEvent(msg, lParam, wParam);
            }
        }

        public void LoadMap(int mapId)
        {
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

            CurrentMap = DATUtility.GetMap(mapId);

            //CurrentMap.Camps[0].AvailableUnitList.Add(DATUtility.GetUnit(300));
            CurrentMap.Camps[0].AvailableUnitList.Add(DATUtility.GetUnit(301));
            //CurrentMap.Camps[0].AvailableUnitList.Add(DATUtility.GetUnit(302));


            CurrentMap.Camps[1].AvailableUnitList.Add(DATUtility.GetUnit(301));
        }

        public void SwitchSence(Sence sence)
        {
            CurrentSence = sence;
        }
    }
}
