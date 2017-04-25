using System;
using MDXEngine;

namespace TestApp.Models.ShapesManagerService.Render
{
    public abstract class RenderBase
    {

        public string Id { get; set; }

        protected RenderBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        public abstract string GetPainterName();

        public abstract IShape AttachToShader(MDXEngine.IDxViewControl _dx, MDXEngine.ITopology topology);

        public abstract void DetachFromShader(MDXEngine.IDxViewControl _dx,IShape shape);

        

    }
}
