﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
namespace MDXEngine.Textures
{
    public class Texture :  ITexture
    {
        private SharpDX.Direct3D11.Texture2D _resource;
        private SharpDX.Direct3D11.ShaderResourceView _view;

        public Texture(IDxContext dx,string fileName)
        {
            _resource=Texture2D.FromFile<Texture2D>(dx.Device, fileName, ImageLoadInformation.Default);
            _view = new ShaderResourceView(dx.Device,_resource);
        }

        public void Dispose()
        {
            _resource.Dispose();
        }

    }
}
