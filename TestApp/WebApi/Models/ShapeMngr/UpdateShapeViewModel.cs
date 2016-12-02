using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.WebApi.Models.ShapeMngr
{
    public class UpdateShapeViewModel
    {
        public string ShapeId { get; set; }
        public string ShapeType { get; set; }
        public string ShapeJsonData { get; set; }
        public string ShapePainterType { get; set; }
        public string ShapePainterJsonData { get; set; }
    }
}
