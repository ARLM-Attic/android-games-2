using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.Editor.Panels
{
    public class AGEMainMenuMidiator
    {
        private MenuStrip _mainMenu;

        public AGEMainMenuMidiator(MenuStrip mainMenu)
        {
            _mainMenu = mainMenu;
        }

        //private void AddF
        public void Clear()
        {
            _mainMenu.Items.Clear();
        }

        public ToolStripMenuItem AddMenu(ToolStripMenuItem item)
        {
            _mainMenu.Items.Add(item);
            return item;
        }
    }
}
