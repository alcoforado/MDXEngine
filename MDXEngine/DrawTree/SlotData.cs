using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree
{
    public class SlotData : ISlotData
    {
        public string SlotName { get; set; }

        public object Data
        { get; set; }


        public SlotData(string slotName, IShaderResource resource)
        {
            this.SlotName = slotName;
            this.Data = resource;
        }

        public SlotData() 
        { 
            
        }



    }

    
}
