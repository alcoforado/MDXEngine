using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public interface IObservable<Type>
    {
        void DetachObserver(MDXEngine.IObserver<Type> obs);
        void AttachObserver(MDXEngine.IObserver<Type> obs);
    }

    public interface IObservable
    {
        void DetachObserver(MDXEngine.IObserver obs);
        void AttachObserver(MDXEngine.IObserver obs);
    }

}
