using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public interface  IObservable<Type>
    {
        void DetachObserver(MDXEngine.IObserver<Type> obs);
        void AttachObserver(MDXEngine.IObserver<Type> obs);


    }

    public class Observable<Type> : MDXEngine.IObservable<Type>
    {
        private List<MDXEngine.IObserver<Type>> _observers;
 
        public void DetachObserver(MDXEngine.IObserver<Type> obs)
        {
            if (_observers == null)
                return;
            if (_observers.Contains(obs))
            {
                _observers.Remove(obs);
            }
        }
        
        public void AttachObserver(MDXEngine.IObserver<Type> obs)
        {
            if (_observers == null)
                _observers = new List<MDXEngine.IObserver<Type>>();

            if (!_observers.Contains(obs))
            {
                _observers.Add(obs);
            }
        }

        protected void OnChanged(Type type)
        {
            foreach (var obs in _observers)
            {
                obs.Changed(type);
            }
        }
     
    }
}
