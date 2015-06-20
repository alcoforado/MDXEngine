using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public interface IShader : IDisposable, IObservable
    {
        void Draw(IDxContext dx);
    }
}
