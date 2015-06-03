using System;
using System.Collections.Generic;
using MDXEngine.Textures;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;

namespace MDXEngine
{


    public class HLSLProgram : IDisposable
    {
        readonly VertexShader _vertexShader;
        readonly PixelShader _pixelShader;
        readonly InputLayout _layout;
        private readonly IDxContext _dx;
        private readonly ShaderSlotsCollection _slots;

        internal VertexShader VertexShader { get { return _vertexShader; } }
        internal PixelShader PixelShader { get { return _pixelShader; } }
        internal InputLayout InputLayout { get { return _layout; } }
        internal ShaderSlotsCollection ProgramResourceSlots { get { return _slots; } }
        
        public InputLayout GetLayout() { return _layout; }

        public HLSLProgram(IDxContext dx, String program, InputElement[] elems)
        {
            var device = dx.Device;
            var vertexShaderByteCode = ShaderBytecode.Compile(program, "VS", "vs_4_0");
            _vertexShader = new VertexShader(device, vertexShaderByteCode);


            var pixelShaderByteCode = ShaderBytecode.Compile(program, "PS", "ps_4_0");
            _pixelShader = new PixelShader(device, pixelShaderByteCode);


            // Layout from VertexShader input signature
            var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
            _layout = new InputLayout(device, signature, elems);


            var vertexReflection = new ShaderReflection(vertexShaderByteCode);
            var pixelReflection  = new ShaderReflection(pixelShaderByteCode);

           
            //Create Resource Slots
            _slots = new ShaderSlotsCollection();
            _slots.AddResourceSlots(pixelReflection, ShaderStage.PixelShader);
            _slots.AddResourceSlots(vertexReflection, ShaderStage.VertexShader);
           

            _dx = dx;
            vertexShaderByteCode.Dispose();
            pixelShaderByteCode.Dispose();
            vertexReflection.Dispose();
            pixelReflection.Dispose();
            signature.Dispose();

        }

        public void Dispose()
        {
            _vertexShader.Dispose();
            _pixelShader.Dispose();
            _layout.Dispose();
        }



        public IDxContext DxContext { get { return _dx; } }
    

        public bool IsCurrent()
        {
            return _dx.CurrentProgram == this;
        }

        public void SetAsCurrent()
        {
            if (!IsCurrent())
                _dx.CurrentProgram = this;
        }
    }

}
