using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    public class Orthomesh2DViewModel : ITopologyViewModel
    {
        public string GetTopologyName()
        {
            return "OrthoMesh2D";
        }

        public int NumElemsX { get; set; }
        public int NumElemsY { get; set; }
        public Vector2 Dims { get; set; }
    }
}
