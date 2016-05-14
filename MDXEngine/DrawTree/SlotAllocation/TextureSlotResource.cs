using System;
using MDXEngine.Interfaces;
using MDXEngine.Textures;
using RectangleF = SharpDX.RectangleF;

namespace MDXEngine.DrawTree.SlotAllocation
{
    internal class TextureSlotResource :  SlotResourceProvider.LoadCommandBase, ITextureSlotResource
    {
        private readonly Texture _resource;
        private readonly IBitmap _bitmap;
        private bool _disposed;
        private SlotAllocationInfo _alloc;
        public TextureSlotResource(string SlotName, IBitmap bp, SlotResourceProvider provider)
        :base(SlotName,provider)
        {
            _disposed = false;
            _bitmap = bp;
            //if bitmap is in cash reuse the shader resource already allocated for this bitmap
            _resource = this.GetBitmapCache().GetCacheAndIncrementReferenceCount(bp);
            if (_resource == null)
            {
                _resource = new Texture(this.GetDxContext());
                _resource.LoadFromBitmap(bp);
                this.GetBitmapCache().AddIntoCash(bp, _resource);
            }
            _alloc = this.GetSlotAllocationInfo(this.SlotName);
        }

        public override void Load()
        {
            if (_disposed)
                throw new Exception(String.Format("Cannot load TextureSlotResource for slot {0}. Resource is disposed",this.SlotName));
            if (_alloc.CurrentResource != _resource)
            {
                _resource.Bind(this.GetHLSL(),this.SlotName);
                _alloc.CurrentResource = _resource;
            }
        }

        public override bool CanBeOnSameSlot(ILoadCommand command)
        {
            if (command.GetType() == typeof(TextureSlotResource))
            {
                return ((TextureSlotResource)command)._resource == this._resource;
            }
            else
            {
                return false;
            }
        }


        public void Dispose()
        {
            
            this.GetBitmapCache().DecrementCacheReferenceCount(_bitmap);
            _disposed = true;
        }

    

        public RectangleF BitmapRegion
        {
            get
            {
                System.Diagnostics.Debug.Assert(!_disposed);
                return new RectangleF(0.0f,0.0f,1.0f,1.0f);
            }


        }
    }
}
