using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MDXEngine.Textures;

namespace MDXEngine
{
 internal class ResourceLoadCommand
    {
       public  string SlotName { get; set; }
       
       public IShaderResource Resource { get; set; }
    }


    public class CommandsSequence
    {
        private readonly Dictionary<string, ResourceLoadCommand> _loadCommands;
        private readonly HLSLProgram _program;

        public CommandsSequence(HLSLProgram program)
        {
            _program = program;
            _loadCommands = new Dictionary<string, ResourceLoadCommand>();
        }

        public  List<IShaderResource> GetAllResources()
        {
            var result = new List<IShaderResource>();
            foreach (var elem in _loadCommands)
            {
                result.Add(elem.Value.Resource);
            }
            return result;
        }

        public void Execute()
        {
            foreach (var pair in _loadCommands)
            {
                var elem = pair.Value;
                elem.Resource.Load(_program, elem.SlotName);
            }
        }

        #region Merge Two Commands Sequence



        /// <summary>
        /// Check if two commands sequence can merge in just one command sequence.
        /// This method returns true if not two different resources occupy the same slot.
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public bool CanMerge(CommandsSequence commands)
        {
            return _program == commands._program && commands._loadCommands.All(elem => this.CanAddLoadCommand(elem.Value.SlotName, elem.Value.Resource));
        }

        /// <summary>
        /// Try to merge two commands sequence.
        /// 
        /// </summary>
        /// <param name="commands"></param>
        /// <returns> true if merge was successfull, false otherwise</returns>
        public bool TryMerge(CommandsSequence commands)
        {
            if (CanMerge(commands))
            {
                foreach (var elem in commands._loadCommands)
                {
                    if (_loadCommands.ContainsKey(elem.Key))
                        Debug.Assert(_loadCommands[elem.Key].Resource == elem.Value.Resource);
                    else
                        _loadCommands[elem.Value.SlotName] = new ResourceLoadCommand()
                        {
                            Resource = elem.Value.Resource,
                            SlotName = elem.Value.SlotName
                        };
                }
                return true;
            }
            return false;
        }
       
        #endregion


        #region Add one command to the command sequence

        public bool TryAddLoadCommand(string varName,IShaderResource resource)
        {
           var result = _program.ProgramResourceSlots[varName];
#if DEBUG
             if (!result.Exists)
                throw new Exception(String.Format("Variable {0} is not defined in program",varName));
#endif
           var slot = result.Value;

            if (_loadCommands.ContainsKey(slot.Name))
            {
                return _loadCommands[slot.Name].Resource == resource;
            }
            _loadCommands[slot.Name] = new ResourceLoadCommand()
            {
                SlotName = slot.Name,
                Resource = resource
            };
            return true;
        }

        public CommandsSequence LoadResource(string varName,IShaderResource resource)
        {
            if (!TryAddLoadCommand(varName, resource))
                throw new Exception("Could not add command to the Sequence");
            return this;
        }


        public bool CanAddLoadCommand(string varName, IShaderResource resource)
        {
            if (_loadCommands.ContainsKey(varName))
            {
                return _loadCommands[varName].Resource == resource;
            }
            else
            {
                return true;
            }
        }
      
        #endregion

        public int Count
        {
            get { return _loadCommands.Count; }
        }
    }
}
