using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree.SlotAllocation
{
    public partial class SlotResourceProvider
    {
        internal abstract class LoadCommandBase 
        {

            public SlotAllocation AllocationInfo
            {
                get; set;
            }

            private SlotResourceProvider _provider;
            public string SlotName { get; set; }
            public object Data { get; set; }


            public LoadCommandBase(string SlotName, SlotResourceProvider provider)
            {
                this.SlotName = SlotName;
                AllocationInfo = null;
                _provider = provider;
            }

            public void Load()
            {
                var pool = _provider._pools[this.SlotName];
                var alloc = this.AllocationInfo;
                var hlsl = _provider._hlsl;

                this.AllocationInfo = pool.Allocate(this);

            }

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

            public IShaderProgram GetHLSL()
            {
                return _provider._hlsl;
            }

        }
    }
}
