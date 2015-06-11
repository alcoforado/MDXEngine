using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shapes
{
    public class WireCube
    {
         public Vector3 _p0, _p1;
        public int NIndices() { return 12; }
        public int NVertices() { return 8; }

        public Vector3 P0 { get { return _p0; } }
        public Vector3 P1 { get { return _p1; } }


        public WireCube(Vector3 p0, Vector3 p1)
        {
            _p0 = p0;
            _p1 = p1;
        }


        public TopologyType GetTopologyType()
        {
            return TopologyType.LINES;
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
            //Vertice 0 Connection 
            vI[i++] = 0; vI[i++] = 1; 
            vI[i++] = 0; vI[i++] = 3; 
            vI[i++] = 0; vI[i++] = 4; 
            

            //Vertice 1 Connection 
            vI[i++] = 1; vI[i++] = 2; 
            vI[i++] = 1; vI[i++] = 5; 

            
            //Vertice 2 Connection
            vI[i++] = 2; vI[i++] = 3; 
            vI[i++] = 2; vI[i++] = 6; 


            //Vertice 3 Connection
            vI[i++] = 3; vI[i++] = 7; 

            //Vertice 4 Connection
            vI[i++] = 4; vI[i++] = 5; 
            vI[i++] = 4; vI[i++] = 7; 
            
            //Vertice 5 Connection
            vI[i++] = 5; vI[i++] = 6; 

            //Vertice 6 Connection
            vI[i++] = 6; vI[i++] = 7; 





        }

    }
}
