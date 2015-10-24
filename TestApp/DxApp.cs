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
using Microsoft.Practices.Unity;
namespace TestApp
{
    public class DxApp
    {
        bool _bResize;
        MainWindow _main;
        Control _display;
        DxControl control;

        
        private void InitializeContainer()
        {
            Container = new UnityContainer();
            Container.RegisterInstance<DxControl>(control);
            Container.RegisterType<IDxContext>(new InjectionFactory(c => this.DxControl.GetDxContext()));



        }
        
        
        public IUnityContainer Container { get; set; }


        public DxApp(MainWindow main, Control display)
        {
            control = new DxControl(display);
            SetInitialCamera();
            _bResize = true;
            _main = main;
            _display = display;
            _main.Resize += (events, args) => _bResize = true;
            _main.SetDxApp(this);
        }


        public void SetInitialCamera()
        {
            control.GetDxContext().Camera.SetCamera(new CameraShpericCoordinates()
            {
                R = 4,
                Focus = new Vector3(0, 0, 0),
                Theta = new Angle(Angle.PI_4),
                Alpha = new Angle(Angle.PI_4),
                Up = new Vector3(0, 0, 1)
            });
            control.GetDxContext().Camera.OrthonormalizeUp();

        }

        public DxControl DxControl { get { return control; } }

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
                    control.Resize();
                    _bResize = false;
                }

                control.Display();


            }
            control.Dispose();

        }
        public void EasyRun()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();


            Application.Idle += (Object sender, EventArgs e) =>
            {
                watch.Restart();
                bool drawed = control.LazyDisplay();
                watch.Stop();
                if (drawed)
                {
                    long ms = watch.ElapsedMilliseconds;
                    _main.Text = (ms == 0) ? String.Format("FPS: Inf")
                        : String.Format("FPS: {0}, Ms: {1}", Math.Round(1000.0 / ms),ms);
                }
            };
            Application.Run(_main);
        }
    }
}
