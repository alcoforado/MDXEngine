using MDXEngine;
using MDXEngine.Textures;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp.Actions
{
    public class BinPackAppState : IAppState
    {

        public SecWindow _form;
        public Bitmap Text { get; set;  }

        public BinPackAppState(IUnityContainer container, IDxViewControl control, MainWindow mainWindow, SecWindow form)
        {
            var dx = control.GetDxContext();
            
                     


            form.Browser.SourceURL = "/content/html/bin_packing.html";
            form.PositionOnRight(mainWindow,900);
            form.Show();
            _form = form;


        }

        public void Dispose()
        {
            _form.Close();
        }
    }
}
