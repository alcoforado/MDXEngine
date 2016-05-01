using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using MDXEngine;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using TestApp.Models.ShapesManagerService.Render;
using TestApp.Models.ShapesManagerService.Topologies;

namespace TestApp.Controllers
{
    public class ShapesMngrController : IController
    {
        private Dictionary<string, Type> _typeMap;
        private int _idCounter=0;
        private IUnityContainer _container;

        public List<string> GetTopologies()
        {
            return (
                from x in _typeMap
                select x.Key
                ).ToList();;
        }


        public ShapesMngrController(IDxViewControl viewControl)
        {
            var shapes = new List<IShapeViewModel>(){new Orthomesh2DViewModel()};
            var painters = new List<IRenderViewModel>() {new SolidColorRender()};

            
            _typeMap =  shapes.ToDictionary(x => x.GetShapeName(), x => x.GetType());
            foreach (var pt in painters)
            {
                _typeMap[pt.GetPainterName()] = pt.GetType();
            }





        }


        //TODO: Update Shape
        public void UpdateShape(string shapeId,string shapeJsonData,string painterId, string painterJsonData)
        {
            if (!(_typeMap.ContainsKey(shapeId) && _typeMap.ContainsKey(painterId)))
            {
                throw new Exception(String.Format("Error, type {0} not identified",shapeId));
            }

            var serializer = new JavaScriptSerializer();
            var topology = (IShapeViewModel)serializer.Deserialize(shapeJsonData, _typeMap[shapeId].GetType());
            var painter = (IRenderViewModel)serializer.Deserialize(shapeJsonData, _typeMap[shapeId].GetType());

      
        }



        
        public void CreateShape(string shapeTypeId, string renderTypeId)
        {
            if (!_typeMap.ContainsKey(shapeTypeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified", shapeTypeId));
            }
            if (!_typeMap.ContainsKey(renderTypeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified", renderTypeId));
            }

            var shape = (IShapeViewModel)Activator.CreateInstance(_typeMap[shapeTypeId]);
            var render = (IRenderViewModel)Activator.CreateInstance(_typeMap[renderTypeId]);

            shape.Render = render;

            shape.Id = "Topology" + Interlocked.Increment(ref _idCounter).ToString();
            render.Id = "Painter" + Interlocked.Increment(ref _idCounter).ToString();

            var topology = shape.CreateTopology();
            var painter = render.CreateRender();
            Type shaderType = render.GetShaderType();

        }
       
    }
}
