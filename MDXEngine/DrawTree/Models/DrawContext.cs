using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree
{
    public class DrawContext<T> : IDrawContext<T>
    {
        public SubArray<T> Vertices { get; internal set; }
        public IArray<int> Indices { get; internal set; }
        public TopologyType TopologyType { get; internal set; }
    }
}
