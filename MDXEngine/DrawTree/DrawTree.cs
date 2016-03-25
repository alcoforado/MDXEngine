using System;
using System.Linq;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Buffer = SharpDX.Direct3D11.Buffer;
using System.Collections.Generic;
using MDXEngine.Interfaces;
using MDXEngine.DrawTree;
using MDXEngine.DrawTree.SlotAllocation;

namespace MDXEngine
{
    public class DrawTree<T> : Observable, IDisposable where T : struct
    {
        private NTreeNode<DrawInfo<T>> _ntree;
        private T[] _vertices;
        private int[] _indices;
        private IShaderProgram _program;
        private IDxContext _context;
        Buffer _vI;
        Buffer _vV;
        private SlotResourceProvider _slotResourceProvider;


        public T[] Vertices { get { return _vertices; } }
        public int[] Indices { get { return _indices; } }

        #region private methods
       

        private void FlagNodeChange(NTreeNode<DrawInfo<T>> node)
        {
            node.GetData().Changed = true;
            node.ForItselfAndAllParents(nd => nd.GetData().Changed = true);
        }


       

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
        #endregion

 #region Resources Observers Functionality


        private Dictionary<CommandsSequence, List<IObserver>> _resourcesObservers;
        
        private class ResourceObserver : Observer
        {
            NTreeNode<DrawInfo<T>> _node;
            DrawTree<T> _tree;
            
            public ResourceObserver(DrawTree<T> tree, NTreeNode<DrawInfo<T>> node)
            {
                _node = node;
                _tree = tree;
            }
            public override void Changed()
            {
                _node.GetData().Changed = true;
                _node.ForItselfAndAllParents(nd => nd.GetData().Changed = true);
                _tree.OnChanged();
            }
           
        }
        
    

        internal void RemoveAllObservers()
        {
            foreach (var lst in _resourcesObservers)
                foreach (var ob in lst.Value)
                    ob.Detach();
            _resourcesObservers = new Dictionary<CommandsSequence, List<IObserver>>();
        }

#endregion
        




        public void Dispose()
        {
            Utilities.Dispose(ref _vI);
            Utilities.Dispose(ref _vV);
        }


        public DrawTree(IShaderProgram program,int nVertices = 0, int nIndices = 0)
        {

            _ntree = new NTreeNode<DrawInfo<T>>(new DrawInfo<T>(new RootNode()));
            _vertices = new T[nVertices];
            _indices = new int[nIndices];
            _resourcesObservers = new Dictionary<CommandsSequence, List<IObserver>>();
            _program = program;
            _slotResourceProvider = new SlotResourceProvider(program);
        }

        public RootNode GetRootNode()
        {
            return _ntree.GetData().RootNode;
        }

        public ISlotResourceAllocator GetRootSlotResourceProvider()
        {
            var result = new ShapeSlotResourceAllocator(_program, _ntree.GetData().RootNode.Commands,_slotResourceProvider);
            return result;
        }


        public void Remove(IShape<T> shape)
        {
            var nodeToDelete = _ntree.FindNodeWhere((node) => node.GetData().IsShapeNode() && node.GetData().ShapeNode == shape);
            if (nodeToDelete == null) //node does not exist nothing to do
                return;
            var parent = nodeToDelete.GetParent();
            nodeToDelete.CutSubTree();

            //Now make sure you remove all possible empty  
            //GroupNodes from the tree
            parent.ForItselfAndAllParents(
                (node) =>
                {
                    if (node.GetData().IsShapeGroupNode())
                    {
                        if (node.IsChildless())
                            node.CutSubTree();
                    }
                });
        }


        

        public void Add(IShape<T> shape)
        {

            //Get Shapes Resources
            CommandsSequence commands = this.GetShapeRequestedResources(shape);
            


            


            foreach (var node in _ntree.GetChilds())
            {

                var shapeGroup = node.GetData().ShapeGroupNode;
                if (shapeGroup.CanHaveShapeAsChild(shape))
                {
                    //Check if the command Sequence is compatible
                    if (commands == null || shapeGroup.Commands.CanMerge(commands))
                    {
                        if (commands != null)
                        {
                            shapeGroup.Commands.TryMerge(commands);
                        }

                        //Add the shape in the shape group tree
                        var newNode = new NTreeNode<DrawInfo<T>>(new DrawInfo<T>(new ShapeNode<T>(shape)));
                        node.AppendChild(newNode);
                        newNode.ForItselfAndAllParents(nd => nd.GetData().Changed = true);
                        return;
                    }

                }
            }
            //If we reach here, there is no group to add this shape.
            //Create one
            var shapeNode = new NTreeNode<DrawInfo<T>>( new DrawInfo<T>( new ShapeNode<T>(shape)));
            var groupNode = new NTreeNode<DrawInfo<T>>( new DrawInfo<T>( new ShapeGroupNode<T>(shape, commands)));
            groupNode.AppendChild(shapeNode);
            _ntree.AppendChild(groupNode);
            shapeNode.ForItselfAndAllParents(nd => nd.GetData().Changed = true);
            this.OnChanged();
        }

        private CommandsSequence GetShapeRequestedResources(IShape<T> shape)
        {
            var alloc = new ShapeSlotResourceAllocator(_program,_slotResourceProvider);
            shape.RequestSlotResources(alloc);
            return alloc.GetLoadSequence();
        }

        public void RemoveAll()
        {
            //Remove all observers
            _ntree = new NTreeNode<DrawInfo<T>>(new DrawInfo<T>(new RootNode()));
            RemoveAllObservers();

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
                            shapeNode.Shape.Draw(new DrawContext<T>
                            {
                                Vertices = vV,
                                Indices = vI,
                                TopologyType = info.ShapeNode.GetTopology()
                            });

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
