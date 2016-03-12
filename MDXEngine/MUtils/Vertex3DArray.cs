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
       public int start;
            T[] data;
            public int size;
            
            public Vertex3DArray(T[] data, int start, int size)
            {
                this.start = start;
                this.size = size;
                this.data = data;
            }

            public Vertex3DArray(T[] data)
            {
                this.start = 0;
                this.size = data.Length;
                this.data = data;
            }

            public Vertex3DArray(SubArray<T> sub,int start,int size)
            {
                this.start = sub.start + start;
                this.size = size;
                this.data = sub.GetData();
            }

            public Vertex3DArray(SubArray<T> sub)
            {
                this.start = sub.start;
                this.size = sub.size;
                this.data = sub.GetData();
            }



            public int Length 
            {

                get { return size; } 
            
            }


            public void CopyFrom(Vector3[] array)
            {
                Debug.Assert(array.Length == this.Length);
                for (int i=0;i<this.Length;i++)
                {
                    this.data[i+start].Position = array[i];
                }
            }


            public void reinit(T[] lst,int start,int size)
            {
                Debug.Assert(start + size < lst.Count());
                this.size = size;
                this.start = start;
                this.data = lst;
            }
            


            public Vector3 this[int index]
            {
                get { 
                    Debug.Assert(index < size,"Index Out Of Range");
                    Debug.Assert(start+size <= data.Length,"Array Changed");
                    return data[index + start].Position; 
                }
                set {
                    Debug.Assert(index < size, "Index Out Of Range");
                    Debug.Assert(start + size <= data.Length, "Array Changed");
                    data[index+start].Position=value;
                }
            }

        
        
      
        }
    }

