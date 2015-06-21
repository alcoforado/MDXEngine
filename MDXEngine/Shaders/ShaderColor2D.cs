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
namespace MDXEngine
{
    public class ShaderColor2D : ShaderBase<VerticeColor> 
    {
        IDxContext  _dx;
        HLSLProgram _program;
       


        public ShaderColor2D(IDxContext dxContext)
        {
            _dx = dxContext;
            _dx.AddShader(this);
            _program = new HLSLProgram(_dx,HLSLResources.Color2D_hlsl,  new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
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

        public void Add(ITopology2D topology,IPainter<VerticeColor> painter  )
        {
            var shape = new Shape2D<VerticeColor>(topology,painter);
            Root.Add(shape);
        }
        
        
    }
}
