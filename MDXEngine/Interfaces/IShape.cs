using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine
{
    public enum TopologyType { POINTS, LINES, TRIANGLES}
    public interface IShape<T> 
    {
        void Draw(IDrawContext<T> context);
        int  NVertices();
        int  NIndices();
        TopologyType GetTopology();
        List<SlotRequest> RequestSlotResources(ISlotResourceAllocator provider);
    }
}
