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
    public class Texture : IShaderResource
    {
        protected SharpDX.Direct3D11.Texture2D _resource;
        protected SharpDX.Direct3D11.ShaderResourceView _view;
        protected IDxContext _dx;

        protected Texture() { }

        public Texture(IDxContext dx, string fileName)
        {
            _resource = Texture2D.FromFile<Texture2D>(dx.Device, fileName, ImageLoadInformation.Default);
            _view = new ShaderResourceView(dx.Device, _resource);
            _dx = dx;
            _dx.ResourcesManager.Add(this);
        }

        public Texture(IDxContext dx,Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new Exception("Only bitmap with SRGB pixel format is suported for now");
            _resource = new Texture2D(dx.Device, new Texture2DDescription()
            {
                BindFlags = BindFlags.ShaderResource,
                Width = bitmap.Width,
                Height = bitmap.Height,
                Usage=ResourceUsage.Default,
                Format=SharpDX.DXGI.Format.B8G8R8A8_UNorm_SRgb,
                ArraySize=1,
                OptionFlags=ResourceOptionFlags.None,
                MipLevels=1,
                CpuAccessFlags=CpuAccessFlags.None,
                SampleDescription=new SharpDX.DXGI.SampleDescription(1,0)
            });
            _dx = dx;
            _dx.ResourcesManager.Add(this);
           
            _view = new ShaderResourceView(dx.Device, _resource);
            CopyFromBitmap(bitmap);
        }


        public void Dispose()
        {
            _resource.Dispose();
            _view.Dispose();

        }

        
        public void CopyFromBitmap(Bitmap bitmap)
        {
            if (_resource.Description.Format == SharpDX.DXGI.Format.B8G8R8A8_UNorm_SRgb && bitmap.PixelFormat == PixelFormat.Format32bppArgb)
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
                    (Resource) _resource,
                    0,
                    region,
                    bitmapData.Scan0,
                    bitmapData.Stride,
                    height * width * pixelSize);

                bitmap.UnlockBits(bitmapData);
            }
            else
                throw new Exception("Texture and bitmap are not compatible");

        }
        
        public ShaderResourceView GetResourceView()
        {
            return _view;
        }

        public bool IsDisposed()
        {
            return _resource.IsDisposed && _view.IsDisposed;
        }

        public void Load(HLSLProgram program, int slotId)
        {
            program.LoadTexture(slotId, this);
        }
    }
}
