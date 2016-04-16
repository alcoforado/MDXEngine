using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using MDXEngine.Interfaces;

namespace MDXEngine.ShaderResources.Textures.BinPack
{
    internal class GDIBitmap : IBitmap
    {
        public Bitmap _bitmap;
        private int RefCount;


        public GDIBitmap(int width, int height)
        {
            _bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            RefCount = 1;
        }

        public GDIBitmap(Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new Exception("Pixel Format 32bppArgb is the only bitmap format accepted for now");
            _bitmap = bitmap;
            RefCount = 1;
        }

        public int Width { get { return Width; } }

        public int Height{ get { return Height; }}
    
    

        public void Dispose()
        {
            if (RefCount <= 0)
            {
                throw new Exception("Object is already disposed");
            }
            RefCount--;
            if (RefCount == 0)
            {
                _bitmap.Dispose();
            }
        }

        public void IncRefCount()
        {
            RefCount++;
        }


        public void DrawImage(IBitmap bitmap, Point location)
        {
            throw new NotImplementedException();
        }
    }
}
