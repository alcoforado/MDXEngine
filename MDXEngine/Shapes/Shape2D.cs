using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.Shapes
{
    public class Shape2D<T> : IShape<T> where T: IPosition2D
    {
        ITopology2D  _topology;
        IPainter<T> _renderer;
        
        public Shape2D(ITopology2D topology,IPainter<T> renderer)
        {
            _topology = topology;
            _renderer = renderer;
        }

        public int NVertices(){return _topology.NVertices();}
        public int NIndices() { return _topology.NIndices(); }
        public TopologyType GetTopology() {return _topology.GetTopologyType();}
        public void Draw(IDrawContext<T> context)
        {

            var vV1 = new Vertex2DArray<T>(context.Vertices);
            _topology.Write(vV1, context.Indices);
            _renderer.Draw(context);
        }

        public List<SlotRequest> RequestSlotResources()
        {
            return this._renderer.GetLoadResourcesCommands();
        }
         
    }
}
