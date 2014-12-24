using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace MDXEngine
{
    public class Vertex3DArray<T> : IArray<Vector3> where T : IPosition
    {
        IArray<T> data;

        public Vertex3DArray(IArray<T> data)
        {
            this.data = data;
        }

        public int Length{get {return data.Length;}}

        public Vector3 this[int index]
        {
            get
            {
                return data[index].Position;
            }
            set
            {
                data[index].Position = value;
            }
        }
    }
}
