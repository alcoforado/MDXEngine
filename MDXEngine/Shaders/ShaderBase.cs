using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    /// <summary>
    /// Defines basic implementation of some methods of the IShape interface.
    /// This classes is used to help Shaders Implement some methods of the IShape.
    /// Note that a shader needs to inherit IShader only. This class is by no means necessary for any 
    /// shader. It is just to avoid boiler plate code. 
    /// This is the beheaveour of this class
    /// 
    /// -Create, Initialize a DrawTree and let it be available for the specialized classes. 
    /// -Implement the IObservable interface, an iterface necessary for IShader. It does that by wiring the change events of its DrawTree.
    /// -If you implement a shader whose need to draw when other member beside the DrawTree changes,  
    ///  don't forget to call the protected method ShaderBase::OnChange() to notify the observers (basically the DxControl) 
    ///  that your class changed and need to be redraw.  
    ///  
    ///  If  the shader do not need a draw tree for some reason, consideri implementing directly from IShader instead of use this clas.
    /// </summary>
    public  abstract class ShaderBase<T> : Observable, IShader where T: struct
    {
        protected DrawTree<T> Root { get; private set; }

        public ShaderBase()
        {
            Root = new DrawTree<T>();
        }

        public override void AttachObserver(MDXEngine.IObserver obs)
        {
            base.AttachObserver(obs);
            Root.AttachObserver(obs);
        }
        
        public override void DetachObserver(MDXEngine.IObserver obs)
        {
            base.DetachObserver(obs);
            Root.DetachObserver(obs);
        }

        public abstract void Draw(IDxContext dx);
        
        public virtual void Dispose()
        {
            Root.Dispose();
            Root = null;
        }
    }
}
