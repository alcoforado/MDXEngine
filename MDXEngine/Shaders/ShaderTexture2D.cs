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
using MDXEngine.DrawTree;

namespace MDXEngine
{
    public class ShaderTexture2D : IShader
    {
        IDxContext _dx;
        HLSLProgram _program;
        DrawTree<VerticeTexture2D> _root;
        public IObservable ObservableDock { get; private set; } //An IObservable used by observers to attach themselves
        public ShaderTexture2D(IDxContext dxContext)
        {
            _dx = dxContext;
            _program = new HLSLProgram(_dx,HLSLResources.Texture2D_hlsl,  new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                    });


            _root = new DrawTree<VerticeTexture2D>(_program); 
            ObservableDock = new ShaderObservableDock(_root);
        }

        public void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
            _root.Draw(dx);
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _root);
            Utilities.Dispose(ref _program);
        }

        public void Add(IShape<VerticeTexture2D> shape, Texture texture)
        {
            _root.Add(shape,new List<ResourceLoadCommand> { new ResourceLoadCommand("gTexture", texture)});
        }

        public void RemoveAll()
        {
            _root.RemoveAll();
        }

    }
}
