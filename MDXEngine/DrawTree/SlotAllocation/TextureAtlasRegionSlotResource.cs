using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;
using MDXEngine.Textures;

namespace MDXEngine.DrawTree.SlotAllocation
{
    internal class TextureAtlasRegionSlotResource : SlotResourceProvider.LoadCommandBase, ITextureSlotResource
    {
        private string _atlasId;
        private IBitmapHandler _bpHandler;
        private Texture _texture = null;
        private SlotAllocationInfo _alloc;

        public TextureAtlasRegionSlotResource(string slotName, string atlasId, Bitmap bp,    SlotResourceProvider provider) 
            : base(slotName, provider)
        {
            _atlasId = atlasId;
            var lst = this._provider.GetBitmapAtlasCollection();
            if (!lst.ContainsKey(atlasId))
                lst.Add(atlasId,new BitmapAtlas());
            if (lst[atlasId].IsClosed())
                throw new Exception(String.Format("Atlas is already closed, cannot add bitmap to group {0} for slot {1}",atlasId,slotName));
            
            _bpHandler = lst[atlasId].Add(bp);
            _alloc = this.GetSlotAllocationInfo(this.SlotName);

        }

        public override void Load()
        {
            var atlas = _provider.GetBitmapAtlasCollection()[_atlasId];
            
            
            //If the atlas is not generated yet than this texture resource is the first bitmap region of the atlas 
            //to be loaded in memory. Generate the atlas.
            if (!atlas.IsClosed())
            {
                var bp = atlas.GenerateAtlas();
                _texture = new Texture(this.GetDxContext());
                _texture.CopyFromBitmap(bp);
                this.GetBitmapCache().AddIntoCash(bp,_texture); 
            }
            //Atlas is generated, We need just to  bind the texture shader resource to the slot
            if (_texture == null)
            {
                //Ooops it is the first time we are loading this TextureAtlasRegion.
                //Let's get a reference to the texture
                _texture = this.GetBitmapCache().GetCacheAndIncrementReferenceCount(atlas.GetAtlas());
            }

            //Bind the texture if it is not already binded
            if (_alloc.CurrentResource != _texture)
            {
                _texture.Bind(this.GetHLSL(), this.SlotName);
                _alloc.CurrentResource = _texture;
            }
        }


        public override IShaderResource LoadData(IShaderResource resource)
        {
            throw new NotImplementedException();
        }
    }
}
