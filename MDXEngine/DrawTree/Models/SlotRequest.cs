using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree
{
    public class SlotRequest : ISlotRequest
    {
        public string SlotName { get; set; }

        public object Data
        { get; set; }


        public SlotRequest(string slotName, IShaderResource resource)
        {
            this.SlotName = slotName;
            this.Data = resource;
        }

        public SlotRequest() 
        { 
            
        }



    }

    
}
