using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.DirectDraw;
using System.Windows.Forms;

namespace AGShell
{
    public class AGGDI : IGDI
    {
#region gdi
        //private Graphics _primaryGraphics;
        //private Graphics _graphics;
        //private Image _image;

        //public void Init(System.Windows.Forms.Form form)
        //{
        //    _primaryGraphics = Graphics.FromHwnd(form.Handle);

        //    _image = new Bitmap(MainWindow.Width, MainWindow.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        //    _graphics = Graphics.FromImage(_image);
        //}
#endregion
        
        private Rectangle m_rect;

        private Device m_device; // Microsoft.DirectX.DirectDraw.

        public Device Device
        {
            get { return m_device; }
        }

        private Clipper m_clipper;   //'剪切对象
        private SurfaceDescription m_desc1;
        private SurfaceDescription m_desc2;
        private Surface m_ps;  //primarySurface
        private Surface m_bs;

        private bool m_isWindowDisplay = false;
        private Form m_form;


        Surface vOffSurface;

        public int m_widthPix;
        public int m_heightPix;
        private Point _startPos;

        private Font m_defaultFont;

        private Graphics m_graphics;

        public AGGDI()
        {
            Bitmap bitmap = new Bitmap(10, 10);
            m_graphics = Graphics.FromImage(bitmap);
        }

