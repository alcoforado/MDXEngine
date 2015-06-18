using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public interface IObserver<Type>
    {
        void Changed(Type type);
    }

    public interface IObserver
    {
        void Changed();
    }

}
