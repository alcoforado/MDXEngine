using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.D3DCompiler;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
namespace MDXEngine
{
    public class ShaderColor2D
    {
        IDxContext _dx;
        VertexShader _vertexShader;
        PixelShader _pixelShader;
        InputLayout _layout;



        public ShaderColor2D(IDxContext dxContext)
        {
            _dx = dxContext;
        }

        public ShaderColor2D()
        {

            var vertexShaderByteCode = ShaderBytecode.CompileFromFile("./hlsl/Color2D.hlsl", "VS", "vs_4_0");
            _vertexShader = new VertexShader(_dx.Device, vertexShaderByteCode);


            var pixelShaderByteCode = ShaderBytecode.CompileFromFile("./hlsl/Color2D.hlsl", "PS", "ps_4_0");
            _pixelShader = new PixelShader(_dx.Device, pixelShaderByteCode);


            // Layout from VertexShader input signature
            var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
            _layout = new InputLayout(_dx.Device, signature, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            vertexShaderByteCode.Dispose();
            pixelShaderByteCode.Dispose();
            signature.Dispose();
        }
    }
}
