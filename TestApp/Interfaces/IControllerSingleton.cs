using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Interfaces
{
    /// <summary>
    /// Implements a controller that once created, it persists until it is Dispose method is called
    /// So it is constructor is only called once it is created for the first time due or after any of its methods
    /// are called again after its Dispose method.
    /// </summary>
    public interface IControllerSingleton : IController, IDisposable
    {
        
    }
}
