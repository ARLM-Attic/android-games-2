using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AGShell
{
    /// <summary>
    /// mail:group.agame@gmail.com
    /// 0p;/9ol.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
