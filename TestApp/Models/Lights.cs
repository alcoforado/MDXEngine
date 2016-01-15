using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp.Controllers
{
    public class Lights
    {
        Vector4 Ambient{get; set;}
        Vector4 Diffuse{get; set;}
        Vector3 Specular{get; set;}
        float SpecPower{get; set;}
        Vector3 Direction{get; set;}
    }
}
