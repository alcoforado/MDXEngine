using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace MDXEngine.Painters
{
    public class GridTextureMapping<T> : IPainter<T> where T : ITEX
    {
        int _nX, _nY;
        float _dx, _dy;
        public GridTextureMapping(int nX = 1, int nY = 1)
        {
            System.Diagnostics.Debug.Assert(nX>=1);
            System.Diagnostics.Debug.Assert(nY>=1);

            _nX = nX;
            _nY = nY;
            _dx = 1.0f / _nX;
            _dy = 1.0f / _nY;
        }


        public void Write(IArray<T> vV, IArray<int> vI, TopologyType topologyType)
        { 
            if (topologyType == TopologyType.TRIANGLES)
            {
                Vector2 p0 =   new Vector2(0f,0f);
                Vector2 Up =   new Vector2(0f,-_dy);
                Vector2 Down = new Vector2(0f,_dy);
                Vector2 Right = new Vector2(_dx,0f);
                Vector2 Left = new Vector2(-_dx, 0f);
            }
        }




    }
}
