using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public class ObservableDock : IObservable
    {
      private List<MDXEngine.IObserver> _observers;

        public void DetachObserver(MDXEngine.IObserver obs)
        {
            if (_observers == null)
                return;
            if (_observers.Contains(obs))
            {
                _observers.Remove(obs);
            }
        }

        public void AttachObserver(MDXEngine.IObserver obs)
        {
            if (_observers == null)
                _observers = new List<MDXEngine.IObserver>();

            if (!_observers.Contains(obs))
            {
                obs.Observable = this;
                _observers.Add(obs);
            }
        }

        public void OnChanged()
        {
            if (_observers == null)
                return;
            foreach (var obs in _observers)
            {
                obs.Changed();
            }
        }
    }

    
}
