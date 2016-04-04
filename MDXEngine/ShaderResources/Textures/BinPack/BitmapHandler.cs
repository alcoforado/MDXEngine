using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDXEngine.Interfaces;
using MDXEngine.Textures;
using SharpDX;
using Rectangle = System.Drawing.Rectangle;

namespace MDXEngine.ShaderResources.Textures.BinPack
{
    public class BitmapHandler : IBitmapHandler
    {
        public BitmapAtlas Owner { get; set; }
        public Bitmap Bitmap { get; set; }
        public Rectangle Rectangle { get; set; }
        public bool RegionSet { get; set; }
        public bool IsDisposed { get; set; }
        public BitmapHandler(BitmapAtlas owner, Bitmap bitmap)
        {
            Owner = owner;
            Bitmap = bitmap;
            RegionSet = false;
            IsDisposed = false;
        }

        public SharpDX.RectangleF GetAtlasNormalizedRegion()
        {
            if (!RegionSet)
                throw new Exception("Region for the bitmap in the atlas not defined yet");
            if (IsDisposed)
                throw new Exception("Atlas was disposed");
            return new SharpDX.RectangleF(
                ((float) Rectangle.X)/((float) Bitmap.Width) ,
                ((float)Rectangle.Y) / ((float)Bitmap.Height),
                ((float)Rectangle.Width) / ((float)Bitmap.Width),
                ((float)Rectangle.Height) / ((float)Bitmap.Height)
            );
        }


    }
}
