using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public interface IArray<T>
    {
        T this[int index] { get; set; }
        int Length { get; }
        void CopyFrom(T[] array);

    }
}
