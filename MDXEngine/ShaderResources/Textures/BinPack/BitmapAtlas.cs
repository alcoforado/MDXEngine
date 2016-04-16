
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
       
        private List<BitmapHandler> _handlers;
        private IBitmap _atlas;

        public BitmapAtlas()
        {
           
            _handlers = new List<BitmapHandler>();
            _atlas = null;
        }

        public IBitmapHandler Add(IBitmap bitmap)
        {
            if (_atlas!= null)
                throw new Exception("Atlas is already generated. Extra Bitmap additions are forbidden");
           
            _handlers.Add(new BitmapHandler(this,bitmap));
            return _handlers.Last();
        }
        
        public IBitmap GenerateAtlas()
        {
            if (_atlas != null)
                throw new Exception("Atlas is already generated. Extra Bitmap additions are forbidden");

            BinPackAlghorithm binPack = new BinPackAlghorithm(_handlers.Select(x=>x.Bitmap).Distinct().ToList());
            _atlas = binPack.CreateBitmap();
            var bitmapRegions = binPack.GetBitmapsRegions();


            foreach (var handler in _handlers)
            {
                handler.Rectangle = bitmapRegions[handler.Bitmap];
                handler.RegionSet = true;
            }
            return _atlas;

        }

        public IBitmap GetAtlas()
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

            //dispose all handlers
            foreach (var handler in _handlers)
            {
                handler.Dispose();
            }
        }

        public bool IsClosed()
        {
            return (_atlas != null);
        }
    }
}
