using MDXEngine;
using MDXEngine.Shaders;
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



        ShaderLight3D _shaderColor;
        MouseSphericNavigator _mouseHandler;
     


        public LightColor3DAppState(IUnityContainer container, DxControl control, MainWindow mainWindow, SecWindow form)
        {

            var dx = control.GetDxContext();
            _shaderColor = new ShaderLight3D(dx);
            control.AddShader(_shaderColor);
            form.Browser.SourceURL = "/content/html/lightcolor3d.html";
            form.PositionOnRight(mainWindow, 900);
            form.Show();
            _form = form;
            _mouseHandler = new MouseSphericNavigator(dx.Camera, true);
            _mouseHandler.AttachControl(control.Control);

        }

        public void Dispose()
        {
            _form.Close();
            _mouseHandler.DetachControl();
        }
    }
}
