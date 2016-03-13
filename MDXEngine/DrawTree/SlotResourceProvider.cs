﻿using MDXEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.ShaderResources;

namespace MDXEngine.DrawTree
{

    public interface ILoadCommand
    {
        SlotData SlotData { get; set; }
        void Load();
    }


    public class SlotResourceProvider
    {
        private readonly HLSLProgram _hlsl;
        private readonly Dictionary<string,SlotPool> _pools;
        private readonly IShaderResourceFactory _shaderResourceFactory;

        private class LoadCommand : ILoadCommand
        {
            private SlotAllocation AllocationInfo;
            private SlotResourceProvider _provider;
            public SlotData SlotData { get; set; }
            public LoadCommand(SlotData data,SlotResourceProvider provider)
            {
                this.SlotData = data;
                AllocationInfo = null;
                _provider = provider;
            }

            public void Load()
            {
                var pool =  _provider._pools[this.SlotData.SlotName];
                var alloc = this.AllocationInfo;
                var hlsl = _provider._hlsl;
                //If AllocationInfo is not null than the data is already loaded 
                //in a buffer in the gpu you just need to bind it.
                //We also have to check if the AllocationInfo has a buffer (Resource) associated with it
                //if not then the AllocationInfo is invalid because it does not belong to any pool anymore. Probably because the pool size
                //changed by the client. 
                if (alloc != null && alloc.Resource != null)
                {
                       System.Diagnostics.Debug.Assert(this.AllocationInfo.SlotData == this.SlotData);
                        if (pool.CurrentBindedBuffer != alloc)
                        {
                            alloc.Resource.Bind(SlotData.SlotName); //bind
                        }
                        pool.Pool.Remove(alloc);
                        pool.Pool.Add(alloc);
                }
                    




            }
            
        }


        public SlotResourceProvider(IShaderResourceFactory resourceFactory,HLSLProgram hlsl)
        {
            _hlsl = hlsl;

            _pools = _hlsl.ProgramResourceSlots.ToList().ToDictionary(x => x.Name, x => new SlotPool(resourceFactory,x));
        }


        public ILoadCommand CreateLoadCommand(SlotData data)
        {
            return new LoadCommand(data, this);
        }


    }

    /// The pool of buffers associated with one slot.
    /// Every HLSL slot has a pool of buffers associated to it.
    /// As the draw tree add elements to the slots they are "cached"
    /// in the buffers. So next time they use the data, they will be 
    /// already in the gpu. The maximum number of buffers (elements that 
    /// can be cached) is defined by the PoolSize argument. A value of 0
    /// means "unlimited"
    internal  class SlotPool
    {
        private readonly IShaderResourceFactory _shaderResourceFactory;
        private SlotDescription Slot;

        public int NextBufferAvailableIndex { get; set; } /*The Buffer (Resource) currently binded to the slot*/
        public SlotAllocation CurrentBindedBuffer { get; set; } /*The Buffer (Resource) currently binded to the slot*/


        
        public LinkedList<SlotAllocation> Pool;  
        
        public uint PoolSize { get; set; }


        public bool CanExpand()
        {
            return Pool.Count < PoolSize;
        }

        private void SetPoolSize(uint size)
        {
            if (size == 0)
                throw new InvalidOperationException("Pool size must be a value greater than 0");

            if (size >= PoolSize)
            {
                PoolSize = size;
            }
            else
            {
                if (CurrentBindedBuffer!= null)
                    Pool.Remove(CurrentBindedBuffer);
                if (Pool.Count > size -1)
                {
                    
                    var newPool = new LinkedList<SlotAllocation>();
                    
                    foreach (var elem in Pool)
                    {
                        newPool.
                    }

                }
                
                


                if (Pool[i] != CurrentBindedBuffer) //make sure this is not the only  one resource assigned to the shader slot
                {
                    Pool[i].Resource.Dispose();
                    Pool[i].Resource = null;
                    Pool.RemoveAt(i);
                    i = 0;
                }


                int i=0;
                while (Pool.Count > size)
                {
                    if (Pool[i] != CurrentBindedBuffer) //make sure this is not the only  one resource assigned to the shader slot
                    {
                        Pool[i].Resource.Dispose();
                        Pool[i].Resource = null;
                        Pool.RemoveAt(i);
                        i = 0;
                    }
                    else
                        i++; //try next
                }
                PoolSize = size;
            }

        }


       
        
