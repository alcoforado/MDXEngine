using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.D3DCompiler;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using MDXEngine.Shapes;
namespace MDXEngine.Shaders
{
    public class ShaderColor2D : IShader
    {
        IDxContext  _dx;
        HLSLProgram _program;
        DrawTree<Color2D> _root;


        public ShaderColor2D(IDxContext dxContext)
        {
            _dx = dxContext;
            _program = new HLSLProgram(_dx.Device,HLSLResources.Color2D_hlsl,  new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });


            _root = new DrawTree<Color2D>();

        }

        public DrawTree<Color2D> Root { get { return _root; } }

        public void Draw(IDxContext dx)
        {
            _program.SetAsCurrent(dx);
            _root.Draw(dx);
            
        }

        public void Add(ITopology2D topology,IPainter<Color2D> painter  )
        {
            var shape = new Shape2D<Color2D>(topology,painter);
            _root.Add(shape);
        }
        
        public void Dispose()
        {
            Utilities.Dispose(ref _root); 
            Utilities.Dispose(ref _program);
        }
    }
}
