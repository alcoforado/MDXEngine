using System;
using System.Collections.Generic;
using System.Linq;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree.SlotAllocation
{



    public partial  class SlotResourceProvider : IDisposable, ISlotResourceProvider
    {
        private readonly HLSLProgram _hlsl;
        private readonly Dictionary<string,SlotPool> _pools;
        private readonly IShaderResourceFactory _shaderResourceFactory;


        public void Dispose()
            {
                foreach (var pool in _pools)
                {
                    pool.Dispose();
                }
            }

        public SlotResourceProvider(IShaderResourceFactory resourceFactory,HLSLProgram hlsl)
        {
            _hlsl = hlsl;

            _pools = _hlsl.ProgramResourceSlots.ToList().ToDictionary(x => x.Name, x => new SlotPool(resourceFactory,x));
        }


        public ILoadCommand CreateLoadCommand(SlotRequest request)
        {

            return new LoadCommand(request, this);
        }


    }

 }
