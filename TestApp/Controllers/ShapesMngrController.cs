using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using MDXEngine;
using TestApp.Models.ShapesManagerService.Render;
using TestApp.Models.ShapesManagerService.Topologies;
using MUtils.Reflection;
using Newtonsoft.Json;

namespace TestApp.Controllers
{
    public class ShapesMngrController : IController
    {
        private Dictionary<string, Type> _typeMap;
        private int _idCounter=0;
        private IDxViewControl _dx;
        private Dictionary<string, ShapeBaseViewModel> _shapeCollection;
        private Dictionary<string, object> _shaderShapeCollection;

        public List<string> GetTopologies()
        {
            return (
                from x in _typeMap
                select x.Key
                ).ToList();;
        }


        public ShapesMngrController(IDxViewControl dx)
        {
            var painters = new List<RenderBaseViewModel>() {new SolidColorRenderBase()};
            _dx = dx;
            _shapeCollection = new Dictionary<string, ShapeBaseViewModel>();
            _shaderShapeCollection = new Dictionary<string, object>();
            _typeMap = new Dictionary<string, Type>();

            var shapesT = typeof(ShapeBaseViewModel).GetImplementationsInCurrentAssembly();
            foreach (var type in shapesT)
            {
                _typeMap.Add(((ShapeBaseViewModel) Activator.CreateInstance(type)).GetShapeName(),type);
            }

            var paintersT = typeof(RenderBaseViewModel).GetImplementationsInCurrentAssembly();
            foreach (var type in paintersT)
            {
                _typeMap.Add(((RenderBaseViewModel)Activator.CreateInstance(type)).GetPainterName(), type);
            }


            

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
                var shaderShape = _shaderShapeCollection[shapeId];
                shape.Painter.DetachFromShader(_dx, shaderShape);
                if (shaderShape is IDisposable)
                    ((IDisposable)shape).Dispose();
                _shapeCollection.Remove(shapeId);
            }

        }

       
        public void UpdateShape(string shapeId,string shapeJsonData,string painterId, string painterJsonData)
        {
            if (!_shapeCollection.ContainsKey(shapeId) || !_shapeCollection.ContainsKey(painterId))
            {
                throw new Exception(String.Format("Error, type {0} not identified",shapeId));
            }
            var shape = _shapeCollection[shapeId];

            
          JsonConvert.PopulateObject(shapeJsonData, shape);
            JsonConvert.PopulateObject(painterJsonData, shape.Painter);



            var shaderShape = _shaderShapeCollection[shapeId];
            shape.Painter.DetachFromShader(_dx, shaderShape);
            if (shaderShape is IDisposable)
                ((IDisposable) shaderShape).Dispose();

           
            var topology = shape.CreateTopology();
            shaderShape = shape.Painter.AttachToShader(_dx, topology);
            _shaderShapeCollection.Add(shapeId,shaderShape);
        }

        public ShapeBaseViewModel CreateShape(string shapeTypeId, string renderTypeId)
        {
            if (!_typeMap.ContainsKey(shapeTypeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified", shapeTypeId));
            }
            if (!_typeMap.ContainsKey(renderTypeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified", renderTypeId));
            }

            var shape = (ShapeBaseViewModel)Activator.CreateInstance(_typeMap[shapeTypeId]);
            var render = (RenderBaseViewModel)Activator.CreateInstance(_typeMap[renderTypeId]);

            shape.Painter = render;

            shape.Id = "Shape" + Interlocked.Increment(ref _idCounter).ToString();

            var topology = shape.CreateTopology();
            var shaderShape = render.AttachToShader(_dx, topology);
            _shaderShapeCollection.Add(shape.Id, shaderShape);
            _shapeCollection.Add(shape.Id,shape);
            return shape;
        }
       
    }
}
