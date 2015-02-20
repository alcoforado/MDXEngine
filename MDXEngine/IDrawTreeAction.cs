using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;

namespace MDXEngine
{
    internal interface IDrawTreeAction
    {
        void Execute();
        bool TryMerge(IDrawTreeAction action);
    }


    internal class ResourceLoadCommand
    {
       public  int SlotId { get; set; }
       
       public IShaderResource Resource { get; set; }
    }


    internal class LoadResourcesDrawTreeAction
    {
        private Dictionary<int, ResourceLoadCommand> _loadCommands;
        private HLSLProgram _program;

        public LoadResourcesDrawTreeAction(HLSLProgram program)
        {
            _program = program;
            _loadCommands = new Dictionary<int, ResourceLoadCommand>();
        }

        public void Execute()
        {
            foreach (var pair in _loadCommands)
            {
                var elem = pair.Value;
                elem.Resource.Load(_program, elem.SlotId);
            }
        }


        
       

    }


   
}
