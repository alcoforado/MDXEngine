using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree.SlotAllocation
{
    public partial class SlotResourceProvider
    {
        private class LoadCommand : ILoadCommand
        {
            private SlotAllocation AllocationInfo;
            private SlotResourceProvider _provider;
            public SlotData SlotData { get; set; }

            public LoadCommand(SlotData data, SlotResourceProvider provider)
            {
                this.SlotData = data;
                AllocationInfo = null;
                _provider = provider;
            }

            public void Load()
            {
                var pool = _provider._pools[this.SlotData.SlotName];
                var alloc = this.AllocationInfo;
                var hlsl = _provider._hlsl;

                this.AllocationInfo = pool.Allocate(this.SlotData, this.AllocationInfo);

            }



        }
    }
}
