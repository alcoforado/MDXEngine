using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MDXEngine.Textures;

namespace MDXEngine
{
 internal class ResourceLoadCommand
    {
       public  int SlotId { get; set; }
       
       public IShaderResource Resource { get; set; }
    }


    public class CommandsSequence
    {
        private readonly Dictionary<int, ResourceLoadCommand> _loadCommands;
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

        public bool CanAddLoadCommand(int slotId,IShaderResource resource)
        {
            if (_loadCommands.ContainsKey(slotId))
            {
                return _loadCommands[slotId].Resource == resource;
            }
            else
            {
                return true;
            }
        }



        public bool CanMerge(CommandsSequence commands )
        {
            return _program == commands._program && commands._loadCommands.All(elem => this.CanAddLoadCommand(elem.Value.SlotId, elem.Value.Resource));
        }

        public bool TryMerge(CommandsSequence commands)
        {
            if (CanMerge(commands))
            {
                foreach (var elem in commands._loadCommands)
                {
                    if (_loadCommands.ContainsKey(elem.Key))
                        Debug.Assert(_loadCommands[elem.Key].Resource == elem.Value.Resource);
                    else
                        _loadCommands[elem.Value.SlotId] = new ResourceLoadCommand()
                        {
                            Resource = elem.Value.Resource,
                            SlotId = elem.Value.SlotId
                        };
                }
                return true;
            }
            return false;
        }


        public bool TryAddLoadCommand(string varName,IShaderResource resource)
        {
           var result = _program.GetResourceSlot(varName);
#if DEBUG
             if (!result.Exists)
                throw new Exception(String.Format("Variable {0} is not defined in program",varName));
#endif
           var slot = result.Value;

            if (_loadCommands.ContainsKey(slot.Slot))
            {
                return _loadCommands[slot.Slot].Resource == resource;
            }
            _loadCommands[slot.Slot] = new ResourceLoadCommand()
            {
                SlotId = slot.Slot,
                Resource = resource
            };
            return true;
        }

        public void AddLoadCommand(string varName, IShaderResource resource)
        {
            if (!TryAddLoadCommand(varName, resource))
            {
                throw new Exception("Could not add command to the Sequence");
            }
            
        }

        public int Count
        {
            get { return _loadCommands.Count; }
        }
    }
}
