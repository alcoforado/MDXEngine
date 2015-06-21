using MDXEngine;
using MDXEngine.Painters;
using MDXEngine.Shapes;
using SharpDX;
namespace TestApp.Actions
{
    public class CubeColor3D : IApp
    {
        ShaderColor3D _shaderColor;
        MouseSphericNavigator _mouseHandler;
        public CubeColor3D(DxControl control)
        {
            var dx = control.GetDxContext();
            _shaderColor = new ShaderColor3D(dx);
            control.AddShader(_shaderColor);
            var triangle = new Triangle3DI(
                         new Vector3(0f, 0f, 0.5f),
                         new Vector3(0.5f, 0f, 0.5f),
                         new Vector3(0f, 0.5f, 0.5f));

            var cube = new Cube(
                         new Vector3(-1f, -1f, -1f),
                         new Vector3(1f, 1f, 1f));

            var colors = new CyclicColorizer(new Color[] { Color.Red, Color.Green, Color.Blue });
            _shaderColor.Add(cube, colors);

            _mouseHandler = new MouseSphericNavigator(dx.Camera,true);
            _mouseHandler.AttachControl(control.Control);
        }

        public void Dispose()
        {
            _mouseHandler.DetachControl();
        }


    }
}

