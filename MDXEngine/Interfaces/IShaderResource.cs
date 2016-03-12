using System;
using SharpDX.Direct3D11;

namespace MDXEngine
{
    //A Shader Resource is any object stored in memory that is capable
    //to load itself in a HLSL program. They can be array of structures or matrices,
    //or textures.....
    public interface IShaderResource : IDisposable
    {

        //given an hlslprogram and a variable name, loads itself in the program.
        void Load(HLSLProgram program, String varName);
        
        //copy data to resource
        void SetResourceData(object data);

        //flag to indicate if the resource is disposed
        bool IsDisposed();

        //A doc to register observers into the shader resources to be notified everytime the resurce 
        //changes.
        IObservable ObservableDock { get; }

    }
}
