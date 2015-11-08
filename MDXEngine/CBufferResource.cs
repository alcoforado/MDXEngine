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
    /// A Constant Buffer resource.
    /// It is use is simple, the user gives the data of type T to be copied to the constant buffer  and the resource and the variable name.
    /// When the shader is draw the shader will check if the buffer changed and need to be loaded again in memory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CBufferResource<T> : IShaderResource where T: struct 
    {
        T _data;
        bool _dataChanged;
        public T Data { 
            get { return _data; } 
            
            set { 
                _data = value;
                _dataChanged = true;
                (this.ObservableDock as ObservableDock).OnChanged();
            } 
        }
        
        Buffer _constantBuffer;

        public IObservable ObservableDock { get; set; }
        
        public CBufferResource(IDxContext context)
        {
            var dx = context;
           _constantBuffer = new Buffer(dx.Device, Utilities.SizeOf<T>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
           _dataChanged = false;
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

        public void UpdateBuffer()
        {
        }


        public void Load(HLSLProgram program, String varName) 
        {
           
            var slot = program.ProgramResourceSlots[varName].Value;
        


            if (_dataChanged)
            {
                program.DxContext.DeviceContext.UpdateSubresource(ref _data, _constantBuffer);
                _dataChanged = false;
            }
            if (slot.Resource != this)
            {
                if (slot.ShaderStage == ShaderStage.PixelShader)
                    program.DxContext.DeviceContext.PixelShader.SetConstantBuffer(slot.SlotId, _constantBuffer);
                else if (slot.ShaderStage == ShaderStage.VertexShader)
                    program.DxContext.DeviceContext.VertexShader.SetConstantBuffer(slot.SlotId, _constantBuffer);
                slot.Resource = this;
            }
        
        }
    }
}
