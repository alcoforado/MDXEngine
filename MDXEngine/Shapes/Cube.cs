using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shapes
{
    public class Cube : ITopology
    {
        public Vector3 _p0, _p1;
        public int NIndices() { return 36; }
        public int NVertices() { return 8; }

        public Vector3 P0 { get { return _p0; } }
        public Vector3 P1 { get { return _p1; } }


        public Cube(Vector3 barycenter, Vector3 dims)
        {
            
            _p0 = barycenter-0.5f*dims;
            _p1 = barycenter+0.5f* dims; 
        }


        public TopologyType GetTopologyType()
        {
            return TopologyType.TRIANGLES;
        }

        public void Write(IArray<Vector3> vV, IArray<int> vI)
        {
            vV[0] = new Vector3(_p0[0], _p0[1], _p0[2]);
            vV[1] = new Vector3(_p0[0], _p1[1], _p0[2]);
            vV[2] = new Vector3(_p1[0], _p1[1], _p0[2]);
            vV[3] = new Vector3(_p1[0], _p0[1], _p0[2]);
            vV[4] = new Vector3(_p0[0], _p0[1], _p1[2]);
            vV[5] = new Vector3(_p0[0], _p1[1], _p1[2]);
            vV[6] = new Vector3(_p1[0], _p1[1], _p1[2]);
            vV[7] = new Vector3(_p1[0], _p0[1], _p1[2]);

            int i = 0;
            //bottom Face
            vI[i++] = 0; vI[i++] = 1; vI[i++] = 2;
            vI[i++] = 0; vI[i++] = 2; vI[i++] = 3;

            //topface Face
            vI[i++] = 5; vI[i++] = 4; vI[i++] = 7;
            vI[i++] = 5; vI[i++] = 7; vI[i++] = 6;

            //Left Face
            vI[i++] = 0; vI[i++] = 4; vI[i++] = 5;
            vI[i++] = 0; vI[i++] = 5; vI[i++] = 1;

            //Right Face
            vI[i++] = 2; vI[i++] = 7; vI[i++] = 3;
            vI[i++] = 2; vI[i++] = 6; vI[i++] = 7;

            //back Face
            vI[i++] = 2; vI[i++] = 5; vI[i++] = 6;
            vI[i++] = 2; vI[i++] = 1; vI[i++] = 5;

            //front Face
            vI[i++] = 0; vI[i++] = 3; vI[i++] = 7;
            vI[i++] = 0; vI[i++] = 7; vI[i++] = 4;

        }


    }
}
