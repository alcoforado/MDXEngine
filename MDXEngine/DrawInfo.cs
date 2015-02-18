using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace MDXEngine
{

    internal enum DrawInfoType { SHAPE_GROUP, SHAPE, ROOT }

   
    
    internal class DrawInfo<T>
    {
        private bool _isIndexed;
        private TopologyType _topology;

        public DrawInfoType Type { get; set; }
        public IShape<T> Shape { get; set; }
        private IDrawTreeAction Action { get; set; }
        public int OffI, OffV;
        public int SizeI, SizeV;
        public bool Changed;

        public void ExecuteAction()
        {
            if (Action != null)
                Action.Execute();
        }
        
        public bool HasAction()
        {
            return Action != null;
        }

        public bool IsIndexed()
        {
            Debug.Assert(Type == DrawInfoType.SHAPE || Type == DrawInfoType.SHAPE_GROUP);
            return _isIndexed;
        }

        public TopologyType GetTopology()
        {
            Debug.Assert(Type == DrawInfoType.SHAPE || Type == DrawInfoType.SHAPE_GROUP);
            return _topology;
        }

        public bool CanHaveShapeAsChild(IShape<T> shape)
        {
            return Type == DrawInfoType.SHAPE_GROUP &&
                ((shape.NIndices() != 0) == _isIndexed) &&
                (shape.GetTopology() == _topology);

        }

        static public DrawInfo<T> CreateRoot()
        {
            return new DrawInfo<T>
            {
                Changed = true,
                OffI = -1,
                OffV = -1,
                Type = DrawInfoType.ROOT,
                Shape = null,
                Action = null
            };
            
        }
        
        /*
        static public DrawInfo<T> CreateCommandSequence(IDrawTreeAction action)
        {
            return new DrawInfo<T>
            {
                Changed=true,
                OffI=-1,
                OffV=-1,
                Type = DrawInfoType.COMMAND_SEQUENCE,
                Shape = null,
                Action =  action

            };
        }
        */
        static public DrawInfo<T> CreateShape(IShape<T> shape)
        {
            return new DrawInfo<T>
            {
                Changed = true,
                OffI = -1,
                OffV = -1,
                Shape = shape,
                _isIndexed = shape.NIndices() != 0,
                _topology = shape.GetTopology(),
                Type = DrawInfoType.SHAPE,
                Action=null
            };

        }

        static public DrawInfo<T> CreateShapeGroup(bool isIndexed, TopologyType topology,IDrawTreeAction action=null)
        {
            return new DrawInfo<T>
            {
                Changed=true,
                OffI=-1,
                OffV=-1,
                Shape      = null,
                Type       = DrawInfoType.SHAPE_GROUP,
                _isIndexed = isIndexed,
                _topology  = topology,
                Action=action
            };

        }
        static public DrawInfo<T> CreateGroupForShape(IShape<T> shape)
        {
            return CreateShapeGroup(shape.NIndices() != 0, shape.GetTopology());
        }




    } 

}
