﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDXEngine;
using TestApp.Models.ShapesManagerService.Render;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    public abstract class  ShapeBaseViewModel
    {
        public string Id { get; set; }

        internal RenderBaseViewModel RenderBase { get; set; }
        internal object ShaderShape { get; set; }

        internal abstract string GetShapeName();
        internal abstract ITopology CreateTopology();
    }
}
