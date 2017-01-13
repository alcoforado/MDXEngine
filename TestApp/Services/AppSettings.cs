using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public class AppSettings
    {
        private AppSettings DefaultValues = new AppSettings()
        {
            Cad3dFile = "./Cad3d.json"
        };

        public string Cad3dFile { get; set; }

    }
}
