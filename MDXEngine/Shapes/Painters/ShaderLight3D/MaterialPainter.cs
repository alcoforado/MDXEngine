using MDXEngine.DrawTree;
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
        private Material Material { get; set; }


        public MaterialPainter(Material mat)
        {
            this.Material = mat;
        }

        
        public void Write(IArray<VerticeNormal> vV, IArray<int> vI, TopologyType topologyType)
        {
            return;
        }

        public List<SlotData> GetLoadResourcesCommands()
        {
            return new List<SlotData>
            {
                new SlotData {
                   Data=Material,
                   SlotName="Material"
                }

            };
        }


    }
}
