using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.ShapesManagerService.Topologies;

namespace TestApp.WebApi.Models.ShapeMngr
{
    public class ShapeViewModel
    {
        public string TypeName { get; set; }
        public ShapeUIBase ShapeData { get; set; }
    }
}
