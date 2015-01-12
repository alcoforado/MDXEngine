using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Properties;
using Device = SharpDX.Direct3D11.Device;
namespace MDXEngine
{
    public class DxControl : IDxContext
    {
        Control renderControl;
        Device _device;
        SwapChain _swapChain;
        Texture2D _backBuffer;
        RenderTargetView _renderView;
        Texture2D _depthBuffer;
        DepthStencilView _depthView;
        SwapChainDescription _desc;


        public DxControl(Control control)
        {
            renderControl=control;
            this.InitializeDX();
        }

        public Device Device
        {
            get
            {
                return _device;
            }
        }        

        public void Resize()
        {
            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _renderView);
            Utilities.Dispose(ref _depthBuffer);
            Utilities.Dispose(ref _depthView);

            // Resize the backbuffer
            _swapChain.ResizeBuffers(_desc.BufferCount, renderControl.ClientSize.Width, renderControl.ClientSize.Height, Format.Unknown, SwapChainFlags.None);

            // Get the backbuffer from the swapchain
            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);

            // Renderview on the backbuffer
            _renderView = new RenderTargetView(_device, _backBuffer);

            // Create the depth buffer
            _depthBuffer = new Texture2D(_device, new Texture2DDescription()
            {
                Format = Format.D32_Float_S8X24_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = renderControl.ClientSize.Width,
                Height = renderControl.ClientSize.Height,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            });

            // Create the depth buffer view
            _depthView = new DepthStencilView(_device, _depthBuffer);

            // Setup targets and viewport for rendering
            _device.ImmediateContext.Rasterizer.SetViewport(new Viewport(0, 0, renderControl.ClientSize.Width, renderControl.ClientSize.Height, 0.0f, 1.0f));
            _device.ImmediateContext.OutputMerger.SetTargets(_depthView, _renderView);
        }


        private void InitializeDX()
        {
            _desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
                    new ModeDescription(renderControl.ClientSize.Width, renderControl.ClientSize.Height,
                                        new Rational(60, 1), Format.B8G8R8A8_UNorm),
                IsWindowed = true,
                OutputHandle = renderControl.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device.CreateWithSwapChain(
                DriverType.Hardware,
                DeviceCreationFlags.Debug,
                _desc,
                out _device,
                out _swapChain);

            //Ignore all windows events
            var factory = _swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(renderControl.Handle, WindowAssociationFlags.IgnoreAll);


        }
        public void Display()
        {

        }


    }
}
