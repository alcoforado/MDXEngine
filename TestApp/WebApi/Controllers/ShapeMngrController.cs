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
using TestApp.Services.Interfaces;
using TestApp.WebApi.Models.ShapeMngr;

namespace TestApp.WebApi.Controllers
{
    public class ShapeMngrController : ApiController
    {
        
        private int _idCounter = 0;
        private IShapesMngrMapper _mapper;
        private IShapeMngrService _mngrService;

        public List<string> GetTopologies()
        {
            return (
                from x in _mngrService.GetShapeTypes()
                select x.Key
                ).ToList(); ;
        }


        public ShapeMngrController(IShapeMngrService mngrService, IShapesMngrMapper mapper)
        {
            _mapper = mapper;
            _mngrService = mngrService;
        }

        [HttpGet]
        public List<UIType> ShapeTypes()
        {
            return _mngrService.GetShapeTypes().Select(pair => _mapper.ToUITypeDto(pair.Key, pair.Value)).ToList();
        }

        [HttpGet]
        public List<UIType> PainterTypes()
        {
            return _mngrService.GetRenderTypes().Select(pair => _mapper.ToUITypeDto(pair.Key, pair.Value)).ToList();
        }


        [HttpGet]
        public List<ShapeViewModel> Shapes()
        {
            return _mngrService.GetShapes().Select(x => new ShapeViewModel()
            {
                TypeName = x.Value.GetShapeName(),
                ShapeData = x.Value
            }).ToList();
        }

        public void DeleteShape(string shapeId)
        {
            _mngrService.DeleteShape(shapeId);
        }


        public void UpdateShape(UpdateShapeViewModel model)
        {
            
            ShapeUIBase shape = _mngrService.GetShape(model.ShapeId);
            JsonConvert.PopulateObject(model.ShapeJsonData, shape);
            
            var render = _mngrService.CreateRender(model.ShapePainterType);
            JsonConvert.PopulateObject(model.ShapePainterJsonData,render);
            _mngrService.SetShapeRender(model.ShapeId, render);
        }

        [HttpPut]
        public ShapeViewModel CreateShape(string shapeTypeId)
        {
            var shape=_mngrService.CreateShape(shapeTypeId);
        
            return new ShapeViewModel()
            {
                TypeName = shapeTypeId,
                ShapeData = shape,
                RenderData = shape.Render,
                RenderType = shape.Render.GetType().Name
            };
        }




    }
}
