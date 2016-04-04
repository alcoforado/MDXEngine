using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;

namespace MDXEngine.DrawTree.SlotAllocation
{
    public class BitmapCache
    {
        private readonly Dictionary<Bitmap, BitmapCacheEntry> _bitmapCash; 

        public  class BitmapCacheEntry
        {
            public Texture Resource { get; set; }
            public int ReferenceCount { get; set; }
        }


        public BitmapCache()
        {
            _bitmapCash = new Dictionary<Bitmap, BitmapCacheEntry>();
        }

        public Texture GetCacheAndIncrementReferenceCount(Bitmap bp)
        {
            if (_bitmapCash.ContainsKey(bp))
            {
                var entry = _bitmapCash[bp];
                entry.ReferenceCount++;
                return entry.Resource;
            }
            else
            {
                return null;
            }
        }




        internal void AddIntoCash(Bitmap bp, Texture texture)
        {
            if (_bitmapCash.ContainsKey(bp))
                throw new Exception("Cache entry already exists for this bitmap");
            _bitmapCash.Add(bp,new BitmapCacheEntry()
            {
                ReferenceCount = 1,
                Resource = texture
            });

        }

        internal int ReferenceCount(Bitmap resourceKey)
        {
            if (!_bitmapCash.ContainsKey(resourceKey))
                throw new Exception("Bitmap not in cache");
            return _bitmapCash[resourceKey].ReferenceCount;
        }

        internal void DecrementCacheReferenceCount(Bitmap resourceKey)
        {
            System.Diagnostics.Debug.Assert(_bitmapCash.ContainsKey(resourceKey));

            var entry = _bitmapCash[resourceKey];

            entry.ReferenceCount--;
            if (entry.ReferenceCount == 0)
            {
                entry.Resource.Dispose();
                _bitmapCash.Remove(resourceKey);
            }

        }
    }
}
