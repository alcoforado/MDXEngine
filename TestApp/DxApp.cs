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
namespace TestApp
{
    public class DxApp
    {
        bool _bResize;
        MainWindow _main;
        Control _display;
        DxControl dx;
        
        public DxApp(MainWindow main, Control display)
        {
            dx = new DxControl(display);
            _bResize = true;
            _main = main;
            _display = display;
            _main.Resize += (events, args) => _bResize = true;
            _main.SetDxApp(this);
        }

        public DxControl DxControl { get { return dx;} }

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
                    dx.Resize();
                    _bResize = false;
                }

                dx.Display();


            }
            dx.Dispose();

        }
        public void EasyRun()
        {

        }
    }
}
