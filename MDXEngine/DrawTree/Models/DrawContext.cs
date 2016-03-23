using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree
{
    public class DrawContext<T> : IDrawContext<T>
    {
        private ISlotResourceProvider _slotResourceProvider;


        public SubArray<T> Vertices { get; internal set; }
        public IArray<int> Indices { get; internal set; }
        public TopologyType TopologyType { get; internal set; }

        

        public DrawContext(ISlotResourceProvider provider)
        {
            _slotResourceProvider = provider;
        }


        public IConstantBufferSlotResource<T1> CreateConstantBuffer<T1>(string slotName, T1 data) where T1 : struct
        {
            var result = _slotResourceProvider.CreateConstantBuffer<T1>(slotName, data); 
            
        }

        public ITextureSlotResource CreateTexture(string slotName, string fileName)
        {
            throw new NotImplementedException();
        }
    }



}
