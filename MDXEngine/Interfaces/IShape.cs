using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public enum TopologyType { POINTS, LINES, TRIANGLES}
    public interface IShape<T> 
    {
        void Write(SubArray<T> vV,IArray<int> vI);
        int  NVertices();
        int  NIndices();
        TopologyType GetTopology();
        List<SlotData> GetResourcesLoadCommands();
    }
}
