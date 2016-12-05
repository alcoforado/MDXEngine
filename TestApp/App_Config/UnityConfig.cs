using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TestApp.Mappers;
using TestApp.Services;
using TestApp.Services.Interfaces;

namespace TestApp.App_Config
{
    public static class UnityConfig
    {
        static public void Config(IUnityContainer container)
        {
            container.RegisterType<IShapesMngrMapper, ShapesMngrMapper>();
            container.RegisterType<IShapeMngrService, ShapeMngrService>(new ContainerControlledLifetimeManager());

        }

    }
}
