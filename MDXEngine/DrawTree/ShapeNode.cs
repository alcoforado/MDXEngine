using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    
    internal class ShapeNode<T>
    {
        public int OffI, OffV;
        public int SizeI, SizeV;
        public IShape<T> Shape { get; set; }
        private TopologyType _topology;
        private bool _isIndexed;

        public bool IsIndexed()
        {
            return _isIndexed;
        }

        public TopologyType GetTopology()
        {
            return _topology;
        }

        public ShapeNode(IShape<T> shape)
        {

                OffI = -1;
                OffV = -1;
                Shape = shape;
                _isIndexed = shape.NIndices() != 0;
                _topology = shape.GetTopology();
        }

    }

   



}
