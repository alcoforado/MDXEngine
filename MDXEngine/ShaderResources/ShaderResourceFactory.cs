using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.DrawTree;
using MDXEngine.Interfaces;
using SharpDX.D3DCompiler;

namespace MDXEngine.ShaderResources
{
    

    public class ShaderResourceFactory : IShaderResourceFactory
    {
        private readonly HLSLProgram _hlsl;

        public ShaderResourceFactory(HLSLProgram hlsl)
        {
            _hlsl = hlsl;
        }

        public IShaderResource CreateForSlot(string slotName)
        {
            var slot = _hlsl.ProgramResourceSlots[slotName].Value;
            var type = slot.ResourceType;
            switch (type)
            {
                case ShaderInputType.ConstantBuffer:
                    {
                        var slotType = slot.DataType;
                        if (slotType == null)
                            throw new Exception("Slot {0} is a constant buffer but the shader did not specify a type for it");

                        var result = (IShaderResource)typeof(CBufferResource<>).MakeGenericType(slotType)
                            .GetConstructor(new Type[] { typeof(HLSLProgram) })
                            .Invoke(new object[] { _hlsl });

                        return result;
                    }
                default:
                    throw new Exception(string.Format("Cannot Construct a ShaderResource of type {0}", type.ToString()));
            }

        }


    }
}
