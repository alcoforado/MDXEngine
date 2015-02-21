using System.Collections.Generic;
using System.Diagnostics;
using MDXEngine.Textures;

namespace MDXEngine
{
 internal class ResourceLoadCommand
    {
       public  int SlotId { get; set; }
       
       public IShaderResource Resource { get; set; }
    }


    internal class CommandsSequence
    {
        private Dictionary<int, ResourceLoadCommand> _loadCommands;
        private readonly HLSLProgram _program;

        public CommandsSequence(HLSLProgram program)
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

        public bool TryAddCommand(string varName,Texture texture)
        {
            var result = _program.GetTextureSlot(varName);
            Debug.Assert(result.Exists);
            var slot = result.Value;

            if (_loadCommands.ContainsKey(slot.Slot))
            {
                return _loadCommands[slot.Slot].Resource == texture;
            }
            _loadCommands[slot.Slot] = new ResourceLoadCommand()
            {
                SlotId = slot.Slot,
                Resource = texture
            };
            return true;
        }

        
       

    }


   
}
