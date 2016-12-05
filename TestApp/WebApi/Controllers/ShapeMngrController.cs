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
        public List<UIType> GetShapeTypes()
        {
            return _mngrService.GetShapeTypes().Select(pair => _mapper.ToUITypeDto(pair.Key, pair.Value)).ToList();
        }

        public List<UIType> GetPainterTypes()
        {
            return _mngrService.GetRenderTypes().Select(pair => _mapper.ToUITypeDto(pair.Key, pair.Value)).ToList();
        }

       
        public List<ShapeViewModel> GetShapes()
        {
            return _mngrService.GetShapes().Select(x => new ShapeViewModel()
            {
                ShapeType = x.Value.GetShapeName(),
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

        [HttpGet]
        public ShapeViewModel CreateShape(string shapeTypeId)
        {
            var shape=_mngrService.CreateShape(shapeTypeId);
        
            return new ShapeViewModel()
            {
                ShapeType = shapeTypeId,
                ShapeData = shape
            };
        }




    }
}
