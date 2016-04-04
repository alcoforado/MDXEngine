
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;
using MDXEngine.ShaderResources.Textures.BinPack;
using MDXEngine.Textures.BinPack;

namespace MDXEngine.Textures
{
    /// <summary>
    /// Encapsulate a collection of bitmaps in just one bitmap using the BinPackAlghorithm
    /// </summary>
    public class BitmapAtlas
    {
        private List<Bitmap> _bitmaps;
        private List<BitmapHandler> _handlers;
        public Bitmap _atlas;

        public BitmapAtlas()
        {
            _bitmaps = new List<Bitmap>();
            _handlers = new List<BitmapHandler>();
            _atlas = null;
        }

        public IBitmapHandler Add(Bitmap bitmap)
        {
            if (_atlas!= null)
                throw new Exception("Atlas is already generated. Extra Bitmap additions are forbidden");
            _bitmaps.Add((Bitmap) bitmap.Clone());
            _handlers.Add(new BitmapHandler(this,bitmap));
            return _handlers.Last();
        }

  


        public Bitmap GenerateAtlas()
        {
            if (_atlas != null)
                throw new Exception("Atlas is already generated. Extra Bitmap additions are forbidden");

            BinPackAlghorithm binPack = new BinPackAlghorithm(_bitmaps.Distinct().ToList());
            _atlas = binPack.CreateBitmap();
            var bitmapRegions = binPack.GetBitmapsRegions();


            foreach (var handler in _handlers)
            {
                handler.Rectangle = bitmapRegions[handler.Bitmap];
                handler.RegionSet = true;
            }
            return _atlas;

        }

        public Bitmap GetAtlas()
        {
            if (_atlas == null)
            {
                throw new Exception("Atlas is not generated yet, please call method GenerateAtlas() to create the atlas");
            }
            return _atlas;
        }

        public void Dispose()
        {
            if (_atlas!=null)
                _atlas.Dispose();

            //dispose all bitmaps
            foreach (var bp in _bitmaps)
            {
                bp.Dispose();
            }
        }

        public bool IsClosed()
        {
            return (_atlas != null);
        }
    }
}
