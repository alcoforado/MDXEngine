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
    /*    int padding_bottom;
        int padding_right;
        int padding_top;
        int padding_left;
        int use_alpha_channel;
        StringAlignment alignment;*/
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
            if (_graphics != null)
                _graphics.Dispose();
            if (_bitmap != null)
                _bitmap.Dispose();
            _bitmap   = new Bitmap(width,height, PixelFormat.Format32bppArgb);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.PageUnit = GraphicsUnit.Pixel;
            _graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;    
        }


        public Bitmap RenderText(String text, Font font, TextWriteOptions options = null)
        {
            
            var sizef = _graphics.MeasureString(text, font, new PointF(0.0f, 0.0f), StringFormat.GenericDefault);

             //Calculate the width height of the bitmap with the padding_bottom
            var  width = (int) sizef.Width + options.padding_left + options.padding_right;
            var  height = (int)sizef.Height + options.padding_top + options.padding_bottom;
                    
                 
            if (height  > _bitmap.Height || width  > _bitmap.Width)
            {
                this.ResizeBuffer(
                    Math.Max((int)height, _bitmap.Width),
                    Math.Max((int)width,  _bitmap.Height));
            }
            var rect = new RectangleF(
                       (float)options.padding_left,
                       (float)options.padding_top,
                       sizef.Width,
                       sizef.Height);
            var brush = new SolidBrush(Color.Red);
            _graphics.DrawString(text, font, brush, rect);

            return _bitmap.Clone(new Rectangle(new Point(0, 0), new Size((int) width, (int) height)),
                _bitmap.PixelFormat);
        }
    }
}
