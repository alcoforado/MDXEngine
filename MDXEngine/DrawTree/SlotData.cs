using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree
{
    public class SlotData
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
