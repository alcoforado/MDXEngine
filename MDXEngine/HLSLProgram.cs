using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;
using SharpDX.D3DCompiler;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
namespace MDXEngine
{


    public class HLSLProgram : IDisposable
    {
        public class TextureSlot
        {
            public TextureSlot(int slot, string name)
            {
                Slot = slot;
                Name = name;
            }

            public int Slot { get; private set; }
            public Texture Texture { get; set; }
            public bool HasTexture { get { return Texture != null; } }
            public string Name { get; private set; }
        }


        readonly VertexShader _vertexShader;
        readonly PixelShader _pixelShader;
        readonly InputLayout _layout;
        private readonly ShaderReflection _vertexReflection;
        private readonly ShaderReflection _pixelReflection;

        public List<TextureSlot> TextureSlots { get; private set; } 
        public InputLayout GetLayout() { return _layout; }

        public HLSLProgram(Device device, String program, InputElement[] elems)
        {

            var vertexShaderByteCode = ShaderBytecode.Compile(program, "VS", "vs_4_0");
            _vertexShader = new VertexShader(device, vertexShaderByteCode);


            var pixelShaderByteCode = ShaderBytecode.Compile(program, "PS", "ps_4_0");
            _pixelShader = new PixelShader(device, pixelShaderByteCode);


            // Layout from VertexShader input signature
            var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
            _layout = new InputLayout(device, signature, elems);


            _vertexReflection = new ShaderReflection( vertexShaderByteCode);
            _pixelReflection = new ShaderReflection(pixelShaderByteCode);


            //Check Texture Slots
            TextureSlots = new List<TextureSlot>();

            var nSlots = _pixelReflection.Description.BoundResources;
            int slotI = 0;
            for (int i = 0; i < nSlots; i++)
            {
                var desc = _pixelReflection.GetResourceBindingDescription(i);
                if (desc.Type == ShaderInputType.Texture)
                {
                    TextureSlots.Add(new TextureSlot(i, desc.Name));
                }
            }


            vertexShaderByteCode.Dispose();
            pixelShaderByteCode.Dispose();
            signature.Dispose();

        }

        public void Dispose()
        {
            _vertexReflection.Dispose();
            _pixelReflection.Dispose();
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

        //TODO: Implement Set Texture given a variable name
        public void SetTexture(string variableName, Texture texture)
        {
            var nSlots = _pixelReflection.InterfaceSlotCount;

        }


    }

}
