using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
namespace MDXEngine
{
    public class DxContext : IDxContext
    {
        private HLSLProgram _hlslProgram;
        public Device Device { get; set; }
        public DeviceContext DeviceContext { get { return Device.ImmediateContext; } }
        public Camera Camera { get; set; }
        public bool IsCameraChanged { get; set; }
        public System.Drawing.Size ScreenSize { get; set; }
        public HLSLProgram CurrentProgram
        {
            get
            {
                return _hlslProgram;
            }
            set
            {
                DeviceContext.VertexShader.Set(value.VertexShader);
                DeviceContext.InputAssembler.InputLayout = value.GetLayout();
                DeviceContext.PixelShader.Set(value.PixelShader);
                _hlslProgram = value;
            }

        }
    }
}
