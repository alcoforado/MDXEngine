using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public class ResourcesManager
    {
        public List<IShaderResource> _resources;

        public ResourcesManager()
        {
            _resources=new List<IShaderResource>();
        }

        public void Add(IShaderResource resource)
        {
            _resources.Add(resource);
        }

        public void DisposeAllResources()
        {
            foreach (var elem in _resources.Where(elem => !elem.IsDisposed()))
            {
                elem.Dispose();
            }
            _resources.Clear();
        }
    }
}
