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
    public class ShaderTexture2D : IShader
    {
        IDxContext _dx;
        HLSLProgram _program;
        DrawTree<VerticeTexture2D> _root;

        public ShaderTexture2D(IDxContext dxContext)
        {
            _dx = dxContext;
            _dx.AddShader(this);
            _program = new HLSLProgram(_dx,HLSLResources.Texture2D_hlsl,  new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                    });


            _root = new DrawTree<VerticeTexture2D>();

        }

        public void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
            /*
           var sampler = new SamplerState(dx.Device, new SamplerStateDescription()
            {
                Filter = Filter.MinMagMipPoint,
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                BorderColor = Color.Black,
                ComparisonFunction = Comparison.Never,
                MaximumAnisotropy = 16,
                MipLodBias = 0,
                MinimumLod = 0,
                MaximumLod = 16,
            });
            _dx.DeviceContext.PixelShader.SetSampler(0,sampler);
            */
              _root.Draw(dx);

        }

        public void Dispose()
        {
            Utilities.Dispose(ref _root);
            Utilities.Dispose(ref _program);
        }

        public void Add(IShape<VerticeTexture2D> shape, Texture texture)
        {
            var command = new CommandsSequence(_program);
            command.AddLoadCommand("gTexture", texture);
            _root.Add(shape,command);
        }


    }
}
