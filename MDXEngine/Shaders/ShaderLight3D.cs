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
using MDXEngine.SharpDXExtensions;

namespace MDXEngine.Shaders
{
    public class ShaderLight3D : IShader
    {
        private struct TViewChange {
            public Matrix projM;
            public Vector4 eyePos;
        }
        
        
        IDxContext _dx;
        HLSLProgram _program;
        DrawTree<VerticeNormal> _drawTree;
        CBufferResource<TViewChange> _worldProj;
        CBufferResource<DirectionalLight> _lights;
        CBufferResource<Material> _material;
        public IObservable ObservableDock { get; private set; } //An IObservable used by observers to attach themselves

        public ShaderLight3D(IDxContext dxContext)
        {
            _dx = dxContext;
            _program = new HLSLProgram(_dx, HLSLResources.Light3D_hlsl, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("NORMAL", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            _drawTree = new DrawTree<VerticeNormal>();
            _worldProj = new CBufferResource<TViewChange>(_dx);
            _lights = new CBufferResource<DirectionalLight>(_dx);
            _material = new CBufferResource<Material>(_dx); 
           
            _worldProj.Data = new TViewChange
{
             projM=Matrix.Identity,
             eyePos=new Vector4(0)
            };

            _drawTree.GetRootNode().Commands = new CommandsSequence(_program)
                .LoadResource("TViewChange", _worldProj)
                .LoadResource("OneTime", _lights);
             
             this.ObservableDock = new ShaderObservableDock(_drawTree);
        }
       

        public void SetDirectionalLight(DirectionalLight light)
        {
            _lights.Data = light;
        }

        public DrawTree<VerticeNormal> Root { get { return _drawTree; } }

        public void Draw(IDxContext dx)
        {
            dx.CurrentProgram = _program;
            if (dx.IsCameraChanged)
            {
                _worldProj.Data = new TViewChange
                {
                    projM = dx.Camera.GetWorldViewMatrix(),
                    eyePos = dx.Camera.Pos.ToVector4()
                };
            }
            _drawTree.Draw(dx);
        }

        public void Add(ITopology topology, Material mat)
        {
            /*
            var shape = new Shape3D<VerticeNormal>(topology, painter);
             */
           // _drawTree.Add(shape);
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _drawTree);
            Utilities.Dispose(ref _program);
        }
    }
}
