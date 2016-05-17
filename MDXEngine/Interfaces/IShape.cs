using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine
{
    public interface IShape
    {
        int NVertices();
        int NIndices();
        TopologyType GetTopology();
        void RequestSlotResources(ISlotResourceAllocator provider);
    }

    public enum TopologyType { POINTS, LINES, TRIANGLES}
    public interface IShape<T> : IShape
    {
        void Draw(IDrawContext<T> context);
       
    }
}
