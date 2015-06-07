using MDXEngine;
using MDXEngine.Painters;
using MDXEngine.Shapes;
using SharpDX;
namespace TestApp.Actions
{
    public class ColorTriangle3D : IApp
    {
        ShaderColor3D _shaderColor;

        public ColorTriangle3D(DxControl dx)
        {
            _shaderColor = new ShaderColor3D(dx);
            var triangle = new Triangle3DI(
                         new Vector3(0f, 0f, 0.5f),
                         new Vector3(0.5f, 0f, 0.5f),
                         new Vector3(0f, 0.5f, 0.5f));

            var colors = new CyclicColorizer(new Color[] { Color.Red, Color.Green, Color.Blue });
            _shaderColor.Add(triangle, colors);
        }

        public void Dispose()
        {
        }


    }
}

