using MDXEngine;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp.Actions
{
    public class BinPackAction : IActionMenu
    {
       
        
        public BinPackAction(IUnityContainer container,DxControl control,MainWindow mainWindow)
        {
            var form = new SecWindow(container);
            form.Browser.SourceURL = "/content/html/bin_packing.html";
            form.PositionOnRight(mainWindow,400);
            form.Show();
        }

        public void Dispose()
        {
        }
    }
}
