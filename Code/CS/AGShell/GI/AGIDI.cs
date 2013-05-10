using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Windows.Forms;
using Microsoft.DirectX;
using System.Runtime.InteropServices;
using System.Drawing;

namespace AGShell
{
    public class AGIDI
    {
        [DllImport("user32.dll")]
        // GetCursorPos() makes everything possible
        static extern bool GetCursorPos(ref Point lpPoint);

        public Device Device { get; private set; }
        private bool _isOK = false;
        private Point _mousePoint;
        public Point MousePoint { get { return _mousePoint; } }
        private Form _form;
        Point _startPos;
        Point _mousePointT;

        public void Init(Form form)
        {
            _form = form;

            _startPos = _form.PointToScreen(new Point(0, 0));

            // Create the device.
            Device = new Device(SystemGuid.Mouse);
            // Set the cooperative level for the device.
            Device.SetCooperativeLevel(form.Handle, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);
            // Set the data format to the mouse2 pre-defined format.
            Device.SetDataFormat(DeviceDataFormat.Mouse);
        }

        public MouseMessage Update()
        {

            try
            {
                Device.Poll();
            }
            catch (DirectXException ex)
            {
                try
                {
                    // Acquire the device.
                    Device.Acquire();
                    // Set the flag for now.
                    _isOK = true;
                }
                catch (DirectXException ex2)
                {
                    _isOK = false;
                }
            }

            if (_isOK)
            {
                MouseMessage mouse = new MouseMessage();

                MouseState mouseState = Device.CurrentMouseState;

                mouse.Buttons = mouseState.GetMouseButtons();
                mouse.DeltaX = mouseState.X;
                mouse.DeltaY = mouseState.Y;
                mouse.DeltaZ = mouseState.Z;

                AGIDI.GetCursorPos(ref _mousePointT);
                _mousePoint.X = _mousePointT.X - _startPos.X;
                _mousePoint.Y = _mousePointT.Y - _startPos.Y;
                mouse.X = _mousePoint.X;
                mouse.Y = _mousePoint.Y;
                return mouse;
            }
            else
            {
                return null;
            }
        }
    }
}
