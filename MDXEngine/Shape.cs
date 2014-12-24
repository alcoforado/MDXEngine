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
        IRenderer<T> _renderer;


        public int NIndices() {return _topology.NIndices();}
        public int NVertices() { return _topology.NVertices(); }
        public void Write(IArray<T> vV,IArray<int> vI)
        {
            var vVPos = new Vertex3DArray<T>(vV);
            _topology.Write(vVPos, vI);
            _renderer.write(vV);
        }
        public TopologyType GetTopology() { return _topology.GetTopologyType(); }
    }
}
