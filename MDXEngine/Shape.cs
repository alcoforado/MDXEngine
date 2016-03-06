using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    class Shape<T> : Observable<IShape<T>>, IShape<T>  where T: IPosition
    {
        ITopology    _topology;
        IPainter<T> _renderer;

        public Shape(ITopology topology,IPainter<T> painter)
        {
            _topology = topology;
            _renderer = painter;
        }

        public int NIndices() {return _topology.NIndices();}
        public int NVertices() { return _topology.NVertices(); }
        public void Write(SubArray<T> vV, IArray<int> vI)
        {
            var vVPos = new Vertex3DArray<T>(vV);
            _topology.Write(vVPos, vI);
            _renderer.Write(vV,vI,_topology.GetTopologyType());
        }
        public TopologyType GetTopology() { return _topology.GetTopologyType(); }

        public List<ResourceLoadCommand> GetResourcesLoadCommands()
        {
            return this._renderer.GetLoadResourcesCommands();
        }
         
    }
}
