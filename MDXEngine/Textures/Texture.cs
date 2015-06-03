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
        protected void InitTexture(IDxContext dx,Texture2DDescription desc) 
        {
            _resource = new Texture2D(dx.Device, desc);
            _view = new ShaderResourceView(dx.Device, _resource);
            _dx = dx;
            _dx.ResourcesManager.Add(this);

        }

        public Texture(IDxContext dx, string fileName)
        {
            _resource = Texture2D.FromFile<Texture2D>(dx.Device, fileName, ImageLoadInformation.Default);
            _view = new ShaderResourceView(dx.Device, _resource);
            _dx = dx;
            _dx.ResourcesManager.Add(this);
        }

     


        public void Dispose()
        {
            _resource.Dispose();
            _view.Dispose();

        }

       
        public ShaderResourceView GetResourceView()
        {
            return _view;
        }

        public bool IsDisposed()
        {
            return _resource.IsDisposed && _view.IsDisposed;
        }

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
