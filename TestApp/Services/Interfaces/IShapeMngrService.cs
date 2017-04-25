using System;
using System.Collections.Generic;
using MDXEngine;
using TestApp.Models.ShapesManagerService.Render;
using TestApp.Models.ShapesManagerService.Topologies;

namespace TestApp.Services.Interfaces
{
    public interface IShapeMngrService
    {
        RenderBase CreateRender(string renderType);
        void DeleteShape(string id);
        Dictionary<string, Type> GetRenderTypes();
        ShapeUIBase GetShape(string id);
        Dictionary<string, Type> GetShapeTypes();
        Dictionary<string, ShapeUIBase> GetShapes();
        bool HasShape(string shapeId);
        void SetShapeRender(string shapeId, RenderBase render);
        ShapeUIBase CreateShape(string shapeTypeId);
        List<RenderBase> GetRenders();
        MayNotExist<RenderBase> GetRender(string shapeId);
        void RenderChanged(RenderBase render);
    }
}