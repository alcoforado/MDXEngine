﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree
{
    public class ShapeGroup<T>
    {
        public int OffI, OffV;
        public int SizeI, SizeV;
        public bool Changed;
        private TopologyType _topology;
        private bool _isIndexed;
        public CommandsSequence Commands { get; private set; }
        public ShapeGroup(bool isIndexed, TopologyType topology, CommandsSequence commands = null)
        {
                Changed = true;
                OffI = -1;
                OffV = -1;
                _isIndexed = isIndexed;
                _topology = topology;
                Commands = commands;

        }

        public ShapeGroup(IShape<T> shape, CommandsSequence commands = null)
            
        {
            Changed = true;
            OffI = -1;
            OffV = -1;
            _isIndexed = shape.NIndices() != 0;
            _topology = shape.GetTopology();
            Commands = commands;
        }


        public bool IsIndexed()
        {
            return _isIndexed;
        }

        public TopologyType GetTopology()
        {
            return _topology;
        }

        public bool CanHaveShapeAsChild(IShape<T> shape)
        {
                return ((shape.NIndices() != 0) == _isIndexed) &&
                (shape.GetTopology() == _topology);

        }


    }
}
