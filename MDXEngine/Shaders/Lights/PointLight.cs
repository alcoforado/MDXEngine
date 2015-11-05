using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shaders.Lights
{
    public struct PointLight
    {
        Vector4 Ambient;
        Vector4 Diffuse;
        Vector4 Specular;
        Vector3 Direction;
        float Range;

        Vector3 Atenuation;
        float Pad;



    }
}
