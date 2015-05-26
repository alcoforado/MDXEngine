using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine.Shapes
{
    public class Triangle3DI : ITopology
    {
        Vector3 P0;
        Vector3 P1;
        Vector3 P2;

        public Triangle3DI(Vector3 p0, Vector3 p1, Vector3 p2)
        {
            P0 = p0;
            P1 = p1;
            P2 = p2;

        }
        public int NIndices() { return 3; }
        public int NVertices() { return 3; }
        public TopologyType GetTopologyType() { return TopologyType.TRIANGLES; }
        public void Write(IArray<Vector3> vV, IArray<int> vI)
        {
            vV[0] = P0;
            vV[1] = P1;
            vV[2] = P2;

            vI[0] = 0;
            vI[1] = 1;
            vI[2] = 2;
        }
    }
}
