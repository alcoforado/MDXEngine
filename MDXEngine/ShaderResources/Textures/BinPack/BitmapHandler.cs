using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDXEngine.Interfaces;
using MDXEngine.Textures;

namespace MDXEngine.ShaderResources.Textures.BinPack
{
    public class BitmapHandler : IBitmapHandler
    {
        public BitmapAtlas Owner { get; set; }
        public Bitmap Bitmap { get; set; }
        public Rectangle Rectangle { get; set; }
        public bool RegionSet { get; set; }
        public BitmapHandler(BitmapAtlas owner, Bitmap bitmap)
        {
            Owner = owner;
            Bitmap = bitmap;
            RegionSet = false;
        }

        public Rectangle AtlasRegion()
        {
            if (!RegionSet)
                throw new Exception("Region for the bitmap in the atlas not defined yet");
            return Rectangle;
        }


    }
}
