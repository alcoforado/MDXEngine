using MDXEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Utilities
{
    public class ShapesManager
    {
        IDxViewControl _dxControl;
        static int i = 0;

        public ShapesManager(IDxViewControl dxControl)
        {
            _dxControl = dxControl;

        }

        ShapesManager RegisterTopology<T>(T topology, string name, string renderName) where T: ITopology
        {
            return this;

        }


    }
}
