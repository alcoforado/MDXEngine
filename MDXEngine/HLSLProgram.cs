using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public interface ITextureSlot
        {
            Texture Texture { get; }
            int Slot { get;  }
            string Name { get; }
        }

        public class TextureSlot : ITextureSlot
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


            //Create Texture Slots
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

        public int GetTextureSlot(String name)
        {
            int i = 0;
            for (; i < TextureSlots.Count; i++)
            {
                if (TextureSlots[i].Name == name)
                    return TextureSlots[i].Slot;
            }
            throw new Exception(String.Format("Texture Slot with Name {0} does not exist",name));
        }
        
        


        
        //TODO: Implement Set Texture given a variable name
        public void SetTexture(string variableName, Texture texture)
        {
            var nSlots = _pixelReflection.InterfaceSlotCount;

        }

        public void SetTexture(int textureIndex, Texture texture)
        {
            
        }


        public void LoadTexture(int textureSlot, Texture texture)
        {
            
        }
    }

}
