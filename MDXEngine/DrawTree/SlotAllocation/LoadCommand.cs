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
    public partial class SlotResourceProvider
    {
        internal abstract class LoadCommandBase : ILoadCommand
        {

            public SlotAllocation AllocationInfo
            {
                get; set;
            }

            protected SlotResourceProvider _provider;
            public string SlotName { get; set; }
            public object Data { get; set; }


            public LoadCommandBase(string SlotName, SlotResourceProvider provider)
            {
                this.SlotName = SlotName;
                AllocationInfo = null;
                _provider = provider;
            }

            public virtual void Load()
            {
                var pool = _provider._pools[this.SlotName];

                this.AllocationInfo = pool.Allocate(this);

            }

            public abstract bool CanBeOnSameSlot(ILoadCommand command);
            

            /// <summary>
           /// The only method  the other load commands need to override. 
           /// The caching system will try to allocate a resource for the 
           /// object to load data into. If the caching system decides that
           /// a  new resource need to be created  the chaching system will 
           /// call this function with null parameter indicating that the load command
           /// will have to create the resource  by itself. 
           /// The method always return the IShaderResource where the data is been loaded.
           /// </summary>
           /// <param name="resource"></param>
           /// <returns>The shaderresource just created</returns>
           public abstract IShaderResource LoadData(IShaderResource resource);
            
            protected SlotPool GetSlotPool(string slotName)
            {
                return _provider._pools[this.SlotName];
            }

            protected SlotAllocationInfo GetSlotAllocationInfo(string slotName)
            {
                return _provider._slotsAllocationInfo[slotName];
            }


            protected BitmapCache  GetBitmapCache()
            {
                return _provider._bitmapCash;
            }


            public IShaderProgram GetHLSL()
            {
                return _provider._hlsl;
            }

            public IDxContext GetDxContext()
            {
                return _provider._hlsl.DxContext;
            }

           
        }

       
    }
}
