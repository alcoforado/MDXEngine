using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shapes
{
    interface ITopologyNormalProvider
    {
        void WriteNormalsAtVertices(IArray<Vector3> vN);
       
    }
}
