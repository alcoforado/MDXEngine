using SharpDX.D3DCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public class ShaderSlotsCollection
    {
        private readonly Dictionary<String,SlotDescription> _slots;

        public void AddResourceSlots(ShaderReflection  shader,ShaderStage stage)
        {
            var nSlots = shader.Description.BoundResources;
            for (int i = 0; i < nSlots; i++)
            {
                var desc = shader.GetResourceBindingDescription(i);
                if (desc.Type == ShaderInputType.Texture || desc.Type == ShaderInputType.ConstantBuffer)
                {
                    if (_slots.ContainsKey(desc.Name))
                    {
                        //Just for consistency make sure that the bind point of what is already in the Dictionary
                        //and the new resource are the same
                        System.Diagnostics.Debug.Assert(desc.BindPoint == _slots[desc.Name].SlotId);
                    }
                    else
                        _slots[desc.Name] = new SlotDescription(desc.BindPoint, desc.Name, desc.Type,stage);
                }
            }

        }


        public ShaderSlotsCollection()
        {
            _slots = new Dictionary<String,SlotDescription>();
        }

        public MayNotExist<SlotDescription> this[string varName] 
        { 
            get 
            { 
                if (_slots.ContainsKey(varName))
                  return new MayNotExist<SlotDescription>(_slots[varName]); 
                else 
                    return new MayNotExist<SlotDescription>(); 
            } 
        }





    }


    public enum ShaderStage {PixelShader,VertexShader,GeometryShader};

    public class SlotDescription
    {
        public IShaderResource LoadedResource { get; set; }
        public int SlotId { get; set; }
        public string Name { get; set; }
        public ShaderInputType ResourceType { get; set; }
        public ShaderStage ShaderStage { get; set; }
        public SlotDescription(int slot, string name, ShaderInputType resourceType,ShaderStage stage)
        {
            this.SlotId = slot;
            this.Name = name;
            this.ResourceType = resourceType;
            this.ShaderStage = stage;
        }

        internal bool IsTexture()
        {
            return ResourceType == ShaderInputType.Texture;
        }

        internal bool HasResource()
        {
            return this.LoadedResource != null;
        }
    }


}
