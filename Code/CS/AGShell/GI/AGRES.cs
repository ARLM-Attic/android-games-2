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
        public static Font DEBUGFONT;
        public static Font NormalFont;
        public static Font TITLEFONT;

        private static Font SmallUIFont;
        private static Font NormalUIFont;
        private static Font LargeUIFont;

        public static IntPtr SmallFontHandle;
        public static IntPtr NormalFontHandle;

        public static IntPtr SmallUIHfont;
        public static IntPtr NormalUIHfont;
        public static IntPtr LargeUIFontHandle;

        static AGRES()
        {
            PrivateFontCollection font = new PrivateFontCollection();
            font.AddFontFile(string.Format("{0}Fonts\\DEFAULT.TTF",DATUtility.GetResPath()));
            FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
            DEBUGFONT = new Font(myFontFamily, 9F, FontStyle.Regular);
            NormalFont = new Font(myFontFamily, 14F, FontStyle.Regular);
            SmallFontHandle = DEBUGFONT.ToHfont();
            NormalFontHandle = NormalFont.ToHfont();

            font.AddFontFile(string.Format("{0}Fonts\\UI.TTF", DATUtility.GetResPath()));
            FontFamily uiFontFamily = new FontFamily(font.Families[1].Name, font);
            LargeUIFont = new Font(uiFontFamily, 32F, FontStyle.Regular);
            LargeUIFontHandle = LargeUIFont.ToHfont();
            NormalUIFont = new Font(uiFontFamily, 16F, FontStyle.Regular);
            NormalUIHfont = NormalUIFont.ToHfont();
            SmallUIFont = new Font(uiFontFamily, 9F, FontStyle.Regular);
            SmallUIHfont = SmallUIFont.ToHfont();
        }
    }
}
