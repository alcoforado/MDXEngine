using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public class MayNotExist<T>  where T: class 
    {
        public T Value { get; private set; }

        public bool Exists
        {
            get { return Value != null; }
        }

        public MayNotExist(T t = null)
        {
            Value= t;
        }

      

    }
}
