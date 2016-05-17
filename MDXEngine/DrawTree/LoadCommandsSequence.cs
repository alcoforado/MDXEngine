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
        private class LoadCommandEntry
        {
            public ILoadCommand LoadCommand { get; set; }

            public int RefCount  { get; set; } 
        }

        private readonly Dictionary<string, LoadCommandEntry> _loadCommands;
        private readonly IShaderProgram _program;
        public LoadCommandsSequence(IShaderProgram program,ISlotResourceAllocator slotResourceProvider)
        {
            _program = program;
            _loadCommands = new Dictionary<string, LoadCommandEntry>();
        }

        public LoadCommandsSequence(IShaderProgram program)
        {
            _program = program;
            _loadCommands = new Dictionary<string, LoadCommandEntry>();
        }


         

        public void Execute()
        {
            foreach (var pair in _loadCommands)
            {
                var command = pair.Value;
                command.LoadCommand.Load();
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
            return _program == loadCommands._program && loadCommands._loadCommands.All(elem => this.CanAddLoadCommand(elem.Value.LoadCommand));
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
                 throw new Exception(String.Format("Variable {0} is not defined in shader program", command.SlotName));
#endif
             

            var slot = result.Value;

            if (_loadCommands.ContainsKey(slot.Name))
            {
                if (command.CanBeOnSameSlot(_loadCommands[slot.Name].LoadCommand))
                {
                    _loadCommands[slot.Name].RefCount++;
                    return true;
                }
                return false;
            }
            _loadCommands[slot.Name] = new LoadCommandEntry()
            {
                LoadCommand = command,
                RefCount = 1
            }; 
            return true;
        }

        public LoadCommandsSequence Add(ILoadCommand elem)
        {
            if (!TryAddLoadCommand(elem))
                throw new Exception("Could not add command to the Sequence");
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
