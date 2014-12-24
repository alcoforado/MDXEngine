using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine
{
    public class Vertex2DArray<T> : IArray<Vector2> where T : IPosition2D
    {
        SubArray<T> vSub;

        public Vertex2DArray(T[] data, int start, int size)
        {
            vSub = new SubArray<T>(data, start, size);
        }

        public int Length { get { return vSub.size; } }

        public Vector2 this[int index]
        {
            get
            {
                return vSub[index].Position;
            }
            set
            {
                vSub[index].Position = value;
            }
        }
    }


}
