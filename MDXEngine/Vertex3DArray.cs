using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace MDXEngine
{
    public class Vertex3DArray<T> : IArray<Vector3> where T : IPosition
    {
        IArray<T> _data;

        public Vertex3DArray(IArray<T> data)
        {
            this._data = data;
        }

        public int Length{get {return _data.Length;}}


        public void CopyFrom(Vector3[] array)
        {
            Debug.Assert(array.Length == this.Length);
            var len = this.Length;
            for (int i = 0; i < len; i++)
            {
                this[i] = array[i];
            }
        }


        public Vector3 this[int index]
        {
            get
            {
                return _data[index].Position;
            }
            set
            {
                var elem = _data[index];
                elem.Position = value;
                _data[index] = elem;
            }
        }
    }
}
