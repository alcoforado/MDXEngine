using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Interfaces
{
    public interface IShaderProgram
    {
        ShaderSlotsCollection ProgramResourceSlots { get; }

        void Load(IShaderResource resource, string name);



    }
}
