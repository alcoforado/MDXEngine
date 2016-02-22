using MDXEngine;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Interfaces;

namespace TestApp.Models.ShapesManagerService
{
    public class SolidColorRender : IShapesRender
    {
        public SolidColorRender(List<Vector3> colors)
        {
            

        }

        public void Render(IDxViewControl _dxControl, ITopology topology)
        {

        }


    }
}
