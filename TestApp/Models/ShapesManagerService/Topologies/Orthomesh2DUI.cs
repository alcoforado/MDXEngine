using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MDXEngine;
using SharpDX;
using TestApp.Models.ShapesManagerService.Render;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    public class Orthomesh2DUI : ShapeUIBase
    {
        public override string GetShapeName()
        {
            return "OrthoMesh2D";
        }


        public Orthomesh2DUI ()
        {
            NumElemsX = 2;
            NumElemsY = 2;
            Width = 1;
            Length = 1;
        }

        protected  override ITopology CreateTopology()
        {
            var Dims = new Vector2(Width,Length);
            if (NumElemsX <= 0) 
                throw new Exception("Number of elements in X must be greater than 0");
            if (NumElemsY <= 0)
                throw new Exception("Number of elements in Y must be greater than 0");
            if (Length <= 0f || Width <= 0f)
                throw new Exception("Dimensions must be positive");
            Vector2 O = new Vector2(0,0) - Dims*0.5f;
            Vector2 P = new Vector2(0,0) + Dims*0.5f;
            return new OrthoMesh2D((uint) NumElemsX,(uint) NumElemsY,O,P);
               
        }

        public int NumElemsX { get; set; }
        public int NumElemsY { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
    }
}
