﻿using System;
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

        public string Id { get; set; }
        public void SetRender(IDxViewControl dx, RenderBase render)
        {
            _render?.DetachFromShader(dx,_shape);
            _render = render;
            _render.AttachToShader(dx, this.CreateTopology());
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
