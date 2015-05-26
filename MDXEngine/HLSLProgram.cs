using System;
using System.Collections.Generic;
using MDXEngine.Textures;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;

namespace MDXEngine
{


    public class HLSLProgram : IDisposable
    {

        public class ResourceSlot
        {
            public IShaderResource Resource { get; set; }
            public int Slot { get; set; }
            public string Name { get; set; }
            public ShaderInputType ResourceType { get; set; }

            public ResourceSlot(int slot,string name,ShaderInputType resourceType)
            {
                this.Slot = slot;
                this.Name = name;
                this.ResourceType = resourceType;
            }

            internal bool IsTexture()
            {
                return ResourceType == ShaderInputType.Texture;
            }

            internal bool HasResource()
            {
                return this.Resource != null;
            }
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


        public List<ResourceSlot> ResourceSlots { get; private set; }
        
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


            //Create Texture Slots and CBuffersSlots
            ResourceSlots = new List<ResourceSlot>();

            var nSlots = _pixelReflection.Description.BoundResources;
            for (int i = 0; i < nSlots; i++)
            {
                var desc = _pixelReflection.GetResourceBindingDescription(i);
                if (desc.Type == ShaderInputType.Texture)
                {
                    ResourceSlots.Add(new ResourceSlot(desc.BindPoint, desc.Name,desc.Type));
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



        public MayNotExist<ResourceSlot> GetResourceSlot(String name)
        {
            return new MayNotExist<ResourceSlot>(ResourceSlots.Find(x => x.Name == name));
        }

        public MayNotExist<ResourceSlot> GetResourceSlot(int slot)
        {
            return new MayNotExist<ResourceSlot>(ResourceSlots.Find(x => x.Slot == slot));
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
            var slot = ResourceSlots.Find(x => x.Slot == textureSlotId);
            
            
            if (slot!=null)
            {
                if (slot.IsTexture())
                {
                    slot.Resource = texture;
                    _dx.DeviceContext.PixelShader.SetShaderResource(textureSlotId, slot.Resource.GetResourceView());
                }
                throw new Exception(String.Format("Slot {0} is not a texture", textureSlotId));
            }
            else
                throw new Exception(String.Format("Texture Slot {0} does not exist", textureSlotId));
        }

      

       
    }

}
