using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Interfaces
{
    internal interface ISlotPoolResourceFactory
    {
        IShaderResource CreateResource();
        void SetResource(IShaderResource resource);
    }
}
