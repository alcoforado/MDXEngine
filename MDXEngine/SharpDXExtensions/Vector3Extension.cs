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

    }
}
