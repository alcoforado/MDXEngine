using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using System.Diagnostics;
namespace MDXEngine
{
    public class Vertex2DArray<T> : IArray<Vector2> where T : IPosition2D
    {
        IArray<T> _data;

        public Vertex2DArray(IArray<T> data)
        {
            _data = data;
        }

        public int Length { get { return _data.Length; } }
        
        public void CopyFrom(Vector2[] array)
        {
            Debug.Assert(array.Length == this.Length);
            var len = this.Length;
            for (int i = 0; i < len; i++)
            {
                this[i] = array[i];
            }
        }

        public void CopyTo(Vector2[] array)
        {
            Debug.Assert(array.Length == this.Length);
            var len = this.Length;
            for (int i = 0; i < len; i++)
            {
                array[i]=this[i];
            }
        }




        public Vector2 this[int index]
        {
            get
            {
                return _data[index].Position2D;
            }
            set
            {
                var elem = _data[index];
                elem.Position2D = value;
                _data[index] = elem;
            }
        }



    }


}
