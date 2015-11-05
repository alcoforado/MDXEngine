using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public interface IAppStateProvider
    {
        T GetAppState<T>() where T : class,IAppState;
    }
}
