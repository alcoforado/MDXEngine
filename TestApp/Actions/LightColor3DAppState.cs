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
    public class LightColor3DAppState : IAppState
    {

        public SecWindow _form;
        public Texture Text { get; set; }

        public LightColor3DAppState(IUnityContainer container, IDxViewControl control, MainWindow mainWindow, SecWindow form)
        {
            var dx = control.GetDxContext();




            form.Browser.SourceURL = "/content/html/lightcolor3d.html";
            form.PositionOnRight(mainWindow, 900);
            form.Show();
            _form = form;


        }

        public void Dispose()
        {
            _form.Close();
        }
    }
}
