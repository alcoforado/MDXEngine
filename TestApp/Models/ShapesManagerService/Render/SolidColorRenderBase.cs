
using System.Dynamic;
using MDXEngine;
using MDXEngine.Painters;
using MDXEngine.Shapes;
using SharpDX;

namespace TestApp.Models.ShapesManagerService.Render
{
    class SolidColorRender: RenderBaseViewModel
    {
        public Color Color { get; set; }

        private Shape3D<VerticeColor> Shape { get; set; }

        public override string GetPainterName()
        {
            return "SolidColor";
        }

        public override IShape AttachToShader(IDxViewControl _dx, ITopology topology)
        {
            var shader = _dx.ResolveShader<ShaderColor3D>();
            this.Shape = new Shape3D<VerticeColor>(topology, new CyclicColorizer(Color));
            shader.Add(this.Shape);
            return Shape;
        }

        public override void DetachFromShader(IDxViewControl _dx)
        {
            var shader = _dx.ResolveShader<ShaderColor3D>();
            shader.Remove(this.Shape);
        }
    }
}
