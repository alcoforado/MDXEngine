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
        IObservable Observable { get; set; }
        void Changed();
        void Detach();
    }

    public abstract class Observer : IObserver 
    {
        public abstract void Changed();
        public IObservable Observable { get; set; }
        public void Detach()
        {
            Observable.DetachObserver(this);
        }
    }

}
