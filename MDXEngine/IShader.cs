using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public interface IShader : IDisposable
    {
        void Draw(IDxContext dx);
        IObservable ObservableDock { get; } //An IObservable used by observers to attach themselves
    
    }
}
