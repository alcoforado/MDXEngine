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
    public class ShaderColor3D : ShaderBase<VerticeColor> 
    {
        IDxContext _dx;
        HLSLProgram _program;
     
        CBufferResource<Matrix> _worldProj;
        public ShaderColor3D(IDxContext dxContext)
        {
            _dx = dxContext;
            _dx.AddShader(this);
            _program = new HLSLProgram(_dx, HLSLResources.Color3D_hlsl, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            
            _worldProj = new CBufferResource<Matrix>(_program);
            Matrix M = Matrix.Identity;
            _worldProj.Data = M;


           
            

            Root.GetRootNode().Commands = new CommandsSequence(_program)
                .AddLoadCommand("TViewChange", _worldProj);
             

        }
       

        

        public override void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
            if (dx.IsCameraChanged)
            {
                _worldProj.Data = dx.Camera.GetWorldViewMatrix();
            }
            Root.Draw(dx);

        }

        public void Add(ITopology topology, IPainter<VerticeColor> painter)
        {
            var shape = new Shape3D<VerticeColor>(topology, painter);
            Root.Add(shape);
        }

        public override void Dispose()
        {
            base.Dispose();
            Utilities.Dispose(ref _program);
        }
    }
}
