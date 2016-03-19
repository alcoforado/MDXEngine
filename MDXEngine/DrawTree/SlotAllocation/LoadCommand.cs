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
        private class LoadCommand : ILoadCommand
        {
            private SlotAllocation AllocationInfo;
            private SlotResourceProvider _provider;
            public string SlotName { get; set; }
            public object Data { get; set; }


            public LoadCommand(SlotRequest request, SlotResourceProvider provider)
            {
                this.SlotName  = request.SlotName;
                this.Data = request.Data;
                AllocationInfo = null;
                _provider = provider;
            }

            public void Load()
            {
                var pool = _provider._pools[this.SlotName];
                var alloc = this.AllocationInfo;
                var hlsl = _provider._hlsl;

                this.AllocationInfo = pool.Allocate(hlsl,this, this.AllocationInfo);

            }



        }
    }
}
