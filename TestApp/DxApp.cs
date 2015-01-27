using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDXEngine;
using MDXEngine.Shapes;
using MDXEngine.Shaders;
using SharpDX;
using MDXEngine.Painters;
namespace TestApp
{
    public class DxApp
    {
        bool _bResize;
        Form _main;
        Control _display;
        DxControl dx;
        ShaderColor2D _shaderColor2D;
        public DxApp(Form main, Control display)
        {
            dx = new DxControl(display);
            _bResize = true;
            _main = main;
            _display = display;
            _shaderColor2D = new ShaderColor2D(dx);
            _main.Resize += (events, args) => _bResize = true;
            dx.AddShader(_shaderColor2D);
        }

        public void PsychoRun()
        {
            long i = 0;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            long limit = 6000;

            var triangle = new Triangle2DI(
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(0.5f, 0f));

            var Aquamarine = new CyclicColorizer(Color.Aquamarine); 


            _shaderColor2D.Add(triangle,Aquamarine);


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
