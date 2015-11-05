using MDXEngine;
using MDXEngine.Textures;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp.Actions
{
    public class BinPackAppState : IAppState
    {


        public Texture Text { get; set;  }

        public BinPackAppState(IUnityContainer container, IDxViewControl control, MainWindow mainWindow, SecWindow form)
        {
            var dx = control.GetDxContext();
            
                     


            form.Browser.SourceURL = "/content/html/bin_packing.html";
            form.PositionOnRight(mainWindow,900);
            form.Show();



        }

        public void Dispose()
        {
        }
    }
}
