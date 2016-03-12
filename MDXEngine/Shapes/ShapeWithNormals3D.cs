using MDXEngine.DrawTree;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Shapes
{
    public class ShapeWithNormals3D<T> : IShape<T> where T : IPosition, INormal
    {
        ITopology _topology;
        IPainter<T> _renderer;
        ITopologyNormalProvider _topologyNormalProvider;



        public ShapeWithNormals3D(ITopology topology, IPainter<T> renderer)
        {
            _topology = topology;
            _topologyNormalProvider = topology as ITopologyNormalProvider;
            _renderer = renderer;
            
        }

        public int NVertices() { return _topology.NVertices(); }
        public int NIndices() { return _topology.NIndices(); }
        public TopologyType GetTopology() { return _topology.GetTopologyType(); }
        public void Write(SubArray<T> vV, IArray<int> vI)
        {
            var v = new SubArray<Vector3>(new Vector3[NVertices()]);
            var n = new SubArray<Vector3>(new Vector3[NVertices()]);
            _topology.Write(v, vI);
            if (_topologyNormalProvider != null)
            {
                _topologyNormalProvider.WriteNormalsAtVertices(n);
            }
            else
            {
                _topologyNormalProvider.WriteNormalsAtVertices(n);
            }
           
            _renderer.Write(vV, vI, _topology.GetTopologyType());

        }
        
        public List<SlotData> GetResourcesLoadCommands()
        {

            return this._renderer.GetLoadResourcesCommands();
        }
         

    }
}
