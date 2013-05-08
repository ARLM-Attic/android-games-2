using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectSound;
using System.Windows.Forms;

namespace AGShell
{
    public class AGADI : IADI
    {
        private Device ApplicationDevice = null;
        private string PathSoundFile = string.Empty;

        public void Init(Form from)
        {
            ApplicationDevice = new Device();
            ApplicationDevice.SetCooperativeLevel(from, CooperativeLevel.Normal);

        }

        public void Play()
        {
            SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sad_01.wav", DATUtility.GetResPath()), ApplicationDevice);
            ApplicationBuffer.Play(0, BufferPlayFlags.Default);
        }
    }
}
