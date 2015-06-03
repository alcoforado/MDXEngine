using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;
namespace MDXEngine
{
    /// <summary>
    /// Incorporate the idea of a resource loaded in the program.
    /// It is use is simple, the user gives the data of type T to be copied to the constant buffer  and the resource and the variable name.
    /// When the shader is draw the shader will check if the buffer changed and need to be loaded again in memory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CBufferResource<T> : IShaderResource where T: struct 
    {
        Buffer _constantBuffer;
        public CBufferResource(HLSLProgram program,string varName)
        {
           var dx = program.DxContext;
           _constantBuffer = new Buffer(dx.Device, Utilities.SizeOf<T>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

        }
        public bool IsDisposed()
        {
            return _constantBuffer.IsDisposed;
        }

        public void Dispose()
        {
            _constantBuffer.Dispose();
        
        }
        public void Load(HLSLProgram program, String varName) { }
    }
}
