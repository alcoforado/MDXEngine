using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MDXEngine.Textures;
using MDXEngine.DrawTree;
using MDXEngine.Interfaces;

namespace MDXEngine
{
    

    public class CommandsSequence
    {
        private readonly Dictionary<string, SlotDataInternal> _loadCommands;
        private readonly HLSLProg _program;

        public CommandsSequence(HLSLProg program)
        {
            _program = program;
            _loadCommands = new Dictionary<string, SlotDataInternal>();
        }

        public CommandsSequence(HLSLProg program,List<SlotDataInternal> commands)
        {
            _program = program;
            _loadCommands = new Dictionary<string, SlotDataInternal>();
            Add(commands);
        }



        public  List<SlotData> GetAllResources()
        {
            return _loadCommands.Select(elem => (SlotData) elem.Value).ToList();
        }

        public void Execute()
        {
            foreach (var pair in _loadCommands)
            {
                var elem = pair.Value;
                _program.Load(elem.Data,elem.SlotName);
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
                        _loadCommands[elem.Value.SlotName] = new SlotDataInternal()
                        {
                            Data = elem.Value.Data,
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
                return _loadCommands[slot.Name].Data == resource;
            }
            _loadCommands[slot.Name] = new SlotDataInternal()
            {
                SlotName = slot.Name,
                Data = resource
            };
            return true;
        }

        public CommandsSequence Add(SlotDataInternal elem)
        {
            if (!TryAddLoadCommand(elem.SlotName, elem.Data))
                throw new Exception("Could not add command to the Sequence");
            return this;
        }

        public CommandsSequence Add(List<SlotDataInternal> elems)
        {
            foreach (var elem in elems)
                Add(elem); 
            return this;
        }


        

        public bool CanAddLoadCommand(string varName, IShaderResource resource)
        {
            if (_loadCommands.ContainsKey(varName))
            {
                return _loadCommands[varName].Data == resource;
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
