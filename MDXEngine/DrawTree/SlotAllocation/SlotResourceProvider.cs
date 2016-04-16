using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MDXEngine.Interfaces;
using MDXEngine.Textures;
using SharpDX.D3DCompiler;

namespace MDXEngine.DrawTree.SlotAllocation
{

    

    public partial  class SlotResourceProvider : IDisposable
    {
        private readonly IShaderProgram _hlsl;
        private readonly Dictionary<string,SlotPool> _pools;
        private readonly Dictionary<string, SlotAllocationInfo> _slotsAllocationInfo;
        private readonly Dictionary<string, BitmapAtlas> _textureAtlasCollection;


        private readonly BitmapCache _bitmapCash; 

        public void Dispose()
            {
                foreach (var pool in _pools)
                {
                    pool.Value.Dispose();
                }
            }

        private void ValidateSlotForTexture(string slotName)
        {
            if (!_slotsAllocationInfo.ContainsKey(slotName))
                throw new Exception(String.Format("Slot {0} does not exist", slotName));
            if (_slotsAllocationInfo[slotName].Description.ResourceType != ShaderInputType.Texture && _slotsAllocationInfo[slotName].Description.ResourceType != ShaderInputType.TextureBuffer)
                throw new Exception(String.Format("Slot {0} is not a Texture", slotName));
        }


        public Dictionary<string, BitmapAtlas>  GetBitmapAtlasCollection()
        {
            return _textureAtlasCollection;
        }


       



        public SlotResourceProvider(IShaderProgram hlsl)
        {
            _hlsl = hlsl;
            _textureAtlasCollection = new Dictionary<string, BitmapAtlas>();

            //create pools for non texture types slots
            _pools = _hlsl.ProgramResourceSlots.ToList()
                .Where(x=>x.ResourceType != ShaderInputType.Texture && x.ResourceType != ShaderInputType.TextureBuffer)
                .ToDictionary(x => x.Name, x => new SlotPool(x));

            
            //create bitmap cache to never allocate a texture more than once for a single bitmap. 
            _bitmapCash = new BitmapCache();

            _slotsAllocationInfo = _hlsl.ProgramResourceSlots.ToList()
                .ToDictionary(x => x.Name, x=>new SlotAllocationInfo()
                {
                    CurrentResource = null,
                    Description =  x
                });


        }

        


        internal ConstantBufferSlotResource<T> RequestConstantBuffer<T>(string slotName, T data) where T : struct
        {
            //Validate data
            if (_pools[slotName].Slot.ResourceType != ShaderInputType.ConstantBuffer)
                throw new Exception(String.Format("Slot {0} is not a constant buffer",slotName));
            if (!_pools.ContainsKey(slotName))
                throw new Exception(String.Format("Slot name {0} not found",slotName));
            if (_pools[slotName].Slot.DataType.FullName != typeof(T).FullName)
               throw new Exception(String.Format("Slot name {0}  has type {1} but data is of type {2}",slotName, _pools[slotName].Slot.DataType,typeof(T)));

            return new ConstantBufferSlotResource<T>(slotName, data, this);            
             
        }




        internal TextureSlotResource RequestTexture(string slotName, Bitmap bp)
        {
           ValidateSlotForTexture(slotName);
            return new TextureSlotResource(slotName,bp,this);
        }

       



        internal TextureAtlasRegionSlotResource RequestTextureForAtlas(string slotName, string atlasId, Bitmap bp)
        {
            ValidateSlotForTexture(slotName);
            return new TextureAtlasRegionSlotResource(slotName,atlasId,bp,this);
            
        }


    }

 }
