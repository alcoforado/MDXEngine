using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;
using SharpDX.D3DCompiler;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;

namespace MDXEngine.Shaders
{
    public class ShaderTexture2D : IShader
    {
        IDxContext _dx;
        HLSLProgram _program;
        DrawTree<Color2D> _root;

        public ShaderTexture2D(IDxContext dxContext)
        {
            _dx = dxContext;
            _dx.AddShader(this);
            _program = new HLSLProgram(_dx.Device,HLSLResources.Texture2D_hlsl,  new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                    });


            _root = new DrawTree<Color2D>();

        }

        public void Draw(IDxContext dx)
        {
            _program.SetAsCurrent(dx);
            _root.Draw(dx);

        }

        public void Dispose()
        {
            Utilities.Dispose(ref _root);
            Utilities.Dispose(ref _program);
        }


        public void Add(IShape<VerticeTexture2D> shape, Texture texture)
        {
           
        }

    }
}
