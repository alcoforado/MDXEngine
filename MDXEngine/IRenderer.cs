using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDXEngine
{
    public interface IRenderer<T>
    {
        void write(IArray<T> vV);
    }
}
