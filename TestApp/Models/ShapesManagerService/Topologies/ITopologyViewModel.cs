using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    interface ITopologyViewModel
    {
        string GetTopologyName();
        string Id { get; set; }

    }
}
