using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MDXEngine;
using MUtils.Reflection;
using TestApp.Mappers;
using TestApp.Models.ShapesManagerService.Render;
using TestApp.Models.ShapesManagerService.Topologies;
using TestApp.Services.Interfaces;
using TestApp.WebApi.Models.ShapeMngr;

namespace TestApp.Services
{
    public class ShapeMngrService : IShapeMngrService
    {
        private Dictionary<string, Type> _shapeTypes;
        private Dictionary<string, Type> _renderTypes;

        private int _idCounter = 0;
        private IDxViewControl _dx;
        private Dictionary<string, ShapeUIBase> _shapeCollection;

        public ShapeMngrService(IDxViewControl dx)
        {
            _dx = dx;
            _shapeCollection = new Dictionary<string, ShapeUIBase>();
            _shapeTypes = new Dictionary<string, Type>();
            _renderTypes = new Dictionary<string, Type>();
            var shapesT = typeof(ShapeUIBase).GetImplementationsInCurrentAssembly();
            foreach (var type in shapesT)
            {
                _shapeTypes.Add(((ShapeUIBase)Activator.CreateInstance(type)).GetShapeName(), type);
            }

            var paintersT = typeof(RenderBase).GetImplementationsInCurrentAssembly();
            foreach (var type in paintersT)
            {
                _renderTypes.Add(((RenderBase)Activator.CreateInstance(type)).GetPainterName(), type);
            }
        }

        public RenderBase CreateRender(string renderType)
        {
            return (RenderBase)Activator.CreateInstance(_renderTypes[renderType]);
        }

        public Dictionary<string, Type> GetShapeTypes()
        {
            return _shapeTypes;
        }

        public Dictionary<string, Type> GetRenderTypes()
        {
            return _renderTypes;
        }

        public ShapeUIBase GetShape(string id)
        {
            return _shapeCollection[id];
        }

        public Dictionary<string, ShapeUIBase> GetShapes()
        {
            return _shapeCollection;
        }

        public bool HasShape(string shapeId)
        {
            return _shapeCollection.ContainsKey(shapeId);
        }

        public void SetShapeRender(string shapeId, RenderBase render)
        {
            var shape = GetShape(shapeId);
            shape.SetRender(_dx,render);
        }

        public void DeleteShape(string id)
        {
            if (!_shapeCollection.ContainsKey(id))
            {
                throw new Exception(String.Format("Error, Shape Id {0} not found", id));
            }
            else
            {
                var shape = _shapeCollection[id];
                shape.DetachFromShader(_dx);
                _shapeCollection.Remove(id);
            }

        }

        public ShapeUIBase CreateShape(string shapeTypeId)
        {
            if (!_shapeTypes.ContainsKey(shapeTypeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified", shapeTypeId));
            }

            var shape = (ShapeUIBase)Activator.CreateInstance(_shapeTypes[shapeTypeId]);
            var render = new SolidColorRender() { Color = SharpDX.Color.Aquamarine };

            shape.SetRender(_dx,render);

            shape.Id = "Shape" + Interlocked.Increment(ref _idCounter).ToString();
            _shapeCollection.Add(shape.Id, shape);
            return shape;
        }

    }
}

