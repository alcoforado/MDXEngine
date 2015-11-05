using System;
using System.Linq;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace MDXEngine
{
    public class DrawTree<T> : Observable, IDisposable where T : struct
    {
        private NTreeNode<DrawInfo<T>> _ntree;
        private T[] _vertices;
        private int[] _indices;
        Buffer _vI;
        Buffer _vV;



        public T[] Vertices { get { return _vertices; } }
        public int[] Indices { get { return _indices; } }

        /// <summary>
        /// Get pointer to the root node
        /// </summary>
        /// <returns></returns>
        internal NTreeNodeIterator<DrawInfo<T>> BeginIterator()
        {
            return new NTreeNodeIterator<DrawInfo<T>>(_ntree);

        }

        internal void ComputeSizes()
        {
            int offI = 0;
            int offV = 0;
            _ntree.ForAllInOrder(
                node =>
                {
                    var info = node.GetData();
                    if (!info.Changed)
                    {
                        return;
                    }
                    if (info.IsShapeNode())
                    {
                        var shapeNode = info.ShapeNode;
                        shapeNode.SizeI = shapeNode.Shape.NIndices();
                        shapeNode.SizeV = shapeNode.Shape.NVertices();
                        shapeNode.OffI = offI;
                        shapeNode.OffV = offV;
                        offI += shapeNode.SizeI;
                        offV += shapeNode.SizeV;
                    }
                    if (info.IsShapeGroupNode())
                    {
                        var shapeGroupNode = info.ShapeGroupNode;
                        shapeGroupNode.SizeI = 0;
                        shapeGroupNode.SizeV = 0;
                        foreach (var child in node.GetChilds())
                        {
                            shapeGroupNode.SizeI += child.GetData().ShapeNode.SizeI;
                            shapeGroupNode.SizeV += child.GetData().ShapeNode.SizeV;
                        }
                        if (node.GetChilds().Count != 0)
                        {
                            shapeGroupNode.OffV = node.GetChilds().First().GetData().ShapeNode.OffV;
                            shapeGroupNode.OffI = node.GetChilds().First().GetData().ShapeNode.OffI;
                        }
                    }
                    if (info.IsRootNode())
                    {
                        var rootNode = info.RootNode;
                        rootNode.SizeI = rootNode.SizeV = 0;
                        foreach (var child in node.GetChilds())
                        {
                            if (child.GetData().IsShapeGroupNode())
                            {
                                rootNode.SizeI += child.GetData().ShapeGroupNode.SizeI;
                                rootNode.SizeV += child.GetData().ShapeGroupNode.SizeV;
                            }
                            else
                                throw new Exception("Root should have only ShapeGroups as childs");
                        }
                        rootNode.OffI = 0;
                        rootNode.OffV = 0;

                    }
                }
                );



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

                _vI = Buffer.Create(context.Device, BindFlags.IndexBuffer, _indices);
                context.DeviceContext.InputAssembler.SetIndexBuffer(_vI, Format.R32_UInt, 0);
            }
            if (_vertices.Length != 0)
            {
                _vV = Buffer.Create(context.Device, BindFlags.VertexBuffer, _vertices);
                context.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_vV, Utilities.SizeOf<T>(), 0));
            }
        }



        public DrawTree(int nVertices = 0, int nIndices = 0)
        {

            _ntree = new NTreeNode<DrawInfo<T>>(new DrawInfo<T>(new RootNode()));
            _vertices = new T[nVertices];
            _indices = new int[nIndices];
        }


        public RootNode GetRootNode()
        {
            return _ntree.GetData().RootNode;
        }


        public void Add(IShape<T> shape, CommandsSequence commands = null)
        {

            foreach (var node in _ntree.GetChilds())
            {

                var shapeGroup = node.GetData().ShapeGroupNode;
                if (shapeGroup.CanHaveShapeAsChild(shape))
                {
                    //Check if the command Sequence is compatible
                    if (commands == null || shapeGroup.Commands.CanMerge(commands))
                    {
                        if (commands != null)
                            shapeGroup.Commands.TryMerge(commands);

                        //Add the shape in the shape group tree
                        var newNode = new NTreeNode<DrawInfo<T>>(new DrawInfo<T>(new ShapeNode<T>(shape)));
                        node.AppendChild(newNode);
                        newNode.ForAllParents(nd => nd.GetData().Changed = true);
                        return;
                    }

                }
            }
            //If we reach here, there is no group to add this shape.
            //Create one]
            var shapeNode = new NTreeNode<DrawInfo<T>>( new DrawInfo<T>( new ShapeNode<T>(shape)));
            var groupNode = new NTreeNode<DrawInfo<T>>( new DrawInfo<T>( new ShapeGroupNode<T>(shape, commands)));
            groupNode.AppendChild(shapeNode);
            _ntree.AppendChild(groupNode);
            shapeNode.ForAllParents(nd => nd.GetData().Changed = true);
            this.OnChanged();
        }

        public void RemoveAll()
        {
            _ntree = new NTreeNode<DrawInfo<T>>(new DrawInfo<T>(new RootNode()));

        }
            

        public void FullSyncTree()
        {

            ComputeSizes();



            _vertices = new T[_ntree.GetData().RootNode.SizeV];
            _indices  = new int[_ntree.GetData().RootNode.SizeI];

            _ntree.ForAllInOrder(
                node =>
                {
                    DrawInfo<T> info = node.GetData();
                    if (info.IsShapeNode())
                    {
                        var shapeNode = info.ShapeNode;
                        var vV = new SubArray<T>(_vertices, shapeNode.OffV, shapeNode.SizeV);
                        var vI = new SubArray<int>(_indices, shapeNode.OffI, shapeNode.SizeI);
                            shapeNode.Shape.Write(vV, vI);

                            //Adjust Indices;
                            for (int i = shapeNode.OffI; i < shapeNode.SizeI; i++)
                            {
                                _indices[i] += shapeNode.OffI;
                            }
                    }
                    info.Changed = false;
                });


        }

        public void Draw(IDxContext dx)
        {
            if (_ntree.GetData().Changed)
            {
                FullSyncTree();
                CopyToDxBuffers(dx);
            }
            //Run Root Node commands
            var rootNode=_ntree.GetData().RootNode;
            if (rootNode.Commands!= null)
                rootNode.Commands.Execute();
          
            foreach (var child in _ntree.GetChilds())
            {
                var info = child.GetData();
                if (child.GetData().IsShapeGroupNode())
                {
                    var shapeGroup = info.ShapeGroupNode;
                    if (shapeGroup.Commands != null)
                        shapeGroup.Commands.Execute();

                    switch (shapeGroup.GetTopology())
                    {
                        case TopologyType.TRIANGLES:
                            {
                                dx.DeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                                if (shapeGroup.IsIndexed())
                                {
                                    dx.DeviceContext.DrawIndexed(shapeGroup.SizeI, shapeGroup.OffI, 0);
                                }
                                else
                                {
                                    dx.DeviceContext.Draw(shapeGroup.SizeV, shapeGroup.OffV);
                                }
                                break;
                            }
                        case TopologyType.LINES:
                            {
                                dx.DeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
                                if (shapeGroup.IsIndexed())
                                {
                                    dx.DeviceContext.DrawIndexed(shapeGroup.SizeI, shapeGroup.OffI, 0);
                                }
                                else
                                {
                                    dx.DeviceContext.Draw(shapeGroup.SizeV, shapeGroup.OffV);
                                }
                                break;
                            }
                        default:
                            throw new Exception(String.Format("Draw Tree, Topology {0) not supported yet",shapeGroup.GetTopology().ToString()));
                    }
                }
            }
        }

    }
}
