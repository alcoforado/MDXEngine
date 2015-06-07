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
    public class ShaderColor3D : IShader, ICameraObserver
    {
        IDxContext _dx;
        HLSLProgram _program;
        DrawTree<VerticeColor> _drawTree;
        CBufferResource<Matrix> _worldProj;
        bool _bCameraChanged;
        public ShaderColor3D(IDxContext dxContext)
        {
            _dx = dxContext;
            _dx.AddShader(this);
            _program = new HLSLProgram(_dx, HLSLResources.Color3D_hlsl, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            _drawTree = new DrawTree<VerticeColor>();
            _worldProj = new CBufferResource<Matrix>(_program);
            Matrix M = Matrix.Identity;
            _worldProj.Data = M;


           
            

            _drawTree.GetRootNode().Commands = new CommandsSequence(_program)
                .AddLoadCommand("TViewChange", _worldProj);
             

        }
        #region CameraObserver Interface
        public void CameraChanged(Camera cam)
        {
            _bCameraChanged = true;
        }
        
        #endregion


        public DrawTree<VerticeColor> Root { get { return _drawTree; } }

        public void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
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
