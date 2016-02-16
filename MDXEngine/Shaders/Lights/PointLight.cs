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
       public  Vector4 Ambient;
       public  Vector4 Diffuse;
       public  Vector4 Specular;
       public  Vector3 Direction;
       public  float Range;
       
       public  Vector3 Atenuation;
       public  float Pad;



    }
}
