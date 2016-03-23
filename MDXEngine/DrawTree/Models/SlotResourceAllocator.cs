using MDXEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree.Models
{
    public class SlotResourceAllocator : ISlotResourceAllocator
    {
        public ISlotResourceProvider _provider;

        public SlotResourceAllocator(ISlotResourceProvider provider)
        {
            _provider = provider;
        }

        public void RequestConstantBuffer<T>(string slotName, T data) where T : struct
        {
            var result=_provider.CreateConstantBuffer(slotName, data);
        }

        public void RequestTexture(string slotName, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
