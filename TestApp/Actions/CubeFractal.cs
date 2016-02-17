using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine;
namespace TestApp.Actions.CubeFractal
{
    public class CubeFractal : IAppState
    {
        private DxControl _dx;
        
        public void Dispose() { }

        public CubeFractal(DxControl dx)
        {
            _dx = dx;
          
        }

    }
}
