using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TestApp.Models.ShapesManagerService.Topologies;

namespace TestApp.Controllers
{
    public class ShapesMngrController : IController
    {
        private List<ITopologyViewModel> _topologies;
        private Dictionary<string, ITopologyViewModel> _typeMap;

        public List<string> GetTopologies()
        {
            return (
                from x in _topologies
                select x.GetTopologyName()
                ).ToList();;
        }


        public ShapesMngrController()
        {
            _topologies = new List<ITopologyViewModel>(){new Orthomesh2DViewModel()};
            _typeMap = _topologies.ToDictionary(x => x.GetTopologyName(), x => x);
        }

        public void CreateShape(string typeId, string jsonData)
        {
            if (!_typeMap.ContainsKey(typeId))
            {
                throw new Exception(String.Format("Error, type {0} not identified",typeId));
            }

            var serializer = new JavaScriptSerializer();
            var topology = (ITopologyViewModel) serializer.Deserialize(jsonData, _typeMap[typeId].GetType());

            topology.CreateShape();
        }

       
    }
}
