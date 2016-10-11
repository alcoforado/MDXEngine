﻿using System;
using MDXEngine.Interfaces;
using MDXEngine.Textures;
using RectangleF = SharpDX.RectangleF;

namespace MDXEngine.DrawTree.SlotAllocation
{
    internal class TextureAtlasRegionSlotResource : SlotResourceProvider.LoadCommandBase, ITextureSlotResource
    {
        private string _atlasId;
        private IBitmapHandler _bpHandler;
        private Texture _texture = null;
        private SlotAllocationInfo _alloc;

        public TextureAtlasRegionSlotResource(string slotName, string atlasId, IBitmap bp,    SlotResourceProvider provider) 
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

        public override bool CanBeOnSameSlot(ILoadCommand command)
        {
            if (command.GetType() == typeof(TextureAtlasRegionSlotResource))
            {
                return ((TextureAtlasRegionSlotResource) command)._atlasId == this._atlasId;
            }
            else
            {
                return false;
            }
        }


       

        public RectangleF BitmapRegion
        {
            get { return _bpHandler.GetAtlasNormalizedRegion(); }
        }


        public void Dispose()
        {
            var atlas = _provider.GetBitmapAtlasCollection()[_atlasId];
            if (this.GetBitmapCache().ReferenceCount(atlas.GetAtlas()) == 1)
            {
                this.GetBitmapCache().DecrementCacheReferenceCount(atlas.GetAtlas());
                _provider.GetBitmapAtlasCollection().Remove(_atlasId);
                atlas.Dispose();
            }
            else
            {
                this.GetBitmapCache().DecrementCacheReferenceCount(atlas.GetAtlas());
            }
        }
    }
}