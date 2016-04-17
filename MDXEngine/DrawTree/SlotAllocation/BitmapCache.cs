using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;
using MDXEngine.Textures;

namespace MDXEngine.DrawTree.SlotAllocation
{
    public class BitmapCache
    {
        private readonly Dictionary<IBitmap, BitmapCacheEntry> _BitmapCash; 

        public  class BitmapCacheEntry
        {
            public Texture Resource { get; set; }
            public int ReferenceCount { get; set; }
        }


        public BitmapCache()
        {
            _BitmapCash = new Dictionary<IBitmap, BitmapCacheEntry>();
        }

        public Texture GetCacheAndIncrementReferenceCount(IBitmap bp)
        {
            if (_BitmapCash.ContainsKey(bp))
            {
                var entry = _BitmapCash[bp];
                entry.ReferenceCount++;
                return entry.Resource;
            }
            else
            {
                return null;
            }
        }




        internal void AddIntoCash(IBitmap bp, Texture texture)
        {
            if (_BitmapCash.ContainsKey(bp))
                throw new Exception("Cache entry already exists for this IBitmap");
            _BitmapCash.Add(bp,new BitmapCacheEntry()
            {
                ReferenceCount = 1,
                Resource = texture
            });

        }

        internal int ReferenceCount(IBitmap resourceKey)
        {
            if (!_BitmapCash.ContainsKey(resourceKey))
                throw new Exception("IBitmap not in cache");
            return _BitmapCash[resourceKey].ReferenceCount;
        }

        internal void DecrementCacheReferenceCount(IBitmap resourceKey)
        {
            System.Diagnostics.Debug.Assert(_BitmapCash.ContainsKey(resourceKey));

            var entry = _BitmapCash[resourceKey];

            entry.ReferenceCount--;
            if (entry.ReferenceCount == 0)
            {
                entry.Resource.Dispose();
                _BitmapCash.Remove(resourceKey);
            }

        }
    }
}
