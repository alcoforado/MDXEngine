using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine
{
    public interface ITopology2D
    {
        int NIndices();
        int NVertices();
        void Write(IArray<Vector2> vV, IArray<int> vI);
        TopologyType GetTopologyType();
    }
}
