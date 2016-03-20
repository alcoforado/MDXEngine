using System;
using System.Collections.Generic;
using System.Linq;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree.SlotAllocation
{
    /// The pool of buffers associated with one slot.
    /// Every HLSL slot has a pool of buffers associated to it.
    /// As the draw tree add elements to the slots they are "cached"
    /// in the buffers. So next time they use the data, they will be 
    /// already in the gpu. The maximum number of buffers (elements that 
    /// can be cached) is defined by the PoolSize argument. A value of 0
    /// means "unlimited"
    internal class SlotPool : IDisposable
    {
        private readonly IShaderResourceFactory _shaderResourceFactory;
        public SlotDescription Slot { get; internal set; }

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
                var newPool = new LinkedList<SlotAllocation>();
                int i = 0;
                if (CurrentBindedBuffer != null)
                {
                    Pool.Remove(CurrentBindedBuffer);
                    newPool.AddFirst(CurrentBindedBuffer);
                    i = 1;
                }
                foreach (var alloc in Pool)
                {
                    if (i < size)
                        newPool.AddFirst(alloc); //add to the new pool
                    else
                    {
                        alloc.Resource.Dispose();
                        alloc.Resource = null;
                    }
                    i++;
                }
                Pool = newPool;
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
        public SlotPool(IShaderResourceFactory shaderResourceFactory, SlotDescription slot, uint poolSize = UInt32.MaxValue)
        {
            _shaderResourceFactory = shaderResourceFactory;
            this.Slot = slot;
            PoolSize = poolSize;
            NextBufferAvailableIndex = 0;
            CurrentBindedBuffer = null;
        }


        public SlotAllocation Allocate(HLSLProgram program, ISlotResource slotRequest, SlotAllocation alloc = null)
        {
            //If AllocationInfo is not null than the data is already loaded 
            //in a buffer in the gpu you just need to bind it.
            //We also have to check if the AllocationInfo has a buffer (Resource) associated with it
            //if not then the AllocationInfo is invalid because it does not belong to any pool anymore. Probably because the pool size
            //changed by the client. 
            if (alloc != null && alloc.Resource != null && alloc.SlotRequest == slotRequest)
            {
                if (CurrentBindedBuffer != alloc) //if alloc is not already binded to the slot bind it
                {
                    alloc.Resource.Bind(program,slotRequest.SlotName);
                }


                //technacally we should move the allocation (buffer) to the far end of the pool since it is the most recent used buffer
                //now. But this is relevant  only in the situation the pool cannot create anymore allocations, so we have to chose the 
                //least used one.
                if (!this.CanExpand())
                {
                    Pool.Remove(alloc);
                    Pool.AddLast(alloc);
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
                        SlotRequest = slotRequest,
                        Resource = _shaderResourceFactory.CreateForSlot(slotRequest.SlotName)
                    };


                    //Load and Bind
                    newAlloc.Resource.Load(newAlloc.SlotRequest.Data);
                    newAlloc.Resource.Bind(program,newAlloc.SlotRequest.SlotName);

                    //Add at the end of the pool (this is the most recent used allocation)
                    Pool.AddLast(newAlloc);
                    return newAlloc;
                }
                else //max number of allocations reached. Reuse one allocation
                {
                    //chose the olders allocation (head in the pool)
                    var oldAlloc = Pool.First();

                    //Load and Bind with the alloc
                    oldAlloc.SlotRequest = slotRequest;
                    oldAlloc.Resource.Load(slotRequest.Data);
                    oldAlloc.Resource.Bind(program,slotRequest.SlotName);

                    //Send the oldAlloc to the last element of the pool
                    //It is the most recent used now
                    Pool.Remove(oldAlloc);
                    Pool.AddLast(oldAlloc);

                    return oldAlloc;
                }
            }


        }

        public void Dispose()
        {
            foreach (var elem in Pool)
            {
                elem.Resource.Dispose();
                elem.Resource = null;
            }
        }


    }
}
