using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;

namespace MDXEngine.Painters
{
    public class EmptyPainter<T> : IPainter<T>
    {
        public void Draw(IDrawContext<T> context)
        {
            //do nothing
            return;
        }       

        /// <summary>
        /// Resturns an empty list of resources config
        /// </summary>
        /// <returns></returns>
        public List<SlotRequest> GetLoadResourcesCommands()
        {
            return new List<SlotRequest>();
        }
    }
}
