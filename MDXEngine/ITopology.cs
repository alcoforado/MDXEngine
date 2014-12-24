using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
namespace MDXEngine
{
    interface ITopology
    {
        int NIndices();
        int NVertices();
        void Write(IArray<Vector3> vV, IArray<int> vI);
        TopologyType GetTopologyType();

    }
}
