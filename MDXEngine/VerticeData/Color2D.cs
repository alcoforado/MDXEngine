using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using MDXEngine.SharpDXExtensions;
namespace MDXEngine
{
    public struct Color2D : IPosition
    {
        public Vector4 _Position;
        public Vector4 Color;

        public Vector3 Position { get { return _Position.XYZ(); } set { _Position=value.ToVector4(); } }

    }


}
