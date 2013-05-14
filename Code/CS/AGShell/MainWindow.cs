using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AGShell
{
    public partial class MainWindow : Form
    {
        public static int Width = 640;
        public static int Height = 480;
        //public static int Width = 960;
        //public static int Height = 600;

        private AGEngine _engine;

        public MainWindow()
        {
            InitializeComponent();

            this.ClientSize = new Size(MainWindow.Width, MainWindow.Height);

            _engine = new AGEngine();
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            _engine.Init(this);
            _engine.Start();

            base.OnShown(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _engine.Stop();

            base.OnClosing(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //_engine.InputEvent(1, e.KeyValue, 0);

            base.OnKeyDown(e);
        }
    }
}
