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
        private Device _device = null;
        private string PathSoundFile = string.Empty;

        public SecondaryBuffer BGMBuffer { get; private set; }

        public void Init(Form from)
        {
            _device = new Device();
            _device.SetCooperativeLevel(from, CooperativeLevel.Normal);

        }

        public void Play()
        {
            BufferDescription bufferDesc = new BufferDescription();
            bufferDesc.Flags = BufferDescriptionFlags.GlobalFocus;
            SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sad_01.wav", DATUtility.GetResPath()), bufferDesc, _device);
            ApplicationBuffer.Play(0, BufferPlayFlags.Default);
        }

        public void PlayBGM(int id)
        {
            if (BGMBuffer != null)
            {
                BGMBuffer.Dispose();
            }

            if (id == 1)
            {
                // splash
                BufferDescription bufferDesc = new BufferDescription();
                bufferDesc.Flags = BufferDescriptionFlags.GlobalFocus;
                SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sap_07.wav", DATUtility.GetResPath()), bufferDesc, _device);
                ApplicationBuffer.Play(0, BufferPlayFlags.Default);
                BGMBuffer = ApplicationBuffer;
            }
            else if (id == 2)
            {
                // stage
                BufferDescription bufferDesc = new BufferDescription();
                bufferDesc.Flags = BufferDescriptionFlags.GlobalFocus;
                SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sabgm_s1.wav", DATUtility.GetResPath()), bufferDesc, _device);
                ApplicationBuffer.Play(0, BufferPlayFlags.Looping);
                BGMBuffer = ApplicationBuffer;
            }
            else if(id==100)
            {
                BufferDescription bufferDesc = new BufferDescription();
                bufferDesc.Flags = BufferDescriptionFlags.GlobalFocus;
                SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sabgm_b1.wav", DATUtility.GetResPath()), bufferDesc, _device);
                ApplicationBuffer.Play(0, BufferPlayFlags.Looping);
                BGMBuffer = ApplicationBuffer;
            }
            else
            {
                BufferDescription bufferDesc = new BufferDescription();
                bufferDesc.Flags = BufferDescriptionFlags.GlobalFocus;

                SecondaryBuffer ApplicationBuffer = new SecondaryBuffer(string.Format("{0}wav\\sabgm_f1.wav", DATUtility.GetResPath()), bufferDesc, _device);
                ApplicationBuffer.Play(0, BufferPlayFlags.Looping);
                BGMBuffer = ApplicationBuffer;
            }
        }
    }
}
