﻿using MDXEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree.SlotAllocation
{
    class ShapeContextSlotResourceProvider : ISlotResourceAllocator
    {
        readonly CommandsSequence _loadSequence;
        readonly ISlotResourceAllocator  _globalProvider;
        readonly IShaderProgram _program;

        public ShapeContextSlotResourceProvider(IShaderProgram program,CommandsSequence loadSequence,SlotResourceProvider provider)
        {
            this._program = program;
            _globalProvider = provider;
            _loadSequence = loadSequence;
        }

        public ShapeContextSlotResourceProvider(IShaderProgram program, SlotResourceProvider slotResourceProvider)
        {
            this._program = program;
            _globalProvider = slotResourceProvider;
            _loadSequence = new CommandsSequence(program);

        }

        public IConstantBufferSlotResource<T> RequestConstantBuffer<T>(string slotName, T data) where T : struct
        {
            var result = _globalProvider.RequestConstantBuffer(slotName, data);
            _loadSequence.Add(result);
            return result;
        }

        public ITextureSlotResource RequestTexture(string slotName, string fileName)
        {
            var result = _globalProvider.RequestTexture(slotName, fileName);
            _loadSequence.Add(result);
            return result;
        }

        public CommandsSequence GetLoadSequence()
        {
            return _loadSequence;
        }


    }
}