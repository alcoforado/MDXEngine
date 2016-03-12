using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Painters
{
    public class EmptyPainter<T> : IPainter<T>
    {
        public void Write(IArray<T> vV, IArray<int> vI, TopologyType topologyType)
        {
            //do nothing
            return;
        }       

        /// <summary>
        /// Resturns an empty list of resources config
        /// </summary>
        /// <returns></returns>
        public List<SlotData> GetLoadResourcesCommands()
        {
            return new List<SlotData>();
        }
    }
}
