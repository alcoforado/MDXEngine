using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
using System.Drawing;
using System.Drawing.Imaging;
using MDXEngine.Interfaces;
using MDXEngine.ShaderResources.Textures.BinPack;

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
        }
        



        public IObservable ObservableDock { get; set; }



        public Texture(IDxContext dx)
        {
          
            _dx = dx;
        }


        public void LoadFromFile(string fileName)
        {
            this.Dispose();
            _resource = Texture2D.FromFile<Texture2D>(_dx.Device, fileName, ImageLoadInformation.Default);
            _view = new ShaderResourceView(_dx.Device, _resource);
            this.ObservableDock = new ObservableDock();

        }

       


         public void LoadFromBitmap(IBitmap bp)
        {
            this.Dispose();
             this.ObservableDock = new ObservableDock();
            _resource= new Texture2D(_dx.Device,new Texture2DDescription()
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
            _view = new ShaderResourceView(_dx.Device, _resource);
           
            this.CopyFromBitmap(bp);
           
        }



        public int Width  { get {return _resource.Description.Width;}}
        public int Height { get { return _resource.Description.Height; } }
     


        public void Dispose()
        {
            if (_resource != null)
            {
                _resource.Dispose();
                _resource = null;
            }
            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }
            
        }

        public void CopyFromBitmap(IBitmap bp)
        {

            var bitmap = ((GDIBitmap) bp)._bitmap;

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


      
    
        public void Load(object data)
        {
            Bitmap bp = data as Bitmap;
            if (bp != null )
            {
                LoadFromBitmap(bp);
                return;
            }
            string fileName = data as string;
            if (fileName != null)
            {
                LoadFromFile(fileName);
            }
            throw new Exception(String.Format("Cannot build a texture from type {0}",data.GetType().FullName));
        }

        /// <summary>
        /// Effective load the resource into the program shader using IDxContext
        /// Remember that you can access IDxContext throuth the HLSLProgram.DxContext property
        /// </summary>
        /// <param name="shaderVariableName"></param>
        public void Bind(IShaderProgram program, string shaderVariableName)
        {
            
            var slotRef = program.ProgramResourceSlots[shaderVariableName];
            
            
            if (slotRef.Exists)
            {
                var slot = slotRef.Value;
                if (slot.IsTexture())
                {
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
