using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using MDXEngine.SharpDXExtensions;
namespace MDXEngine.Shapes
{
    public class Triangle2DI : ITopology2D
    {
        Vector2 P0;
        Vector2 P1;
        Vector2 P2;

        public Triangle2DI(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            P0 = p0;
            P1 = p1;
            P2 = p2;

        }
        public int NIndices() { return 3; }
        public int NVertices() { return 3; }
        public TopologyType GetTopologyType() { return TopologyType.TRIANGLES; }
        public void Write(IArray<Vector2> vV, IArray<int> vI)
        {
            vV[0] = P0;
            vV[1] = P1;
            vV[2] = P2;

            vI[0] = 0;
            vI[0] = 1;
            vI[1] = 2; 
        }
    }
}
