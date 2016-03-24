using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MDXEngine.DrawTree;
using MDXEngine.Interfaces;

namespace MDXEngine
{
    

    public class CommandsSequence
    {
        private readonly Dictionary<string, ILoadCommand> _loadCommands;
        private readonly IShaderProgram _program;
        private readonly ISlotResourceAllocator _slotResourceProvider;
        public CommandsSequence(IShaderProgram program,ISlotResourceAllocator slotResourceProvider)
        {
            _program = program;
            _loadCommands = new Dictionary<string, ILoadCommand>();
            _slotResourceProvider = slotResourceProvider;
        }

        public CommandsSequence(IShaderProgram program)
        {
            _program = program;
            _loadCommands = new Dictionary<string, ILoadCommand>();
        }




        public void Execute()
        {
            foreach (var pair in _loadCommands)
            {
                var command = pair.Value;
                command.Load();
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
            return _program == commands._program && commands._loadCommands.All(elem => this.CanAddLoadCommand(elem.Value.SlotName, elem.Value.Data));
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
                        Debug.Assert(_loadCommands[elem.Key].Data == elem.Value.Data);
                    else
                        _loadCommands[elem.Key] = elem.Value;
                }
                return true;
            }
            return false;
        }
       
        #endregion


        #region Add one command to the command sequence

        public bool TryAddLoadCommand(ILoadCommand command)
        {
           var result = _program.ProgramResourceSlots[command.SlotName];
#if DEBUG
             if (!result.Exists)
                 throw new Exception(String.Format("Variable {0} is not defined in program", command.SlotName));
#endif
             

            var slot = result.Value;

            if (_loadCommands.ContainsKey(slot.Name))
            {
                return false;
            }
            else
            {
                

            }
            _loadCommands[slot.Name] = command; 
            return true;
        }

        public CommandsSequence Add(ILoadCommand elem)
        {
            if (!TryAddLoadCommand(elem))
                throw new Exception("Could not add command to the Sequence");
            return this;
        }

        public CommandsSequence Add(List<ILoadCommand> elems)
        {
            foreach (var elem in elems)
                Add(elem); 
            return this;
        }


        

        public bool CanAddLoadCommand(ILoadCommand comm)
        {
            return !_loadCommands.ContainsKey(comm.SlotName);
        }
      
        #endregion

        public int Count
        {
            get { return _loadCommands.Count; }
        }

        public bool Any()
        {
            return _loadCommands.Count >= 1;
        }
    }
}