        public bool InitDDraw(Form form ,int widthPix, int heightPix)
        {
            try
            {
                m_widthPix = widthPix;
                m_heightPix = heightPix;
                System.Diagnostics.Debug.WriteLine(string.Format("screent : ({0},{1})!", m_widthPix, m_heightPix));

                m_device = new Device(CreateFlags.Default);
                m_device.SetCooperativeLevel(form, CooperativeLevelFlags.FullscreenExclusiveAllowModex); // fullscreen

                m_device.SetDisplayMode(m_widthPix, m_heightPix, 16, 0, true);

                //Primarybuffer的设置
                m_desc1 = new SurfaceDescription();
                m_desc1.SurfaceCaps.VideoMemory = true;
                m_desc1.SurfaceCaps.PrimarySurface = true;
                m_desc1.SurfaceCaps.Flip = true;
                m_desc1.SurfaceCaps.Complex = true;
                m_desc1.BackBufferCount = 1;
                m_ps = new Surface(m_desc1, m_device);

                m_desc2 = new SurfaceDescription();
                m_desc2.SurfaceCaps.BackBuffer = true;
                m_bs = m_ps.GetAttachedSurface(m_desc2.SurfaceCaps);
                m_bs.ForeColor = Color.Black;
                m_bs.FontTransparency = true;


                m_defaultFont = new Font("楷体", 9);
                m_bs.FontHandle = m_defaultFont.ToHfont();

                //OK只要把PrimaryBuffer跟BackBuffer设置好就算初始化完成,其他的图像都往上贴


                m_isWindowDisplay = false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Init(Form form)
        {
            try
            {
                m_form = form;
                _startPos = m_form.PointToScreen(new Point(0, 0));

                m_widthPix = MainWindow.Width;
                m_heightPix = MainWindow.Height;

                #region Create new DirectDraw device
                // Create new DirectDraw device
                m_device = new Device();
                // set windowed mode, owner is this form
                m_device.SetCooperativeLevel(form, CooperativeLevelFlags.Normal);
                #endregion

                // Create new clipper
                m_clipper = new Clipper();
                // Set window size
                //form.ClientSize = new Size(m_widthPix, m_heightPix);
                //form.Location = new Point(0, 0);
                // Set window associated to clipper
                m_clipper.Window = form;

                #region Primary Surface
                // Create new SurfaceDescription to create Surface
                SurfaceDescription primaryDesc = new SurfaceDescription();
                // Use System Memory
                primaryDesc.SurfaceCaps.VideoMemory = true;
                // To create Primary Surface
                primaryDesc.SurfaceCaps.PrimarySurface = true;

                // Create new Primary Surface
                m_ps = new Surface(primaryDesc, m_device);
                // Set clipper associated to surface
                m_ps.Clipper = m_clipper;
                #endregion

                #region Background Surface
                // Create new SurfaceDescription to create Surface
                SurfaceDescription backgroundDesc = new SurfaceDescription();
                // Use System Memory
                backgroundDesc.SurfaceCaps.VideoMemory = true;
                // To create Secondary Surface
                backgroundDesc.SurfaceCaps.OffScreenPlain = true;
                // Set Secondary Surface width as Primary Surface width
                backgroundDesc.Width = m_widthPix;// m_ps.SurfaceDescription.Width;
                // Set Secondary Surface height as Primary Surface height
                backgroundDesc.Height = m_heightPix;// m_ps.SurfaceDescription.Height;

                // Crate Secondary Surface
                m_bs = new Surface(backgroundDesc, m_device);
                // Create new Clipper to Secondary Surface
                m_bs.Clipper = new Clipper();
                // Create Clipper List
                m_bs.Clipper.ClipList = new Rectangle[] { new Rectangle(0, 0, m_widthPix, m_widthPix) };
                #endregion

                m_defaultFont = new Font("宋体", 9);
                m_bs.FontHandle = m_defaultFont.ToHfont();

                m_isWindowDisplay = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public void ReleaseDDraw()
        {
            if (m_bs != null)
            {
                m_bs.Dispose();
                m_bs = null;
            }

            if (m_ps != null)
            {
                m_ps.Dispose();
                m_ps = null;
            }

            if (m_device != null)
            {
                m_device.Dispose();
                m_device = null;
            }
        }

        public void DrawText(string text, float x, float y)
        {
            m_bs.FontHandle = AGRES.SmallFontHandle;
            m_bs.ForeColor = Color.FromArgb(0xee, 0xee, 0xee);
            m_bs.DrawText((int)x, (int)y, text, false);
        }

        public void DrawText(IntPtr font, int color, string text, int x, int y)
        {
            m_bs.FontHandle = font;
            m_bs.ForeColor = Color.FromArgb(color);
            m_bs.DrawText((int)x, (int)y, text, false);
        }

        public void DrawShadowText(string text, float x, float y)
        {
            m_bs.FontHandle = AGRES.SmallFontHandle;
            m_bs.ForeColor = Color.FromArgb(0xee, 0xee, 0xee);
            m_bs.DrawText((int)x, (int)y, text, false);

            m_bs.ForeColor = Color.FromArgb(0x01, 0x01, 0x01);
            m_bs.DrawText((int)x + 1, (int)y + 1, text, false);
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            m_bs.ForeColor = Color.Red;
            m_bs.DrawLine((int)x1, (int)y1, (int)x2, (int)y2);
        }

        public void DrawEllipse(float x, float y, float w, float h)
        {
            return;
            m_bs.ForeColor = Color.Red;
            m_bs.FillColor = Color.Transparent;
            m_bs.DrawEllipse((int)x, (int)y, (int)(x + w), (int)(y + h));
        }

        public void DrawRectangle(float x, float y, float w, float h)
        {
            return;
            m_bs.ForeColor = Color.Green;
            m_bs.FillColor = Color.Transparent;
            m_bs.DrawBox((int)x, (int)y, (int)(x + w), (int)(y + h));
        }

        public void DrawBlock(float x, float y, float w, float h)
        {
            //return;
            DrawRectangle(x, y, w, h);
            DrawLine(x, y, x + w, y + h);
            DrawLine(x, y + h, x + w, y);
        }

        public void DrawImage(Bitmap image, float x, float y, float width, float height, float originalWidth, float originalHeight)
        {
            Surface surface = CraeteSurface(image, Color.Black);
            Rectangle destRect = new Rectangle((int)x, (int)y, (int)width, (int)height);
            Rectangle srcRect = new Rectangle((int)0, (int)0, (int)originalWidth, (int)originalHeight);
            m_bs.Draw(destRect, surface, srcRect, DrawFlags.KeySource | DrawFlags.DoNotWait);
            surface.Dispose();
        }

        public void DrawImage(Bitmap image, float x, float y, float width, float height, float srcX, float srcY, float originalWidth, float originalHeight)
        {
            if (srcY <= 0)
            {
                srcY = 0;
            }

            Surface surface = CraeteSurface(image, Color.Black);
            Rectangle destRect = new Rectangle((int)x, (int)y, (int)width, (int)height);
            Rectangle srcRect = new Rectangle((int)srcX, (int)srcY, (int)originalWidth, (int)originalHeight);
            m_bs.Draw(destRect, surface, srcRect, DrawFlags.KeySource | DrawFlags.Wait);
            surface.Dispose();
        }

        public void Clear()
        {
            m_bs.ColorFill(Color.Black);
        }

        public void Flush()
        {
            if (m_isWindowDisplay)
            {
                // window mode
                Rectangle destRect = new Rectangle(_startPos, new Size(m_widthPix, m_heightPix));
                Rectangle srcRect = new Rectangle(0, 0, m_widthPix, m_heightPix);
                m_ps.Draw(destRect, m_bs, srcRect, DrawFlags.Wait);
            }
            else
            {
                m_ps.Flip(m_bs, FlipFlags.DoNotWait); // fullscreen
            }
        }

        public Surface CraeteSurface(Bitmap bitmap)
        {
            Surface surface = null;
            SurfaceDescription desc = new SurfaceDescription();
            desc.Height = bitmap.Height;
            desc.Width = bitmap.Width;
            surface = new Surface(bitmap, desc, m_device);

            return surface;
        }

        public Surface CraeteSurface(Bitmap bitmap, Color colorKey)
        {
            Surface surface = null;
            SurfaceDescription desc = new SurfaceDescription();
            desc.Height = bitmap.Height;
            desc.Width = bitmap.Width;
            surface = new Surface(bitmap, desc, m_device);

            ColorKey key = new ColorKey();
            key.ColorSpaceHighValue = key.ColorSpaceLowValue = colorKey.ToArgb(); //设置透明颜色
            ColorKeyFlags flags = ColorKeyFlags.SourceDraw;
            surface.SetColorKey(flags, key); // 设置页面透明色.

            return surface;
        }

        #region gdi
        //public void DrawText(string text, float x, float y)
        //{
        //    _graphics.DrawString(text, AGRES.DEBUGFONT, Brushes.White, x, y);
        //}

        //public void DrawShadowText(string text, float x, float y)
        //{
        //    _graphics.DrawString(text, AGRES.DEBUGFONT, Brushes.Gray, x + 1, y);
        //    _graphics.DrawString(text, AGRES.DEBUGFONT, Brushes.Gray, x, y + 1);
        //    _graphics.DrawString(text, AGRES.DEBUGFONT, Brushes.Gray, x + 1, y + 1);
        //    _graphics.DrawString(text, AGRES.DEBUGFONT, Brushes.Red, x, y);
        //}

        //public void DrawRectangle(float x, float y, float w, float h)
        //{
        //    _graphics.DrawRectangle(Pens.Green, x, y, w, h);
        //}

        //public void DrawBlock(float x, float y, float w, float h)
        //{
        //    _graphics.DrawRectangle(Pens.Red, x, y, w, h);
        //    _graphics.DrawLine(Pens.Red, x, y, x + w, y + h);
        //    _graphics.DrawLine(Pens.Red, x, y + h, x + w, y);
        //}

        //public void DrawImage(Bitmap image, float x, float y, float width, float height, float originalWidth, float originalHeight)
        //{
        //    ImageAttributes attr = new ImageAttributes();
        //    attr.SetColorKey(Color.Black, Color.Black);
        //    _graphics.DrawImage(image, new Rectangle((int)x, (int)y, (int)width, (int)height), 0, 0, originalWidth, originalHeight, GraphicsUnit.Pixel, attr);
        //}

        //public void DrawImage(Bitmap image, float x, float y, float width, float height, float srcX, float srcY, float originalWidth, float originalHeight)
        //{
        //    ImageAttributes attr = new ImageAttributes();
        //    attr.SetColorKey(Color.Black, Color.Black);
        //    _graphics.DrawImage(image, new Rectangle((int)x, (int)y, (int)width, (int)height), srcX, srcY, originalWidth, originalHeight, GraphicsUnit.Pixel, attr);
        //}

        //public void Clear()
        //{
        //    _graphics.Clear(Color.Black);
        //}

        //public void Flush()
        //{
        //    _primaryGraphics.DrawImage(_image, 0, 0);
        //}
        #endregion

    }
}
