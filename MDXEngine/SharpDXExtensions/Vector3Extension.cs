using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;


namespace MDXEngine.SharpDXExtensions
{
    public static class Vector3Extension
    {

        public static void Set(this Vector3 v,float c)
        {
            v.X = v.Y = v.Z = c;
        }
        public static  Vector4 ToVector4(this Vector3 v,float W=1)
        {
           return  new Vector4(v.X,v.Y,v.Z,W);
        }

        public static float Norm2(this Vector3 v)
        {
            return v.X*v.X + v.Y*v.Y + v.Z*v.Z;
        }


        public static float Norm(this Vector3 v)
        {
            return (float) Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }
    }
}
