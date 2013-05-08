using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AGShell.GI
{
    public class AGEngine : IEngine
    {
        public Sence CurrentSence { get; private set; }

        private Thread _thread;
        private EventWaitHandle _waitHanle;
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

        public IADI ADI { get { return _adi; } }

        public Map2D CurrentMap { get; private set; }

        public void Init(System.Windows.Forms.Form form)
        {
            CurrentSence = new LoadMapSence(this);

            _gdi = new AGGDI();
            _gdi.Init(form);

            _adi = new AGADI();
            _adi.Init(form);

            _thread = new Thread(Run);
            _waitHanle = new EventWaitHandle(false, EventResetMode.ManualReset);

            Debug.WriteLine("AGEngine Init!");
        }

        public void Start()
        {
            _isRunning = true;
            _thread.Start();

            Debug.WriteLine("AGEngine Start!");
        }

        public void Stop()
        {
            _isRunning = false;

            _waitHanle.WaitOne();

            Debug.WriteLine("AGEngine Stop!");
        }

        private void Run()
        {
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
                    _gdi.DrawText(string.Format("fps:{0}", _fps), 0, 0);
                    _gdi.Flush();
                }
            }
            Debug.WriteLine("AGEngine Thread Exist!");
            _waitHanle.Set();
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
