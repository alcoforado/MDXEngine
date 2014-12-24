using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SharpDX;


//namespace SharpDX
//{
//    public delegate int VertexDataOperator<T>(ref T data, int index);

//    class ShapeInfo<T>
//    {
//        internal Shape shape;
//        internal int nLVert;//Number of local vertices
//        internal int nLInd; //Number of  local indices 
//        internal int off;
//        //internal VertexDataOperator<T> op;
//        internal IVertexDataOperator<T>[] ops;No
//        public VertexDataOperator<T> op;
        
//        public ShapeInfo(Shape pshape)
//        {
//            shape = pshape;
//            nLVert = pshape.nVertices();
//            nLInd = pshape.nIndices();
            

//        }
//        public ShapeInfo(Shape pshape,VertexDataOperator<T> op)
//        :this(pshape)
//        {
//            this.op = op;
            
//        }
        
//        public ShapeInfo(Shape pshape, params IVertexDataOperator<T>[] ops)
//        :this(pshape)
//        {
//            this.ops = ops;
        
//        }
        
    
//    }

//    class SubArrayIndex : IArrayIndex
//    {
//        int _displacement;
//        protected int _off, _size;
//        protected int[] _data;

//        public void SetSubArray(int[] array, int off, int disp, int size) { _data = array; _off = off; _displacement = disp; _size = size; }
//        public int this[int index] { set { _data[index + _off] = value + _displacement; } }
//    }


//    public class ShapeMarshalling<T>
//    {
//        List<ShapeInfo<T>> _shapes=new List<ShapeInfo<T>>();
//        int _nVertTotal=0;
//        int _nIndTotal=0;
//        bool _bComputed=false;
        
        
//        T[] _verticesHeap {get; set;}
//        int[]    _indicesHeap { get; set; }
        
        
//        SubArrayVector3<T> _posAdapter;
//        SubArrayIndex _localIndices;


//        DataStream _streamV, _streamI;


//        public DataStream StreamI { get { return _streamI; } }
//        public DataStream StreamV { get { return _streamV; } }
//        public int TotalIndices { get { return _nIndTotal; } }

//        public ShapeMarshalling(SubArrayVector3<T> positionAdapter)
//        {
//            _posAdapter = positionAdapter;
//            _localIndices = new SubArrayIndex();
//        }
        
//        public bool NeedsUpdate()
//        {
//            return !_bComputed;
//        }

//        public void AddShape(Shape sh, params IVertexDataOperator<T>[] ops)
//        {
//            var shInfo = new ShapeInfo<T>(sh, ops);
//            _nVertTotal += shInfo.nLVert;
//            _nIndTotal += shInfo.nLInd;
//            _shapes.Add(shInfo);

//        }
//        public void AddShape(Shape sh,VertexDataOperator<T> op)
//        {
//            var shInfo = new ShapeInfo<T>(sh,op);
//            _nVertTotal += shInfo.nLVert;
//            _nIndTotal  += shInfo.nLInd;
//            _shapes.Add(shInfo);
        
//        }
       



//        public void Clear()
//        {
            
//            _nVertTotal =0;
//            _nIndTotal =0;
//            _bComputed = false;
//            _shapes.Clear();


//        }


//        public void Marshall()
//        {
//            Debug.Assert(!_bComputed);
//            _bComputed = true;
//            _verticesHeap= new T[_nVertTotal];
//            _indicesHeap=  new int[_nIndTotal];
           
//            int offV=0;
//            int offI=0;

           


//            foreach (var shInfo in _shapes)
//            {
//                shInfo.off = offV;
//                _posAdapter.SetSubArray(_verticesHeap, offV, shInfo.nLVert);
//                _localIndices.SetSubArray(_indicesHeap, offI, offV, shInfo.nLInd);   



//                //Shape write to subvectors
//                shInfo.shape.write(_posAdapter,_localIndices);

//                if (shInfo.op != null)
//                    for (int i=0;i<shInfo.nLVert;i++)
//                        shInfo.op(ref  _verticesHeap[i+offV],i);

//                if (shInfo.ops != null)
//                    for (int i = 0; i < shInfo.nLVert; i++)
//                        foreach (IVertexDataOperator<T> op in shInfo.ops)
//                            op.apply(ref  _verticesHeap[i + offV], i);
                   
               
               
               
                
//                offI += shInfo.nLInd;
//                offV += shInfo.nLVert;
                
               
                
                
//            }
//            _streamI = new DataStream(_indicesHeap, true, true);
//            _streamV = new DataStream(_verticesHeap, true, true);
//            _streamI.Position = 0;
//            _streamV.Position = 0;
//         }

//        public T[] getVertices()
//        {
//            if (!_bComputed)
//                Marshall();
//            return _verticesHeap;
//        }
//        public int[] getIndices()
//        {
//            if (!_bComputed)
//                Marshall();
//            return _indicesHeap;
//        }

        
    
//    }
    
//}

