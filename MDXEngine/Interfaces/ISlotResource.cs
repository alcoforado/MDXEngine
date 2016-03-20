using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Interfaces
{
    public interface ISlotResource
    {
        string SlotName { get; set; }
        object Data { get; set; }
    }
}
