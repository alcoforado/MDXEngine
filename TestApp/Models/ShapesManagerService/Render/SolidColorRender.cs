using System;
using MDXEngine;

namespace TestApp.Models.ShapesManagerService.Render
{
    class SolidColorRender : IRenderViewModel
    {
        public string GetPainterName()
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; }
        public object CreateRender()
        {
            throw new NotImplementedException();
        }

        public Type GetShaderType()
        {
            return typeof(ShaderColor3D);
        }


    }
}
