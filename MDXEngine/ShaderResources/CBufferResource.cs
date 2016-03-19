using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;
namespace MDXEngine
{
    /// <summary>
    /// A Constant Buffer resource. This is a wrapper for a constant buffer and represents an allocation
    /// of a type structure in gpu memory.
    /// It is use is simple, the user gives the data of type T to be copied to the constant buffer  and the resource and the variable name.
    /// When the shader is draw the shader will check if the buffer changed and need to be loaded again in memory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CBufferResource<T> : IShaderResource where T : struct
    {
        private readonly IDxContext _dx;


        private readonly Buffer _constantBuffer;

        public IObservable ObservableDock { get; set; }

        public CBufferResource(IDxContext dx)
        {
            _dx = dx;
            _constantBuffer = new Buffer(dx.Device, Utilities.SizeOf<T>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            ObservableDock = new ObservableDock();

        }
        public bool IsDisposed()
        {
            return _constantBuffer.IsDisposed;
        }

        public void Dispose()
        {
            _constantBuffer.Dispose();

        }

        public void Load(object data)
        {
            var dt = (T)data;
            _dx.DeviceContext.UpdateSubresource(ref dt, _constantBuffer);
        }



        public void Bind(IShaderProgram program, String varName)
        {

            var slot = program.ProgramResourceSlots[varName].Value;


            if (slot.ShaderStage == ShaderStage.PixelShader)
                program.DxContext.DeviceContext.PixelShader.SetConstantBuffer(slot.SlotId, _constantBuffer);
            else if (slot.ShaderStage == ShaderStage.VertexShader)
                program.DxContext.DeviceContext.VertexShader.SetConstantBuffer(slot.SlotId, _constantBuffer);

        }
    }
}
