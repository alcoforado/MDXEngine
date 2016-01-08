using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace MDXEngine.Shaders
{
    [StructLayout(LayoutKind.Sequential,Pack=1)]
    public struct DirectionalLight
    {
        Vector4 Ambient;
        Vector4 Diffuse;
        Vector3 Specular;
        float SpecPower;
        Vector3 Direction;
        float Padding;
    }
}
