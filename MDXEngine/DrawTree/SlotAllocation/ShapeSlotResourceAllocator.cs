using MDXEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree.SlotAllocation
{
    class ShapeSlotResourceAllocator : ISlotResourceAllocator
    {
        readonly LoadCommandsSequence _loadSequence;
        readonly SlotResourceProvider _globalProvider;
        readonly IShaderProgram _program;

        public ShapeSlotResourceAllocator(IShaderProgram program,LoadCommandsSequence loadSequence,SlotResourceProvider provider)
        {
            this._program = program;
            _globalProvider = provider;
            _loadSequence = loadSequence;
        }

        public ShapeSlotResourceAllocator(IShaderProgram program, SlotResourceProvider slotResourceProvider)
        {
            this._program = program;
            _globalProvider = slotResourceProvider;
            _loadSequence = new LoadCommandsSequence(program);

        }

        public IConstantBufferSlotResource<T> RequestConstantBuffer<T>(string slotName, T data) where T : struct
        {
            var result = _globalProvider.RequestConstantBuffer(slotName, data);
            _loadSequence.Add(result);
            return result;
        }

        public IConstantBufferSlotResource<T> RequestConstantBuffer<T>(string slotName) where T : struct
        {
            return this.RequestConstantBuffer<T>(slotName, new T());
            
        }

        public ITextureSlotResource RequestTextureInAtlas(string slotName, IBitmap bp, string atlasName)
        {
            var result = _globalProvider.RequestTextureForAtlas(slotName, atlasName, bp);
            _loadSequence.Add(result);
            return result;
        }

        public ITextureSlotResource RequestTexture(string slotName, IBitmap bp)
        {
            var result = _globalProvider.RequestTexture(slotName, bp);
            _loadSequence.Add(result);
            return result;
        }



        public ITextureSlotResource RequestTexture(string slotName, string fileName)
        {
            throw new NotImplementedException();
            //var result = _globalProvider.RequestTexture(slotName, fileName);
            //_loadSequence.Add(result);
            //return result;
        }

        public LoadCommandsSequence GetLoadSequence()
        {
            return _loadSequence;
        }


    }
}
