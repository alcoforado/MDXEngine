using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDXEngine;
using MDXEngine.Shapes;
using SharpDX;
using MDXEngine.Painters;
using MDXEngine.Geometry;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using TestApp.App_Config;
using TestApp.Services;
namespace TestApp
{
    public class DxApp
    {
        bool _bResize;
        MainWindow _main;
        DxControl _control;
        public IUnityContainer Container { get; set; }


        
        private void RegisterTypesInContainer()
        {
            Container.RegisterInstance<DxControl>(_control);
            Container.RegisterInstance<MainWindow>(_main);
            Container.RegisterType<IDxContext>(new InjectionFactory(c => this.DxControl.GetDxContext()));
            Container.RegisterInstance<IUnityContainer>(this.Container);
            Container.RegisterType<IDxViewControl, DxControl>();
            Container.RegisterType<MWebBrowser>();
            Container.RegisterType<SecWindow>();
            Container.RegisterType<IAppStateProvider,MainWindow>();

            //Register Services
            Container.RegisterType<ISettingsService, SettingsService>();
        }
        



       
            


    



        public DxApp()
        {
            //InitializeContainer
            Container = new UnityContainer();
            
            //Create Main Window
            var menuActionFactory =  new ImplementationFactory<IAppState>(Container, (Type t) =>
            {
                var baseName = t.Name.Replace("Action","").Replace("MenuAction","").Replace("AppState","");
                var result = new List<string>() { baseName, baseName+"Action",baseName + "MenuAction",baseName + "AppState" };
                return result.Distinct().ToList();
            });

            _main = new MainWindow(menuActionFactory);
            _control = new DxControl(_main.RenderControl());

            
                        
            SetInitialCamera();
            _bResize = true;
           
            _main.Resize += (events, args) => _bResize = true;
            _main.SetDxApp(this);


            this.RegisterTypesInContainer();
            TestApp.App_Config.UnityConfig.Config(this.Container);

            string baseAddress = "http://localhost:9000/";
            Startup.Container = this.Container;
            WebApp.Start<Startup>(url: baseAddress);
        }


        public void SetInitialCamera()
        {
            _control.GetDxContext().Camera.SetCamera(new CameraShpericCoordinates()
            {
                R = 4,
                Focus = new Vector3(0, 0, 0),
                Theta = new Angle(Angle.PI_4),
                Alpha = new Angle(Angle.PI_4),
                Up = new Vector3(0, 0, 1)
            });
            _control.GetDxContext().Camera.OrthonormalizeUp();

        }

        public DxControl DxControl { get { return _control; } }

        public void PsychoRun()
        {
            long i = 0;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            long limit = 6000;


            _main.Show();
            while (_main.Created)
            {
                Application.DoEvents();

                if (i++ == limit)
                {
                    watch.Stop();
                    long ms = watch.ElapsedMilliseconds;
                    i = 0;
                    long FPS = limit * 1000 / ms;
                    _main.Text = String.Format("FPS: {0}", FPS);
                    limit = FPS;
                    //log.WriteLine(String.Format("FPS: {0}", ms));
                    watch.Restart();

                }
                if (_bResize)
                {
                    _control.Resize();
                    _bResize = false;
                }

                _control.Display();


            }
            _control.Dispose();

        }
        public void EasyRun()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();


            Application.Idle += (Object sender, EventArgs e) =>
            {
                watch.Restart();
                bool drawed = _control.LazyDisplay();
                watch.Stop();
                if (drawed)
                {
                    long ms = watch.ElapsedMilliseconds;
                    _main.Text = (ms == 0) ? String.Format("FPS: Inf")
                        : String.Format("FPS: {0}, Ms: {1}", Math.Round(1000.0 / ms), ms);
                }
            };
            Application.Run(_main);
        }
    }
}
