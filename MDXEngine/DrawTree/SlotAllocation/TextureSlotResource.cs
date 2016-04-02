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
    internal class TextureSlotResource :  SlotResourceProvider.LoadCommandBase, ITextureSlotResource
    {
        private readonly Texture _resource;
        private readonly Bitmap _resourceKey;
        private bool _disposed;
        private SlotAllocationInfo _alloc;
        public TextureSlotResource(string SlotName, Bitmap bp, SlotResourceProvider provider)
        :base(SlotName,provider)
        {
            _disposed = false;
            _resourceKey = bp;
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
            if (_alloc.CurrentResource == _resource)
                return;
            else
            {
                _resource.Bind(this.GetHLSL(),this.SlotName);
                _alloc.CurrentResource = _resource;
            }
        }

        


        public void Dispose()
        {
            
            this.GetBitmapCache().DecrementCacheReferenceCount(_resourceKey);
            _disposed = true;
        }

        public override IShaderResource LoadData(IShaderResource resource)
        {
            throw new NotImplementedException();
        }




    }
}
