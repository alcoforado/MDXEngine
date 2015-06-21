using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    /// <summary>
    /// ShaderObservableDock implements the IObservable. 
    /// This class is basically a relay. 
    /// It receives an IObservable object  during its construction and when an observer attaches itself,  the ShaderObservabledock, 
    /// attaches it to the IObservable defined in its constructor.
    /// Shaders are generally implemented with a drawtree and their state change basically, when their drawtree changes.
    /// So in order to implement the IShader.ObservableDock interface method they generally create an instance of ShaderObservableDoc passing the 
    /// drawtree in its constructor and exposes it in IShader.ObservableDock. So basically when the drawtree changes, all Shader observers are notified. 
    /// Take a look at one of the native shaders like ShaderColor2D to see how this is implemented
    /// </summary>
    public class ShaderObservableDock: IObservable
    {
        IObservable _drawTree;

        public ShaderObservableDock(IObservable drawTree)
        {
            _drawTree = drawTree;
        }

        public  void AttachObserver(MDXEngine.IObserver obs)
        {
            
            _drawTree.AttachObserver(obs);
        }
        
        public void DetachObserver(MDXEngine.IObserver obs)
        {
            _drawTree.DetachObserver(obs);
        }

       
    }
}
