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
       public Vector4 Ambient;
       public Vector4 Diffuse;
       public Vector3 Specular;
       public float SpecPower;
       public Vector3 Direction;
       public float Padding;
    }
}
