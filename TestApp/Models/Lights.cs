using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp.Models
{
    public class Lights
    {
        public Vector4 Ambient{get; set;}
        public Vector4 Diffuse{get; set;}
        public Vector3 Specular{get; set;}
        public float SpecPower{get; set;}
        public Vector3 Direction{get; set;}
        public List<int> test { get; set; }

        public Lights()
        {
            test = new List<int> { 1, 2, 3, 4 };
        }
    }
}
