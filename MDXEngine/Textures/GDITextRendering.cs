using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Textures
{
    public class TextOptions
    {
        int padding_bottom;
        int padding_right;
        int padding_top;
        int padding_left;
        int use_alpha_channel;
        StringAlignment alignment;
    };

    public class GDITextRendering
    {
        Bitmap _bitmap;
        Graphics _graphics;

        public GDITextRendering(int initial_width=600,int initial_height=200)
        {
            this.ResizeBuffer(initial_width, initial_height);

        }
        void ResizeBuffer(int width, int height)
        {
            _bitmap   = new Bitmap(width,height, PixelFormat.Format32bppArgb);
            
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.PageUnit = GraphicsUnit.Pixel;
            _graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;    
        }


        public Bitmap RenderText(String text, Font font, TextOptions options = null)
        {
            
            var size = _graphics.MeasureString(text, font, new PointF(0.0f, 0.0f), StringFormat.GenericDefault);
            if (size.Height > _bitmap.Height || size.Width > _bitmap.Width)
            {
                this.ResizeBuffer(
                    Math.Max((int)size.Width, _bitmap.Width),
                    Math.Max((int)size.Height, _bitmap.Height));

            }
            var brush = new SolidBrush(Color.Red);
            _graphics.DrawString(text, font, brush, new PointF(0.0f, 0.0f));

            return _bitmap.Clone(new Rectangle(new Point(0, 0), new Size((int) size.Width, (int) size.Height)),
                _bitmap.PixelFormat);
        }
    }
}
