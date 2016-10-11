using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp.Controllers
{
    public class TemplatesController : IController
    {

        public string GetTemplatesInFolder(string path)
        {
            var startup = AppDomain.CurrentDomain.BaseDirectory;
            if (path.StartsWith("~"))
            {
                path.Replace("~", "\\" + AppDomain.CurrentDomain.BaseDirectory);
            }
            var files=Directory.GetFiles(path).ToList();

            var files = new FileInfo(path);
            
        }

    }
}
