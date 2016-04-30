using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDXEngine;
using TestApp.Models.ShapesManagerService.Render;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    interface IShapeViewModel
    {
        string GetShapeName();
        string Id { get; set; }

        IRenderViewModel Render { get; set; }

        ITopology CreateTopology();
    }
}
