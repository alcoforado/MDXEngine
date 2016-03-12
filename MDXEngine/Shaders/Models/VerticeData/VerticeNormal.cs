using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.SharpDXExtensions;
namespace MDXEngine.Shaders
{
    public struct VerticeNormal : IPosition, INormal
    {
        public Vector4 _Position;
        public Vector4 _Normal;

        public Vector3 Position { get { return _Position.XYZ(); } set { _Position = value.ToVector4(); } }
        public Vector3 Normal { get { return _Normal.XYZ(); } set { _Normal = value.ToVector4(); } }
        
    }
}
