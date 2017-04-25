﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.ShapesManagerService.Render;
using TestApp.Models.ShapesManagerService.Topologies;

namespace TestApp.WebApi.Models.ShapeMngr
{
    public class RenderViewModel
    {
        public string TypeName { get; set; }
        public RenderBase RenderData { get; set; }
    }
}