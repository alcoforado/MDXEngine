﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{

    public class NTreeNode<Data>
    {
        private List<NTreeNode<Data>> _childs;

        private NTreeNode<Data> _parent;
        
        private Data _data;

        public NTreeNode(Data Data)
        {
            _parent = null;
            _childs = null;
            _data = Data;
        }

        public bool IsChildless()
        {
            return (_childs == null || _childs.Count == 0);
        }

        public bool IsRoot()
        {
            return _parent == null;
        }

        public void AppendChild(NTreeNode<Data> child)
        {
            if (_childs == null)
                _childs = new List<NTreeNode<Data>>();
            _childs.Add(child);
            child._parent = this;
        }

        public Data GetData()
        {
            return _data;
        }

        public NTreeNode<Data> GetParent()
        {
            return _parent;
        }

        public NTreeNode<Data> GetChild(int index)
        {
            if (this.IsChildless())
                throw new Exception("Node is childless");
            else
                return _childs[index];
        }

        public List<NTreeNode<Data>> GetChilds()
        {
            if (_childs == null)
            {
                _childs = new List<NTreeNode<Data>>();
            }
            return _childs;
        }

        public NTreeNodeIterator<Data> GetIterator()
        {
            return new NTreeNodeIterator<Data>(this);

        }

        public void ForAllParents(Action<NTreeNode<Data>> action)
        {
            NTreeNode<Data> ptr;
            ptr = this;
            
            while (ptr._parent != null)
            {
                ptr = ptr._parent;
                action(ptr);
            }
        }

        public void ForAllInOrder(Action<NTreeNode<Data>> visitor)
        {
            if (this.IsChildless())
            {
                visitor(this);
                return;
            }
            else
            {
                foreach (var ch in _childs)
                {
                    ch.ForAllInOrder(visitor);
                }
                visitor(this);
            }
        }



    }
    
}