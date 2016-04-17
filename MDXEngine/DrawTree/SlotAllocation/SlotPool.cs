using System;
using System.Collections.Generic;
using System.Data;
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
        public SlotDescription Slot { get; internal set; }

        public int NextBufferAvailableIndex { get; set; } /*The Buffer (Resource) currently binded to the slot*/
        public SlotAllocation CurrentBindedBuffer { get; set; } /*The Buffer (Resource) currently binded to the slot*/

        private IShaderProgram _program;

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
                        alloc.Discarded = true;
                    }
                    i++;
                }
                Pool = newPool;
            }

        }

        public  class SlotAllocationHandler
        {
            private SlotPool _pool;
            private SlotAllocation _alloc;
            private ISlotPoolResourceFactory _resourceFactory; 
            
            public SlotAllocationHandler(SlotPool pool, SlotAllocation alloc,ISlotPoolResourceFactory resourceFactory)
            {
                _pool = pool;
                _alloc = alloc;
                _resourceFactory = resourceFactory;
                
            }

            public void Update(bool bForceResourceUpdate)
            {
               
                //If AllocationInfo is not null than the data is already loaded 
                //in a buffer in the gpu you just need to bind it.
                //We also have to check if the AllocationInfo has a buffer (Resource) associated with it
                //if not then the AllocationInfo is invalid because it does not belong to any pool anymore. Probably because the pool size
                //changed by the client. 
                if (_alloc != null && !_alloc.Discarded  && _alloc.SlotRequest == this)
                {
                    if (bForceResourceUpdate)
                    {
                       _resourceFactory.SetResource(_alloc.Resource);
                    }


                    if (_pool.CurrentBindedBuffer != _alloc) //if alloc is not already binded to the slot bind it
                    {
                        _alloc.Resource.Bind(_pool._program, _pool.Slot.Name);
                        _pool.CurrentBindedBuffer = _alloc;
                    }


                    //technacally we should move the allocation (buffer) to the far end of the pool since it is the most recent used buffer
                    //now. But this is relevant  only in the situation the pool cannot create anymore allocations, so we have to chose the 
                    //least used one.
                    if (!_pool.CanExpand())
                    {
                        _pool.Pool.Remove(_alloc);
                        _pool.Pool.AddLast(_alloc);
                    }
                   
                }
                else //need to select or create a new buffer.
                {
                    //Can create a new buffer for the slot data?
                    if (_pool.CanExpand())
                    {
                        var newAlloc = new SlotAllocation
                        {
                            SlotRequest = this,
                            Resource = _resourceFactory.CreateResource()
                        };


                        //Load and Bind
                        newAlloc.Resource.Bind(_pool._program, _pool.Slot.Name);
                        _pool.CurrentBindedBuffer = newAlloc;
                        _alloc = newAlloc;



                        //Add at the end of the pool (this is the most recent used allocation)
                        _pool.Pool.AddLast(newAlloc);
                    }
                    else //max number of allocations reached. Reuse one allocation
                    {
                        //chose the olders allocation (head in the pool)
                        var oldAlloc = _pool.Pool.First();

                        //Load and Bind with the alloc
                        oldAlloc.SlotRequest = this;
                        _resourceFactory.SetResource(oldAlloc.Resource);
                        oldAlloc.Resource.Bind(_pool._program, _pool.Slot.Name);
                        _pool.CurrentBindedBuffer = oldAlloc;

                        //Send the oldAlloc to the last element of the pool
                        //It is the most recent used now
                        _pool.Pool.Remove(oldAlloc);
                        _pool.Pool.AddLast(oldAlloc);

                        _alloc = oldAlloc;
                    }
                }
            }


        }



        
        /// <summary>
        /// This is the most important function in slotpool.
        /// It creates a handler responsible to update the resource.
        /// This handler is binded to an interface that is used by the 
        /// pool to create a resource if it is not already in the pool or 
        /// to set the resource with data in case the handler already has a resource.
        ///  </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public SlotAllocationHandler CreateHandler(ISlotPoolResourceFactory factory)
        {
            return new SlotAllocationHandler(this,null,factory);
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
        public SlotPool(SlotDescription slot, IShaderProgram program,uint poolSize = UInt32.MaxValue)
        {
            this.Slot = slot;
            PoolSize = poolSize;
            NextBufferAvailableIndex = 0;
            CurrentBindedBuffer = null;
            Pool = new LinkedList<SlotAllocation>();
            _program = program;
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
