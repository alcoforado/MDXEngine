﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public interface IFactory<T>
    {
        T Resolve(string typeName);
        Type GetType(string typeName);
       
    }
}
