using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace MDXEngine
{

    
    /// <summary>
    /// The type stored in the NTree representing the DrawTree.
    /// It can only store one of three type of nodes.
    /// Shapes, Root, ShapeGroup. The info node is immutable.
    /// You can only assign one of these three classes in its constructor.
    /// To query which class is stored you have to call IsShape, IsShapeGroup, IsRoot
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DrawInfo<T>
    {
        
        private ShapeGroupNode<T> _shapeGroupNode;
        private ShapeNode<T> _shapeNode;
        private RootNode _rootNode;

        public bool Changed { get; set; }

        public DrawInfo(ShapeGroupNode<T> node)
        {
            _shapeGroupNode = node;
            _shapeNode = null;
            _rootNode = null;
            Changed = true;
        }

        public DrawInfo(ShapeNode<T> node)
        {
            _shapeGroupNode = null;
            _shapeNode = node;
            _rootNode = null;
            Changed = true;

        }

        public DrawInfo(RootNode node)
        {
            _shapeGroupNode = null;
            _shapeNode = null;
            _rootNode = node;
            Changed = true;

        }

        public ShapeGroupNode<T> ShapeGroupNode { 
            get {
                if (_shapeGroupNode == null)
                    throw new Exception("The DrawTree Info is not a ShapeGroupNode");
               return _shapeGroupNode; 
            } }
        public ShapeNode<T> ShapeNode 
        { 
            get 
            {
                if (_shapeNode == null)
                    throw new Exception("The DrawTree Info is not a ShapeNode");
               return _shapeNode; 
            } 
        }
         public RootNode RootNode 
        { 
            get 
            {
                if (_rootNode == null)
                    throw new Exception("The DrawTree Info is not a RootNode");
               return _rootNode; 
            } 
        }

        public CommandsSequence GetCommandSequence()
         {
            if (HasCommandSequence())
            {
                if (IsRootNode())
                {
                    return RootNode.Commands;
                }
                if (IsShapeGroupNode())
                {
                    return ShapeGroupNode.Commands;
                }
            }
            return null;

         }

        public bool  HasCommandSequence()
         {
             return (IsShapeGroupNode() && ShapeGroupNode.Commands != null && ShapeGroupNode.Commands.Count >= 1) ||
                 (IsRootNode() && RootNode.Commands != null && RootNode.Commands.Count >= 1); 
         }


        public bool IsShapeGroupNode() { return _shapeGroupNode != null; }
        public bool IsShapeNode() { return _shapeNode != null; }
        public bool IsRootNode() { return _rootNode != null; }
    } 

}
