using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using Buffer = SharpDX.Direct3D11.Buffer;
namespace MDXEngine
{
    public class DrawTree<T> : IDisposable where T : struct
    {
        internal NTreeNode<DrawInfo<T>> _root;
        internal T[] _vertices;
        internal int[] _indices;
        Buffer _vI;
        Buffer _vV;



        internal NTreeNodeIterator<DrawInfo<T>> BeginIterator()
        {
            return new NTreeNodeIterator<DrawInfo<T>>(_root);

        }

        internal void ComputeSizes()
        {
            int OffI = 0;
            int OffV = 0;
            _root.ForAllInOrder(
                node =>
                {
                    var info = node.GetData();
                    if (!info.Changed)
                        return;
                    switch (info.Type)
                    {
                        case DrawInfoType.SHAPE:
                            {
                                info.SizeI = info.Shape.NIndices();
                                info.SizeV = info.Shape.NVertices();
                                info.OffI = OffI;
                                info.OffV = OffV;
                                OffI += info.SizeI;
                                OffV += info.SizeV;
                                break;
                            }
                        case DrawInfoType.SHAPE_GROUP:
                            {
                                info.SizeI = 0;
                                info.SizeV = 0;
                                foreach (var child in node.GetChilds())
                                {
                                    info.SizeI += child.GetData().SizeI;
                                    info.SizeV += child.GetData().SizeV;
                                }
                                if (node.GetChilds().Count != 0)
                                {
                                    info.OffV = node.GetChilds().First().GetData().OffV;
                                    info.OffI = node.GetChilds().First().GetData().OffI;
                                }

                                break;
                            }
                        case DrawInfoType.COMMAND_SEQUENCE:
                            {
                                info.SizeI = info.SizeV = 0;
                                foreach (var child in node.GetChilds())
                                {
                                    if (child.GetData().Type == DrawInfoType.SHAPE_GROUP)
                                    {
                                        info.SizeI += child.GetData().SizeI;
                                        info.SizeV += child.GetData().SizeV;
                                    }
                                    else
                                        throw new Exception("Root should have only ShapeGroups as childs");
                                }
                                info.OffI = 0;
                                info.OffV = 0;
                                break;
                            }
                    }
                });



        }

        public void Dispose()
        {
            Utilities.Dispose(ref _vI);
            Utilities.Dispose(ref _vV);
        }

        internal void CopyToDxBuffers(IDxContext context)
        {
            Utilities.Dispose(ref _vI);
            Utilities.Dispose(ref _vV);
            if (_indices.Length != 0)
            {

                _vI = Buffer.Create<int>(context.Device, BindFlags.IndexBuffer, _indices);
                context.DeviceContext.InputAssembler.SetIndexBuffer(_vI, SharpDX.DXGI.Format.R32_UInt, 0);
            }
            if (_vertices.Length != 0)
            {
                _vV = Buffer.Create<T>(context.Device, BindFlags.VertexBuffer, _vertices);
                context.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_vV, Utilities.SizeOf<T>(), 0));
            }
        }



        public DrawTree(int nVertices = 0, int nIndices = 0)
        {
            _root = new NTreeNode<DrawInfo<T>>(DrawInfo<T>.CreateCommandSequence());
           
        }

        public void Add(IShape<T> shape)
        {
            var drawShape = DrawInfo<T>.CreateShape(shape);
            foreach (var node in _root.GetChilds())
            {
                var drawInfo = node.GetData();
                if (node.GetData().CanHaveShapeAsChild(shape))
                {
                    //Add the shape in the tree
                    var newNode = new NTreeNode<DrawInfo<T>>(DrawInfo<T>.CreateShape(shape));
                    node.AppendChild(newNode);
                    newNode.ForAllParents(nd => nd.GetData().Changed = true);
                    return;
                }
            }
            //If we reach here, there is no group to add this shape.
            //Create one
            var shapeNode = new NTreeNode<DrawInfo<T>>(DrawInfo<T>.CreateShape(shape));
            var groupNode = new NTreeNode<DrawInfo<T>>(DrawInfo<T>.CreateGroupForShape(shape));
            groupNode.AppendChild(shapeNode);
            _root.AppendChild(groupNode);
            shapeNode.ForAllParents(nd => nd.GetData().Changed = true);
        }


        public void FullSyncTree()
        {

            this.ComputeSizes();

           
            
            _vertices = new T[_root.GetData().SizeV];
            _indices = new int[_root.GetData().SizeI];

            _root.ForAllInOrder(
                node =>
                {
                    DrawInfo<T> info = node.GetData();
                    switch (info.Type)
                    {
                        case DrawInfoType.SHAPE:
                            var vV = new SubArray<T>(_vertices, info.OffV, info.SizeV);
                            var vI = new SubArray<int>(_indices, info.OffI, info.SizeI);
                            info.Shape.Write(vV, vI);

                            //Adjust Indices;
                            for (int i = info.OffI; i < info.SizeI; i++)
                            {
                                _indices[i] += info.OffI;
                            }
                            break;
                    }
                    info.Changed = false;
                });


        }

        public void Draw(IDxContext dx)
        {
            if (_root.GetData().Changed)
            {
                FullSyncTree();
                CopyToDxBuffers(dx);
            }

            foreach (var child in _root.GetChilds())
            {
                var node = child.GetData();
                if (child.GetData().Type == DrawInfoType.SHAPE_GROUP)
                {
                    switch (node.GetTopology())
                    {
                        case TopologyType.TRIANGLES:
                            {
                                dx.DeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                                if (node.IsIndexed())
                                {
                                    dx.DeviceContext.DrawIndexed(node.SizeI, node.OffI, 0);
                                }
                                else
                                {
                                    dx.DeviceContext.Draw(node.SizeV, node.OffV);

                                }
                                break;
                            }
                        default:
                            throw new Exception("Draw Tree, Topology not supported yet");
                    }

                }
            }


        }

    }
}
