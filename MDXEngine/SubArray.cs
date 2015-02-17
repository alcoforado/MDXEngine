using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MDXEngine
{

        public class SubArray<T> : IArray<T>
        {
            public int start;
            T[] data;
            public int size;
            
            public SubArray(T[] data, int start, int size)
            {
                this.start = start;
                this.size = size;
                this.data = data;
            }

            public SubArray(T[] data)
            {
                this.start = 0;
                this.size = data.Length;
                this.data = data;
            }

            public SubArray(SubArray<T> sub,int start,int size)
            {
                this.start = sub.start + start;
                this.size = size;
                this.data = sub.data;
            }

            public int Length 
            {
                get { return size; } 
            
            }

            public void CopyFrom(T[] array)
            {
                Debug.Assert(array.Length == this.Length);
                Array.Copy(array,0,data,this.start,this.Length);
            }

            public void CopyTo(T[] array)
            {
                Debug.Assert(array.Length == this.Length);
                Array.Copy(data, this.start, array, 0, this.Length);
            }


            public void reinit(T[] lst,int start,int size)
            {
                Debug.Assert(start + size < lst.Count());
                this.size = size;
                this.start = start;
                this.data = lst;
            }
            


            public T this[int index]
            {
                get { 
                    Debug.Assert(index < size,"Index Out Of Range");
                    Debug.Assert(start+size <= data.Length,"Array Changed");
                    return data[index + start]; 
                }
                set {
                    Debug.Assert(index < size, "Index Out Of Range");
                    Debug.Assert(start + size <= data.Length, "Array Changed");
                    data[index+start]=value;
                }
            }


     
 
        }

}
