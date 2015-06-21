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

namespace MDXEngine
{
    public class ShaderTexture2D : ShaderBase<VerticeTexture2D>
    {
        IDxContext _dx;
        HLSLProgram _program;
        

        public ShaderTexture2D(IDxContext dxContext)
        {
            _dx = dxContext;
            _dx.AddShader(this);
            _program = new HLSLProgram(_dx,HLSLResources.Texture2D_hlsl,  new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                    });


            

        }

        public override void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
            Root.Draw(dx);
        }

        public override void Dispose()
        {
            base.Dispose();
            Utilities.Dispose(ref _program);
        }

        public void Add(IShape<VerticeTexture2D> shape, Texture texture)
        {
            var command = new CommandsSequence(_program);
            command.AddLoadCommand("gTexture", texture);
            Root.Add(shape,command);
        }


    }
}
