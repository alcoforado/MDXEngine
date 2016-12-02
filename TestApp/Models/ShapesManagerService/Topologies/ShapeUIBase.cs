﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDXEngine;
using TestApp.Models.ShapesManagerService.Render;

namespace TestApp.Models.ShapesManagerService.Topologies
{
    public abstract class  ShapeUIBase
    {
        public string Id { get; set; }
        public RenderBaseViewModel Painter { get; set; }
        public string PainterType { get; set; }
        internal  abstract string GetShapeName();
        internal abstract ITopology CreateTopology();
    }
}
