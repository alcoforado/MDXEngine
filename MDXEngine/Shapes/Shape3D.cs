﻿using MDXEngine.DrawTree;
using MDXEngine.Painters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shapes
{
    public class Shape3D<T> : IShape<T> where T : IPosition
    {
        ITopology _topology;
        IPainter<T> _renderer;
        
        
       
        
        public Shape3D(ITopology topology, IPainter<T> renderer)
        {
            _topology = topology;
            _renderer = renderer;
        }

        public Shape3D(ITopology topology)
        {
            _topology = topology;
            _renderer = new EmptyPainter<T>();
        }



        public int NVertices() { return _topology.NVertices(); }
        public int NIndices() { return _topology.NIndices(); }
        public TopologyType GetTopology() { return _topology.GetTopologyType(); }
        public void Write(SubArray<T> vV, IArray<int> vI)
        {
            var vV1 = new Vertex3DArray<T>(vV);
            _topology.Write(vV1, vI);
           _renderer.Write(vV, vI, _topology.GetTopologyType());
        }

        public List<ResourceLoadCommand> GetResourcesLoadCommands()
        {
         
                return this._renderer.GetLoadResourcesCommands();
        }
         

    }

}
