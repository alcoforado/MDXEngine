using MDXEngine.DrawTree;
using MDXEngine.Interfaces;
using MDXEngine.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Painters.ShaderLight3D
{
    public class MaterialPainter : IPainter<VerticeNormal>
    {
        private IConstantBufferSlotResource<Material> _slotMaterial;
        private Material Material { get; set; }


        public MaterialPainter(Material mat)
        {
            this.Material = mat;
        }

        
        public void Draw(IDrawContext<VerticeNormal> context)
        {
            return;
        }

        public void RequestSlotResources(ISlotResourceAllocator provider)
        {
            _slotMaterial = provider.RequestConstantBuffer("Material", this.Material);
        }



    }
}
