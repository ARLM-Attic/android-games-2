using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Settings;
using AG.Editor.Core.Data;

namespace AG.Editor.Core
{
    public class AGEContext
    {
        private static AGEContext _context;
        
        /// <summary>
        /// get current context information
        /// <para>this is a singleton instance</para>
        /// </summary>
        public static AGEContext Current
        {
            get
            {
                if (_context == null)
                {
                    _context = new AGEContext();
                }
                return _context;
            }
        }

        /// <summary>
        /// get curent config info
        /// </summary>
        public AGESettings Settings { get; set; }

        public AGEProject EProject { get; set; }
    }
}
