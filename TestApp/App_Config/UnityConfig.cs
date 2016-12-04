using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TestApp.Mappers;

namespace TestApp.App_Config
{
    public static class UnityConfig
    {
        static public void Config(IUnityContainer container)
        {
            container.RegisterType<IShapesMngrMapper, ShapesMngrMapper>();
            
        }

    }
}
