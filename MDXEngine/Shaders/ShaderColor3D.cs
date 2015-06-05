using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Shapes;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using MDXEngine;

namespace MDXEngine
{
    public class ShaderColor3D : IShader
    {
        IDxContext  _dx;
        HLSLProgram _program;
        DrawTree<VerticeColor> _root;


        public ShaderColor3D(IDxContext dxContext)
        {
            _dx = dxContext;
            _dx.AddShader(this);
            _program = new HLSLProgram(_dx,HLSLResources.Color3D_hlsl,  new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            _root = new DrawTree<VerticeColor>();

        }

        public DrawTree<VerticeColor> Root { get { return _root; } }

        public void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
            _root.Draw(dx);
            
        }

        public void Add(ITopology topology,IPainter<VerticeColor> painter  )
        {
            var shape = new Shape3D<VerticeColor>(topology,painter);
            _root.Add(shape);
        }
        
        public void Dispose()
        {
            Utilities.Dispose(ref _root); 
            Utilities.Dispose(ref _program);
        }
    }
}
