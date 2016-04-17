using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree.SlotAllocation
{
    internal class SlotAllocation
    {
        public SlotPool.SlotAllocationHandler SlotRequest { get; internal set; }
        public IShaderResource Resource { get; internal set; }
        public bool Discarded { get; set; }

        public SlotAllocation()
        {
            Discarded = false;
        }
    }

}
