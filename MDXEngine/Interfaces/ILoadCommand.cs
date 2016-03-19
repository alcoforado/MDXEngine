using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.DrawTree;

namespace MDXEngine.Interfaces
{
    public interface ILoadCommand : ISlotRequest
    {
        
        void Load();
    }

}
