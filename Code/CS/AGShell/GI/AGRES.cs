using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;

namespace AGShell
{
    public class AGRES
    {
        public static Font DEBUGFONT = new Font("宋体", 12);
        public static Font TITLEFONT = new Font("宋体", 12);

        static AGRES()
        {
            PrivateFontCollection font = new PrivateFontCollection();
            font.AddFontFile(string.Format("{0}Fonts\\TITLE.TTF",DATUtility.GetResPath()));
            FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
            DEBUGFONT = new Font(myFontFamily, 56F, FontStyle.Regular); 
        }
    }
}
