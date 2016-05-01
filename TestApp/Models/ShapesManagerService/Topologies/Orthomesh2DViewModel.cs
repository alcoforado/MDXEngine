using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine;
using SharpDX;
using TestApp.Models.ShapesManagerService.Render;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    public class Orthomesh2DViewModel : IShapeViewModel
    {
        public string GetShapeName()
        {
            return "OrthoMesh2D";
        }
        public IRenderViewModel Render { get; set; }
        public ITopology CreateTopology()
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; }
        public int NumElemsX { get; set; }
        public int NumElemsY { get; set; }
        public Vector2 Dims { get; set; }
    }
}
