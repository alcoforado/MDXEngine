using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Interfaces
{
    public interface IBitmap : IDisposable
    {
        
        void IncRefCount();




        int Width { get; }

        int Height { get; }

        void Save(string file);
    }
}
