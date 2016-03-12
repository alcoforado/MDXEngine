using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using Microsoft.Practices.Unity;
using System;

namespace MDXEngine
{
    
    public class DxControl :  ICameraObserver, IDxViewControl
    {
       private Device _device;
       private readonly Control _renderControl;
       private readonly ResourcesManager _resourceManager;
       private SwapChain _swapChain;
       private Texture2D _backBuffer;
       private RenderTargetView _renderView;
       private Texture2D _depthBuffer;
       private DepthStencilView _depthView;
       private SwapChainDescription _desc;
       private RasterizerState _rasterizerState;
       private readonly List<IShader> _shaders;
       private HLSLProgram _hlslProgram;
       private bool _needRedraw;
       private bool _needResize;
       private readonly DxContext _dx;
       private IUnityContainer _container;


       private void InitializeContainer()
       {
           _container = new UnityContainer();
           foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
           {
               foreach (Type t in a.GetTypes())

                   if (typeof(IShader).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                   {
                       _container.RegisterType(t);
                   }
           }

           _container.RegisterInstance<IDxContext>(_dx);
       }

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
               DeviceCreationFlags.BgraSupport,
               _desc,
               out _device,
               out _swapChain);

           //Ignore all windows events
           var factory = _swapChain.GetParent<Factory>();
           factory.MakeWindowAssociation(_renderControl.Handle, WindowAssociationFlags.IgnoreAll);

           //Set right hand convention
           _rasterizerState = new RasterizerState(_device, new RasterizerStateDescription
           {
               CullMode = CullMode.None,
               FillMode = FillMode.Solid,
               IsAntialiasedLineEnabled = true,
               IsFrontCounterClockwise = true,
               IsMultisampleEnabled = false,
               IsScissorEnabled = false

           });
           _device.ImmediateContext.Rasterizer.State = _rasterizerState;




           _needResize = true;
           _needRedraw = true;

       }

        private class ShaderWatcher : Observer 
        {
            private DxControl _dc;
            public ShaderWatcher(DxControl dc){_dc = dc;}
            public override void Changed() { _dc.ScheduleForRedraw(); }
        }

       public IDxContext GetDxContext() {  return _dx; }
        
        public T ResolveShader<T>() where T: IShader
       {
            foreach(var shader in _shaders)
            {
                if (shader.GetType() == typeof(T))
                {
                    return (T) shader;
                }
            }
           return this.CreateShader<T>();
       }

        public T CreateShader<T>() where T : IShader
        {
            var shader = _container.Resolve<T>();
            this.AddShader(shader);
            return shader;
        }

       public void ScheduleForRedraw()
       {
           _needRedraw = true;
       }

      
        
        public DeviceContext DeviceContext { get { return _device.ImmediateContext; } }
        
        public Device Device {   get {return _device; } }


        public Control Control { get { return _renderControl; } }

        public DxControl(Control control)
        {
            
            _renderControl=control;
            InitializeDX();
            _resourceManager = new ResourcesManager();
            _shaders = new List<IShader>();
            
            control.Resize += (events, args) => this._needResize = true;
            control.Paint += (events, args) =>  this._needRedraw = true;

            _dx = new DxContext();
            _dx.Camera = new Camera(control.ClientSize.Width,control.ClientSize.Height);
            _dx.Camera.AddObserver(this);
            _dx.Device = _device;
            _dx.ResourcesManager = _resourceManager;
            _dx.IsCameraChanged = true;
            _dx.ScreenSize = _renderControl.ClientSize;

            InitializeContainer();
        }

        public void  CameraChanged(Camera Camera)
        {
            _dx.IsCameraChanged = true;
        }

        public void Reset()
        {
            foreach (var shd in _shaders)
                shd.Dispose();
            _shaders.Clear();
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
            //Only add if the shader does not exist
            if (_shaders.FirstOrDefault(x => x==shader) == null)
            {
                _shaders.Add(shader);
                shader.ObservableDock.AttachObserver(new ShaderWatcher(this));
                _needRedraw = true;
                _dx.IsCameraChanged = true;
            }
        }


        public void Resize()
        {

            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _renderView);
            Utilities.Dispose(ref _depthBuffer);
            Utilities.Dispose(ref _depthView);


            _dx.Camera.SetLens(_renderControl.ClientSize.Width, _renderControl.ClientSize.Height);

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
                shd.Draw(this.GetDxContext());
            }
            _swapChain.Present(0, PresentFlags.None);
        }

        public bool LazyDisplay()
        {
            if (_needResize)
            {
                this.Resize();
                this.Display();
                _needRedraw = false;
                _needResize = false;
                _dx.IsCameraChanged = false;
                return true; //process needed
            }
            if (_needRedraw || _dx.IsCameraChanged)
            {
                this.Display();
                _dx.IsCameraChanged = false;
                _needRedraw = false;
                return true; //process needed
            }
            return false; //No process needed
        }


        public ResourcesManager ResourcesManager { get { return _resourceManager; } }

        public void Dispose()
        {
            foreach (var shd in _shaders)
                shd.Dispose();

            _resourceManager.DisposeAllResources();
           

            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _renderView);
            Utilities.Dispose(ref _depthBuffer);
            Utilities.Dispose(ref _depthView);
            Utilities.Dispose(ref _swapChain);
            Utilities.Dispose(ref _rasterizerState);
            Utilities.Dispose(ref _device);
           


        }
    }
}
