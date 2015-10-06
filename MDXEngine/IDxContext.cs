using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace MDXEngine
{
    public interface IDxContext
    {
        Device Device { get;  }
        DeviceContext DeviceContext { get; }
        HLSLProgram CurrentProgram { get; set; }
        ResourcesManager ResourcesManager { get; }
        Camera Camera { get; }
        bool IsCameraChanged { get; }
        System.Drawing.Size ScreenSize { get; }
    }


}
