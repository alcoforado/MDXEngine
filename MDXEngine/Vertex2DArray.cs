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

        public Vector2 this[int index]
        {
            get
            {
                return _data[index].Position2D;
            }
            set
            {
                _data[index].Position2D = value;
            }
        }
    }


}
