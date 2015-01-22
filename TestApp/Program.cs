using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainWindow mainW = new MainWindow();
           // var hello = new HelloWorld();
            DxApp app = new DxApp(mainW,mainW.RenderControl());
            //mainW.SetDxApp(app);
            app.PsychoRun();
           //var cl = new Test.TestII();


        }
    }
}
