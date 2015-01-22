using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDXEngine
{
    public interface IPainter<T>
    {
        void Write(IArray<T> vV);
    }
}
