using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.D3DCompiler;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
namespace MDXEngine
{
    public class HLSLProgram : IDisposable
    {
        VertexShader _vertexShader;
        PixelShader _pixelShader;
        InputLayout _layout;
       


        public InputLayout GetLayout() { return _layout; }

        public HLSLProgram(Device device, String program, InputElement[] elems)
        {
            
            var vertexShaderByteCode = ShaderBytecode.Compile(HLSLResources.Color2D_hlsl, "VS", "vs_4_0");
            _vertexShader = new VertexShader(device, vertexShaderByteCode);


            var pixelShaderByteCode = ShaderBytecode.Compile(HLSLResources.Color2D_hlsl, "PS", "ps_4_0");
            _pixelShader = new PixelShader(device, pixelShaderByteCode);


            // Layout from VertexShader input signature
            var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
            _layout = new InputLayout(device, signature, elems);
            
          
            vertexShaderByteCode.Dispose();
            pixelShaderByteCode.Dispose();
            signature.Dispose();

        }

        public void Dispose()
        {
            _vertexShader.Dispose();
           _pixelShader.Dispose();
           _layout.Dispose();
        }

        public void SetAsCurrent(IDxContext dx)
        {
            dx.DeviceContext.InputAssembler.InputLayout = _layout;
            dx.DeviceContext.VertexShader.Set(_vertexShader);
            dx.DeviceContext.PixelShader.Set(_pixelShader);
     
        }

    }
}
