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
        private Texture _resource;
        private Bitmap _resourceKey;
        public TextureSlotResource(string SlotName, Bitmap bp, SlotResourceProvider provider)
        :base(SlotName,provider)
        {
            _resourceKey = bp;
            //if bitmap is in cash reuse the shader resource already allocated for this bitmap
            _resource = this.GetBitmapCache().GetCacheAndIncrementReferenceCount(bp);
            if (_resource == null)
            {
                _resource = new Texture(this.GetDxContext());
                _resource.LoadFromBitmap(bp);
                this.GetBitmapCache().AddIntoCash(bp, _resource);
            }
            
        }

        public override void Load()
        {
            
        }


        public void Dispose()
        {
            this.GetBitmapCache().DecrementCacheReferenceCount(_resourceKey);
        }

        public override IShaderResource LoadData(IShaderResource resource)
        {
            throw new NotImplementedException();
        }




    }
}
