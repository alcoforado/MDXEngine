using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine {
    public class RootNode
    {
        public LoadCommandsSequence LoadCommands { get; set; }
        public int OffI, OffV;
        public int SizeI, SizeV;

        public RootNode()
        {
            OffI = -1;
            OffV = -1;
            SizeI = 0;
            SizeV = 0;
            LoadCommands = null;
        }
    
    }
}
