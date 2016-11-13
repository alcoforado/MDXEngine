using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestApp.Models.Templates;

namespace TestApp.Controllers
{
    public class TemplatesController : IController
    {

        public List<GetTemplatesViewModel> GetTemplatesInFolder(string path)
        {
            var startup = AppDomain.CurrentDomain.BaseDirectory;
            if (path.StartsWith("~"))
            {
                path.Replace("~", "\\" + AppDomain.CurrentDomain.BaseDirectory);
            }
            var files=Directory.GetFiles(path).ToList();

            return files.Select(file =>
            {
                return new GetTemplatesViewModel()
                {
                    BaseName = Path.GetFileNameWithoutExtension(path),
                    Content = System.IO.File.ReadAllText(file)
                };
            }).ToList();
        }

    }
}
