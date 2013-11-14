using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Data;

namespace AG.Editor.Windows.Controls
{
    public interface IFrameEditObserver
    {
        void OnFrameChanged(AGFrame frame);
    }
}
