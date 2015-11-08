using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
using System.Drawing;
using System.Drawing.Imaging;
namespace MDXEngine.Textures
{
    /// <summary>
    /// This class represents a 2D texture loaded on GPU.
    /// Internally it contains a resource binded with its view.
    /// </summary>
    public class Texture : IShaderResource
    {
        protected SharpDX.Direct3D11.Texture2D _resource;
        protected SharpDX.Direct3D11.ShaderResourceView _view;
        protected IDxContext _dx;
        
        protected Texture() 
        {
            this.ObservableDock = new ObservableDock();
        }
        protected void InitTexture(IDxContext dx,Texture2DDescription desc) 
        {
            _resource = new Texture2D(dx.Device, desc);
            _view = new ShaderResourceView(dx.Device, _resource);
            _dx = dx;
            _dx.ResourcesManager.Add(this);
        }

        public IObservable ObservableDock { get; set; }


        public Texture(IDxContext dx, string fileName)
        {
            _resource = Texture2D.FromFile<Texture2D>(dx.Device, fileName, ImageLoadInformation.Default);
            _view = new ShaderResourceView(dx.Device, _resource);
            _dx = dx;
            _dx.ResourcesManager.Add(this);
            this.ObservableDock = new ObservableDock();

        }

        public Texture(IDxContext dx,Bitmap bp)
        {
            this.ObservableDock = new ObservableDock();
            if (bp.PixelFormat != PixelFormat.Format32bppArgb)
                throw new Exception("Only Bitmap with PixelFormat Format32bppArgb is compatible for now");
            _resource= new Texture2D(dx.Device,new Texture2DDescription()
                        {
                            BindFlags = BindFlags.ShaderResource,
                            Width = bp.Width,
                            Height = bp.Height,
                            Usage = ResourceUsage.Default,
                            Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm_SRgb,
                            ArraySize = 1,
                            OptionFlags = ResourceOptionFlags.None,
                            MipLevels = 1,
                            CpuAccessFlags = CpuAccessFlags.None,
                            SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0)
                        });
            _view = new ShaderResourceView(dx.Device, _resource);
            _dx = dx;
            _dx.ResourcesManager.Add(this);
            this.CopyFromBitmap(bp);
           
        }



        public int Width  { get {return _resource.Description.Width;}}
        public int Height { get { return _resource.Description.Height; } }
     


        public void Dispose()
        {
            _resource.Dispose();
            _view.Dispose();
            
        }

        public void CopyFromBitmap(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
            {

                var height = Math.Min(_resource.Description.Height, bitmap.Height);
                var width = Math.Min(_resource.Description.Width, bitmap.Width);
                int pixelSize = 4;
                var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

                Nullable<ResourceRegion> region = new ResourceRegion
                {
                    Left = 0,
                    Top = 0,
                    Right = width,
                    Bottom = height,
                    Front = 0,
                    Back = 1
                };


                _dx.DeviceContext.UpdateSubresource(
                    (Resource)_resource,
                    0,
                    region,
                    bitmapData.Scan0,
                    bitmapData.Stride,
                    height * width * pixelSize);

                bitmap.UnlockBits(bitmapData);
                ((ObservableDock) this.ObservableDock).OnChanged();

            }
            else
                throw new Exception("Only Bitmap with PixelFormat Format32bppArgb is compatible for now");

        }

       
        public ShaderResourceView GetResourceView()
        {
            return _view;
        }

        public bool IsDisposed()
        {
            return _resource.IsDisposed && _view.IsDisposed;
        }

        /// <summary>
        /// Effective load the resource into the program shader using IDxContext
        /// Remember that you can access IDxContext throuth the HLSLProgram.DxContext property
        /// </summary>
        /// <param name="program"></param>
        /// <param name="shaderVariableName"></param>
        public void Load(HLSLProgram program, String shaderVariableName)
        {
            if (!program.IsCurrent())
                throw new Exception("HLSLProgram is not the current loaded program");
            var slotRef = program.ProgramResourceSlots[shaderVariableName];
            
            
            if (slotRef.Exists)
            {
                var slot = slotRef.Value;
                if (slot.IsTexture())
                {
                    slot.Resource = this;
                    _dx.DeviceContext.PixelShader.SetShaderResource(slot.SlotId, this._view);
                }
                else
                {
                    throw new Exception(String.Format("Slot {0} is not a texture", shaderVariableName));
                }
            }
            else
                throw new Exception(String.Format("Texture Slot {0} does not exist", shaderVariableName));
        }

      
       
    }
}
