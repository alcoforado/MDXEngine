using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDXEngine;
using TestApp.Models.ShapesManagerService.Render;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    public abstract class  ShapeUIBase
    {
        private RenderBase _render;
        private IShape _shape;
        internal string PainterType() {return _render.GetType().Name; }
        protected abstract ITopology CreateTopology();
        internal RenderBase Render { get { return _render; } set { _render = value; } }

        public string Id { get; set; }
        public string Name { get; set; }
        public void SetRender(IDxViewControl dx, RenderBase render)
        {
            _render?.DetachFromShader(dx,_shape);
            _render = render;
            _render.AttachToShader(dx, this.CreateTopology());
        }

        protected ShapeUIBase()
        {
            Id = Guid.NewGuid().ToString();
            Name = "";
        }

        public abstract string GetShapeName();

        public void DetachFromShader(IDxViewControl dx)
        {
            _render.DetachFromShader(dx,_shape);
        }

       

        public void Redraw(IDxViewControl dx)
        {
            _render.DetachFromShader(dx,_shape);
            _shape=_render.AttachToShader(dx, this.CreateTopology());
        }
    }
}
