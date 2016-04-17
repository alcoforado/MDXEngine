using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MDXEngine.DrawTree;
using MDXEngine.Interfaces;

namespace MDXEngine
{
    

    public class LoadCommandsSequence
    {
        private readonly Dictionary<string, ILoadCommand> _loadCommands;
        private readonly IShaderProgram _program;
        private readonly ISlotResourceAllocator _slotResourceProvider;
        public LoadCommandsSequence(IShaderProgram program,ISlotResourceAllocator slotResourceProvider)
        {
            _program = program;
            _loadCommands = new Dictionary<string, ILoadCommand>();
            _slotResourceProvider = slotResourceProvider;
        }

        public LoadCommandsSequence(IShaderProgram program)
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

        #region Merge Two loadCommands Sequence



        /// <summary>
        /// Check if two loadCommands sequence can merge in just one command sequence.
        /// This method returns true if not two different resources occupy the same slot.
        /// </summary>
        /// <param name="loadCommands"></param>
        /// <returns></returns>
        public bool CanMerge(LoadCommandsSequence loadCommands)
        {
            return _program == loadCommands._program && loadCommands._loadCommands.All(elem => this.CanAddLoadCommand(elem.Value));
        }

        /// <summary>
        /// Try to merge two loadCommands sequence.
        /// 
        /// </summary>
        /// <param name="loadCommands"></param>
        /// <returns> true if merge was successfull, false otherwise</returns>
        public bool TryMerge(LoadCommandsSequence loadCommands)
        {
            if (CanMerge(loadCommands))
            {
                foreach (var elem in loadCommands._loadCommands)
                {
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
                return command.CanBeOnSameSlot(_loadCommands[slot.Name]);
            }
            _loadCommands[slot.Name] = command; 
            return true;
        }

        public LoadCommandsSequence Add(ILoadCommand elem)
        {
            if (!TryAddLoadCommand(elem))
                throw new Exception("Could not add command to the Sequence");
            return this;
        }

        public LoadCommandsSequence Add(List<ILoadCommand> elems)
        {
            foreach (var elem in elems)
                Add(elem); 
            return this;
        }


        

        public bool CanAddLoadCommand(ILoadCommand comm)
        {
            if (_loadCommands.ContainsKey(comm.SlotName))
            {
                return comm.CanBeOnSameSlot(comm);
            }
            return true;
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
