using System;
using SharpDX.Direct3D11;

namespace MDXEngine
{
    public interface IShaderResource : IDisposable
    {
        void Load(HLSLProgram program, String varName);
        bool IsDisposed();
        IObservable ObservableDock { get; }

    }
}
