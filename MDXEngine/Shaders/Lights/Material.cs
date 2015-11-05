using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shaders.Lights
{
    public struct  Material
    {
        Vector4 Ambient;
        Vector4 Diffuse;
        Vector4 Specular; //w=SpecPower
        Vector4 Reflect;
    }
}
