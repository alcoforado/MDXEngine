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
        IDxContext _dx;
        HLSLProgram _program;
        DrawTree<VerticeColor> _drawTree;
        CBufferResource<Matrix> _worldProj;
        
        public IObservable ObservableDock { get; private set; } //An IObservable used by observers to attach themselves

        public ShaderColor3D(IDxContext dxContext)
        {
            _dx = dxContext;
            _program = new HLSLProgram(_dx, HLSLResources.Color3D_hlsl, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            _drawTree = new DrawTree<VerticeColor>();
            _worldProj = new CBufferResource<Matrix>(_dx);
            Matrix M = Matrix.Identity;
            _worldProj.Data = M;

            _drawTree.SetRootCommandsSequence(new CommandsSequence(_program)
                .LoadResource("TViewChange", _worldProj));
             
             this.ObservableDock = new ShaderObservableDock(_drawTree);
        }
       

        public DrawTree<VerticeColor> Root { get { return _drawTree; } }

        public void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
            if (dx.IsCameraChanged)
            {
                _worldProj.Data = dx.Camera.GetWorldViewMatrix();
            }
            _drawTree.Draw(dx);
        }

        public void Add(ITopology topology, IPainter<VerticeColor> painter)
        {
            var shape = new Shape3D<VerticeColor>(topology, painter);
            _drawTree.Add(shape);
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _drawTree);
            Utilities.Dispose(ref _program);
        }
    
    }
}
