using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shaders
{
    public struct Material
    {
        public Vector4 Ambient;
        public Vector4 Diffuse;
        public Vector4 Specular; //w=SpecPower
        public Vector4 Reflect;
    }
}
