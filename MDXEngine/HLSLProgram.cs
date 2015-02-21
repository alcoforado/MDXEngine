using System;
using System.Collections.Generic;
using MDXEngine.Textures;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;

namespace MDXEngine
{


    public class HLSLProgram : IDisposable
    {

        public interface ITextureSlot
        {
            Texture Texture { get; }
            int Slot { get; }
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
        private readonly IDxContext _dx;

        internal VertexShader VertexShader { get { return _vertexShader; } }
        internal PixelShader PixelShader { get { return _pixelShader; } }
        internal InputLayout InputLayout { get { return _layout; } }


        public List<TextureSlot> TextureSlots { get; private set; }
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


            _vertexReflection = new ShaderReflection(vertexShaderByteCode);
            _pixelReflection = new ShaderReflection(pixelShaderByteCode);


            //Create Texture Slots
            TextureSlots = new List<TextureSlot>();

            var nSlots = _pixelReflection.Description.BoundResources;
            for (int i = 0; i < nSlots; i++)
            {
                var desc = _pixelReflection.GetResourceBindingDescription(i);
                if (desc.Type == ShaderInputType.Texture)
                {
                    TextureSlots.Add(new TextureSlot(i, desc.Name));
                }
            }

            _dx = dx;
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



        public MayNotExist<ITextureSlot> GetTextureSlot(String name)
        {
            return new MayNotExist<ITextureSlot>(TextureSlots.Find(x => x.Name == name));
        }

        public MayNotExist<ITextureSlot> GetTextureSlot(int slot)
        {
            return new MayNotExist<ITextureSlot>(TextureSlots.Find(x => x.Slot == slot));
        }



        public bool IsCurrent()
        {
            return _dx.CurrentProgram == this;
        }

        public void SetAsCurrent()
        {
            if (!IsCurrent())
                _dx.CurrentProgram = this;
        }




        public void LoadTexture(int textureSlotId, Texture texture)
        {
            if (!IsCurrent())
                throw new Exception("HLSLProgram is not the current loaded program");
            var slot = GetTextureSlot(textureSlotId);
            if (slot.Exists)
            {
                _dx.DeviceContext.PixelShader.SetShaderResource(textureSlotId, texture.GetResourceView());
            }
            else
                throw new Exception(String.Format("Texture Slot {0} does not exist", textureSlotId));
        }

      

       
    }

}
