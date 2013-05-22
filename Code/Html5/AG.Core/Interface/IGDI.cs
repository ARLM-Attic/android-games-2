using System;
using System.Drawing;

public interface IGDI
{
    void Clear();
    void DrawBlock(float x, float y, float w, float h);
    void DrawEllipse(float x, float y, float w, float h);
    void DrawImage(System.Drawing.Bitmap image, float x, float y, float width, float height, float originalWidth, float originalHeight);
    void DrawImage(System.Drawing.Bitmap image, float x, float y, float width, float height, float srcX, float srcY, float originalWidth, float originalHeight);
    void Draw(Texture2D texture, float x, float y, float width, float height);
    void DrawLine(float x1, float y1, float x2, float y2);
    void DrawRectangle(float x, float y, float w, float h);
    void DrawRectangle(int color, float x, float y, float w, float h);
    void DrawShadowText(string text, float x, float y);
    void DrawShadowText(IntPtr font, int color, string text, float x, float y);
    void DrawText(IntPtr font, int color, string text, int x, int y);
    void DrawText(string text, float x, float y);
    void DrawText(Font font, int color, string text, Rectangle rect);
    void DrawShadowText(Font font, int color, string text, Rectangle rect);
    void Flush();

    Texture2D CreateTexture(byte[] data);

    Size2D MeasureStringSize(string text, Font font);
}