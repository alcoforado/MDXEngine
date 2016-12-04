using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using MDXEngine;
using MUtils.Reflection;
using Newtonsoft.Json;
using SharpDX;
using TestApp.Mappers;
using TestApp.Models.ShapesManagerService;
using TestApp.Models.ShapesManagerService.Render;
using TestApp.Models.ShapesManagerService.Topologies;
using TestApp.WebApi.Models.ShapeMngr;

namespace TestApp.WebApi.Controllers
{
    public class ShapeMngrController : ApiController
    {
        private Dictionary<string, Type> _shapeTypes;
        private Dictionary<string, Type> _renderTypes;

        private int _idCounter = 0;
        private IDxViewControl _dx;
        private IShapesMngrMapper _mapper;
        private Dictionary<string, ShapeUIBase> _shapeCollection;

        public List<string> GetTopologies()
        {
            return (
                from x in _shapeTypes
                select x.Key
                ).ToList(); ;
        }


        public ShapeMngrController(IDxViewControl dx, IShapesMngrMapper mapper)
        {
            _mapper = mapper;
            var painters = new List<RenderBase>() { new SolidColorRender() };
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

        [HttpGet]
        public List<UIType> GetShapeTypes()
        {
            return _shapeTypes.Select(pair => _mapper.ToUITypeDto(pair.Key, pair.Value)).ToList();
        }

        public List<UIType> GetPainterTypes()
        {
            return _renderTypes.Select(pair => _mapper.ToUITypeDto(pair.Key, pair.Value)).ToList();
        }


        public List<ShapeViewModel> GetShapes()
        {
            return _shapeCollection.Select(x => new ShapeViewModel()
            {
                ShapeType = x.Value.GetShapeName(),
                ShapeData = x.Value
            }).ToList();
        }

        public void DeleteShape(string shapeId)
        {
            if (!_shapeCollection.ContainsKey(shapeId))
            {
                throw new Exception(String.Format("Error, Shape Id {0} not found", shapeId));
            }
            else
            {
                var shape = _shapeCollection[shapeId];
                shape.Painter.DetachFromShader(_dx);
                _shapeCollection.Remove(shapeId);
            }

        }


        public void UpdateShape(UpdateShapeViewModel model)
        {
            if (!_shapeCollection.ContainsKey(model.ShapeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified", model.ShapeId));
            }
            ShapeUIBase shape = _shapeCollection[model.ShapeId];


           
            shape.Painter.DetachFromShader(_dx);
            var topology = shape.CreateTopology();
            shape.Painter.AttachToShader(_dx, topology);
            
        }

        [HttpGet]
        public ShapeViewModel CreateShape(string shapeTypeId)
        {
            if (!_shapeTypes.ContainsKey(shapeTypeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified", shapeTypeId));
            }

            var shape = (ShapeUIBase)Activator.CreateInstance(_shapeTypes[shapeTypeId]);
            var render = new SolidColorRender() {Color = Color.Aquamarine}; 

            shape.Painter = render;

            shape.Id = "Shape" + Interlocked.Increment(ref _idCounter).ToString();

            var topology = shape.CreateTopology();
            var shaderShape = render.AttachToShader(_dx, topology);
            _shapeCollection.Add(shape.Id, shape);
            return new ShapeViewModel()
            {
                ShapeType = shapeTypeId,
                ShapeData = shape
            };
        }




    }
}
