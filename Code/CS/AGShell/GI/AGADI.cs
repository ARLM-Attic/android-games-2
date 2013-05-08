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

        public SecondaryBuffer BGMBuffer { get; private set; }

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

        public void PlayBGM(int id)
        {
            if (BGMBuffer != null)
            {
                BGMBuffer.Dispose();
            }

            if (id != 100)
            {
                SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sabgm_f1.wav", DATUtility.GetResPath()), ApplicationDevice);
                ApplicationBuffer.Play(0, BufferPlayFlags.Looping);
                BGMBuffer = ApplicationBuffer;
            }
            else
            {
                SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sabgm_b1.wav", DATUtility.GetResPath()), ApplicationDevice);
                ApplicationBuffer.Play(0, BufferPlayFlags.Looping);
                BGMBuffer = ApplicationBuffer;
            }
        }
    }
}
