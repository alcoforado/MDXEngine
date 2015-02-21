using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace MDXEngine
{
    public class DxControl : IDxContext
    {
       private Device _device;
       private readonly Control _renderControl;
       private SwapChain _swapChain;
       private Texture2D _backBuffer;
       private RenderTargetView _renderView;
       private Texture2D _depthBuffer;
       private DepthStencilView _depthView;
       private SwapChainDescription _desc;
       private RasterizerState _rasterizerState;
       private readonly List<IShader> _shaders;
       private HLSLProgram _hlslProgram;
      
        private void InitializeDX()
        {
            _desc = new SwapChainDescription
            {
                BufferCount = 1,
                ModeDescription =
                    new ModeDescription(_renderControl.ClientSize.Width, _renderControl.ClientSize.Height,
                                        new Rational(60, 1), Format.B8G8R8A8_UNorm),
                IsWindowed = true,
                OutputHandle = _renderControl.Handle,
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
            factory.MakeWindowAssociation(_renderControl.Handle, WindowAssociationFlags.IgnoreAll);

            //Set right hand convention
            _rasterizerState = new RasterizerState(_device, new RasterizerStateDescription
            {
                CullMode=CullMode.Back,
                FillMode=FillMode.Solid,
                IsAntialiasedLineEnabled=true,
                IsFrontCounterClockwise=true,
                IsMultisampleEnabled=false,
                IsScissorEnabled=false
                
            });
            _device.ImmediateContext.Rasterizer.State = _rasterizerState;
            
        }
        
        public DeviceContext DeviceContext { get { return _device.ImmediateContext; } }
        
        public Device Device {   get {return _device; } }




        public DxControl(Control control)
        {
            _renderControl=control;
            InitializeDX();
            _shaders = new List<IShader>();
        }

        public void Reset()
        {
            foreach (var shd in _shaders)
                shd.Dispose();
            _device.ImmediateContext.Rasterizer.State = _rasterizerState;
        }


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
        
        public void AddShader(IShader shader)
        {
            if (_shaders.FirstOrDefault(x => x==shader) == null)
            {
                _shaders.Add(shader);
            }
        }


        public void Resize()
        {
            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _renderView);
            Utilities.Dispose(ref _depthBuffer);
            Utilities.Dispose(ref _depthView);

            // Resize the backbuffer
            _swapChain.ResizeBuffers(_desc.BufferCount, _renderControl.ClientSize.Width, _renderControl.ClientSize.Height, Format.Unknown, SwapChainFlags.None);

            // Get the backbuffer from the swapchain
            _backBuffer = SharpDX.Direct3D11.Resource.FromSwapChain<Texture2D>(_swapChain, 0);

            // Renderview on the backbuffer
            _renderView = new RenderTargetView(_device, _backBuffer);

            // Create the depth buffer
            _depthBuffer = new Texture2D(_device, new Texture2DDescription
            {
                Format = Format.D32_Float_S8X24_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = _renderControl.ClientSize.Width,
                Height = _renderControl.ClientSize.Height,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            });

            // Create the depth buffer view
            _depthView = new DepthStencilView(_device, _depthBuffer);

            // Setup targets and viewport for rendering
            _device.ImmediateContext.Rasterizer.SetViewport(new Viewport(0, 0, _renderControl.ClientSize.Width, _renderControl.ClientSize.Height, 0.0f, 1.0f));
            _device.ImmediateContext.OutputMerger.SetTargets(_depthView, _renderView);
        }
        

        public void Clear()
        {
            // Clear views
            _device.ImmediateContext.ClearDepthStencilView(_depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
            _device.ImmediateContext.ClearRenderTargetView(_renderView, Color.Black);
      
        }

        public void Display()
        {
            Clear();
            foreach (var shd in _shaders)
            {
                shd.Draw(this);
            }
            _swapChain.Present(0, PresentFlags.None);
        }
        
        public void Dispose()
        {
            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _renderView);
            Utilities.Dispose(ref _depthBuffer);
            Utilities.Dispose(ref _depthView);
            Utilities.Dispose(ref _swapChain);
            Utilities.Dispose(ref _rasterizerState);
            Utilities.Dispose(ref _device);
           
            foreach (var shd in _shaders)
                shd.Dispose();
        }
    }
}
