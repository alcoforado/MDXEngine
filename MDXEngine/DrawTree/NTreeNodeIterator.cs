using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public class NTreeNodeIterator<T>
    {
        NTreeNode<T> data;

        public NTreeNodeIterator(NTreeNode<T> node)
        {
            data = node;
        }

        public T GetData() { return data.GetData(); }
        
        
        
        public NTreeNode<T> GetNode()
        {
            return data;
        }
        public bool GotoChild()
        {
            if (data.IsChildless())
                return false;
            else
                data = data.GetChilds()[0];
            return true;
        }

        public bool GotoNextSibling()
        {
            var parent = data.GetParent();
            if (parent == null)
                return false;
            var childs = parent.GetChilds();
            int i = childs.IndexOf(data);
            if (i == -1 || i == childs.Count - 1)
            {
                return false;
            }
            else
            {
                data = parent.GetChild(i + 1);
                return true;
            }
        }

        public bool GotoPrevSibling()
        {
            var parent = data.GetParent();
            if (parent == null)
                return false;
            var childs = parent.GetChilds();
            int i = childs.IndexOf(data);
            if (i == -1 || i == 0)
            {
                return false;
            }
            else
            {
                data = parent.GetChild(i - 1);
                return true;
            }
        }

        public bool GotoParent()
        {
            var parent = data.GetParent();
            if (parent == null)
                return false;
            else
            {
                data = parent;
                return true;
            }
        }








    }
}