        /// <summary>
        /// Create the pool of buffers associated with one slot.
        /// Every HLSL slot has a pool of buffers associated to it.
        /// As the draw tree add elements to the slots they are "cached"
        /// in the buffers. So next time they use the data, they will be 
        /// already in the gpu. The maximum number of buffers (elements that 
        /// can be cached) is defined by the PoolSize argument. A value of 0
        /// means "unlimited"
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="poolSize">Max number of buffers (cached elements). A value 0 indicates no limit</param>
        public SlotPool(IShaderResourceFactory shaderResourceFactory,SlotDescription slot, uint  poolSize = UInt32.MaxValue)
        {
            _shaderResourceFactory = shaderResourceFactory;
            this.Slot = slot;
            PoolSize = poolSize;
            PoolIndex = new HashSet<SlotAllocation>();
            NextBufferAvailableIndex = 0;
            CurrentBindedBuffer = null;
        }


        public SlotAllocation Allocate(SlotData slotData, SlotAllocation alloc=null)
        {
            //If AllocationInfo is not null than the data is already loaded 
            //in a buffer in the gpu you just need to bind it.
            //We also have to check if the AllocationInfo has a buffer (Resource) associated with it
            //if not then the AllocationInfo is invalid because it does not belong to any pool anymore. Probably because the pool size
            //changed by the client. 
            if (alloc != null && alloc.Resource != null && alloc.SlotData == slotData)
            {
                if (CurrentBindedBuffer != alloc) //if alloc is not already binded to the slot bind it
                {
                    alloc.Resource.Bind(slotData.SlotName);
                }


                //technacally we should move the allocation (buffer) to the far end of the pool since it is the most recent used buffer
                //now. But this is relevant  only in the situation the pool cannot create anymore allocations, so we have to chose the 
                //least used one.
                if (!this.CanExpand())
                {
                    Pool.Remove(alloc);
                    Pool.Add(alloc);
                }
                return alloc;
            }
            else //need to select or create a new buffer.
            {
                //Can create a new buffer for the slot data?
                if (this.CanExpand())
                {
                    var newAlloc = new SlotAllocation
                    {
                        SlotData = slotData,
                        Resource = _shaderResourceFactory.CreateForSlot(slotData.SlotName)
                    };

                    
                    //Load and Bind
                    newAlloc.Resource.Load(newAlloc.SlotData.Data);
                    newAlloc.Resource.Bind(newAlloc.SlotData.SlotName);

                    //Add at the end of the pool (this is the most recent used allocation)
                    Pool.Add(newAlloc);
                    return newAlloc;
                }
                else //max number of allocations reached. Reuse one allocation
                {
                    //chose the olders allocation (head in the pool)
                    var oldAlloc = Pool.First();
                    
                    //Load and Bind with the alloc
                    oldAlloc.SlotData = slotData;
                    oldAlloc.Resource.Load(slotData.Data);
                    oldAlloc.Resource.Bind(slotData.SlotName);

                    //Send the oldAlloc to the last element of the pool
                    //It is the most recent used now
                    Pool.Remove(oldAlloc);
                    Pool.Add(oldAlloc);

                    return oldAlloc;
                }
            }


        }



    }

    internal class SlotAllocation 
    {
        public SlotData SlotData   { get; internal set; }
        public IShaderResource Resource { get; internal set; }
       
    }




}
