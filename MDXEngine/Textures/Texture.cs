using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
namespace MDXEngine.Textures
{
    public class Texture :  ITexture, IShaderResource
    {
        private SharpDX.Direct3D11.Texture2D _resource;
        private SharpDX.Direct3D11.ShaderResourceView _view;
        private IDxContext _dx;
        public Texture(IDxContext dx,string fileName)
        {
            _resource=Texture2D.FromFile<Texture2D>(dx.Device, fileName, ImageLoadInformation.Default);
            _view = new ShaderResourceView(dx.Device,_resource);
            _dx = dx;
        }

        public void Dispose()
        {
            _resource.Dispose();
        }

        public ShaderResourceView GetResourceView()
        {
            return _view;
        }

        public void Load(HLSLProgram program, int slotId)
        {
            program.LoadTexture(slotId,this);
        }
    }
}
