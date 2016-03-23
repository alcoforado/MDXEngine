using MDXEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree.SlotAllocation
{
   internal  class ConstantBufferSlotResource<T> : SlotResourceProvider.LoadCommandBase, IConstantBufferSlotResource<T> where T:struct
    {
        T _data;
        public ConstantBufferSlotResource(string slotName,T data,SlotResourceProvider provider)
            :base(slotName,provider)
        {
            _data = data; 
        }

        public override IShaderResource LoadData(IShaderResource resource)
        {
            if (resource == null)
            {
                resource = new CBufferResource<T>(this.GetHLSL().DxContext);
            }
            resource.Load(resource);
            return resource;
        }
    }
}
