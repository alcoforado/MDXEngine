using System;
using System.Collections.Generic;
using System.Linq;
using MDXEngine.Interfaces;
using SharpDX.D3DCompiler;

namespace MDXEngine.DrawTree.SlotAllocation
{



    public partial  class SlotResourceProvider : IDisposable
    {
        private readonly IShaderProgram _hlsl;
        private readonly Dictionary<string,SlotPool> _pools;


        public void Dispose()
            {
                foreach (var pool in _pools)
                {
                    pool.Value.Dispose();
                }
            }

        public SlotResourceProvider(IShaderProgram hlsl)
        {
            _hlsl = hlsl;

            _pools = _hlsl.ProgramResourceSlots.ToList().ToDictionary(x => x.Name, x => new SlotPool(x));
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

     

 

        public ITextureSlotResource RequestTexture(string slotName, string fileName)
        {
            throw new NotImplementedException();
        }
    }

 }
