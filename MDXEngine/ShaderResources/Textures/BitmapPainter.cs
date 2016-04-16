using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;
using MDXEngine.ShaderResources.Textures.BinPack;

namespace MDXEngine.ShaderResources.Textures
{
    public class BitmapPainter : IDisposable
    {
        private Graphics _g;
        public BitmapPainter(IBitmap bitmap)
        {
            _g = Graphics.FromImage((bitmap as GDIBitmap)._bitmap);
        }

        public void DrawRectangle(System.Drawing.Color color, Rectangle rect)
        {
            _g.DrawRectangle(new Pen(new SolidBrush(color)),rect);
        }

        public void DrawImage(IBitmap bitmap,Point pt)
        {
            var bp = ((GDIBitmap) bitmap)._bitmap;
            _g.DrawImage(bp, pt);
        }

        public void Dispose()
        {
            _g.Dispose();
        }
    }
}
