using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine;
using MDXEngine.Shapes;
using MDXEngine.Shaders;
using SharpDX;
using MDXEngine.Painters;
namespace TestApp.Actions
{
    public class ColorTriangle : IApp
    {
        ShaderColor2D _shaderColor2D;
        
        public ColorTriangle(DxControl dx)
        {
            _shaderColor2D = new ShaderColor2D(dx);
           var triangle = new Triangle2DI(
                        new Vector2(-1f, -1f),
                        new Vector2(1f, -1f),
                        new Vector2(0f, 1f));

             var colors = new CyclicColorizer(new Color[] { Color.Red, Color.Green, Color.Blue });
            _shaderColor2D.Add(triangle, colors);
        }

        public void Dispose()
        {
        }


    }
}
