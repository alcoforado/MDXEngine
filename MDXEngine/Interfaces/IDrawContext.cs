using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDXEngine.Interfaces
{
    public interface IDrawContext<T> : ISlotResourceProvider
    {
        SubArray<T> Vertices { get; }
        IArray<int> Indices { get;  }
        TopologyType TopologyType { get; }
    }
}
