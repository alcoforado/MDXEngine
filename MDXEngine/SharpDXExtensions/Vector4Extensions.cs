using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine.SharpDXExtensions
{
        public static class Vector4Extension
        {
            public static Vector3 XYZ(this Vector4 v)
            {
                return new Vector3(v.X,v.Y,v.Z);
            }
        }
}
