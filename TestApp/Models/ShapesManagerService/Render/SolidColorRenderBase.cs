
using System.Dynamic;
using MDXEngine;
using MDXEngine.Painters;
using MDXEngine.Shapes;
using SharpDX;

namespace TestApp.Models.ShapesManagerService.Render
{
    class SolidColorRender: RenderBase
    {
        public Color Color { get; set; }


        public override string GetPainterName()
        {
            return "SolidColor";
        }

        public override IShape AttachToShader(IDxViewControl _dx, ITopology topology)
        {
            var shader = _dx.ResolveShader<ShaderColor3D>();
            var shape = new Shape3D<VerticeColor>(topology, new CyclicColorizer(Color));
            shader.Add(shape);
            return shape;
        }

        public override void DetachFromShader(IDxViewControl _dx,IShape shape )
        {
            var shader = _dx.ResolveShader<ShaderColor3D>();
            shader.Remove((IShape<VerticeColor>) shape);
        }
    }
}
