using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGEditor.Windows.Model
{
    public interface IFrameEditObserver
    {
        void OnFrameChanged(Frame2D frame);
    }
}
