﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree
{
    public class ResourceLoadCommand
    {
        public string SlotName { get; set; }

        public IShaderResource Resource { get; set; }


        public ResourceLoadCommand(string slotName, IShaderResource resource)
        {
            this.SlotName = slotName;
            this.Resource = resource;
        }

        public ResourceLoadCommand() { }
    }

    
}
