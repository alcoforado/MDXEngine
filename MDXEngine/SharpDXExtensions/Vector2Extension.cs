using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine.SharpDXExtensions
{
    static public class Vector2Extension
    {
        public static Vector4 ToVector4(this Vector2 v, float Z = 0,float W = 1)
        {
            return new Vector4(v.X, v.Y, Z, W);
        }


    }
}
