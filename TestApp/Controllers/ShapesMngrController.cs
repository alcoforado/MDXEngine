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
namespace TestApp.Controllers
{
    public class ShapesMngrController : IController
    {
        private Dictionary<string, Type> _typeMap;
        private int _idCounter=0;
        private IDxViewControl _dx;
        private Dictionary<string, ShapeBaseViewModel> _shapeCollection;
   

        public List<string> GetTopologies()
        {
            return (
                from x in _typeMap
                select x.Key
                ).ToList();;
        }


        public ShapesMngrController(IDxViewControl dx)
        {
            var shapes = new List<ShapeBaseViewModel>(){new Orthomesh2DBaseViewModel()};
            var painters = new List<RenderBaseViewModel>() {new SolidColorRenderBase()};
            _dx = dx;
            
            _typeMap =  shapes.ToDictionary(x => x.GetShapeName(), x => x.GetType());
            foreach (var pt in painters)
            {
                _typeMap[pt.GetPainterName()] = pt.GetType();
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
                shape.RenderBase.DetachFromShader(_dx, shape.ShaderShape);
                if (shape.ShaderShape is IDisposable)
                    ((IDisposable)shape).Dispose();
                _shapeCollection.Remove(shapeId);
            }

        }

        //TODO: Update Shape
        public void UpdateShape(string shapeId,string shapeJsonData,string painterId, string painterJsonData)
        {
            if (!_shapeCollection.ContainsKey(shapeId) || !_shapeCollection.ContainsKey(painterId))
            {
                throw new Exception(String.Format("Error, type {0} not identified",shapeId));
            }
            var shape = _shapeCollection[shapeId];

            
            var serializer = new JavaScriptSerializer();
            var newShape = serializer.Deserialize(shapeJsonData, shape.GetType());
            var painter = serializer.Deserialize(painterJsonData, shape.RenderBase.GetType());

            

            shape.CopyPublicPropertiesFrom(newShape);
            shape.RenderBase.CopyPublicPropertiesFrom(painter);

            shape.RenderBase.DetachFromShader(_dx, shape.ShaderShape);
            if (shape.ShaderShape is IDisposable)
                ((IDisposable) shape).Dispose();

           
            var topology = shape.CreateTopology();
            shape.ShaderShape = shape.RenderBase.AttachToShader(_dx, topology);
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

            shape.RenderBase = render;

            shape.Id = "Shape" + Interlocked.Increment(ref _idCounter).ToString();

            var topology = shape.CreateTopology();
            shape.ShaderShape = render.AttachToShader(_dx,topology);
            _shapeCollection.Add(shape.Id,shape);
            return shape;
        }
       
    }
}
