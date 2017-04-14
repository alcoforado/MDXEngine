using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestApp.Services.Interfaces;

namespace TestApp.Services
{
    public class AppSettings : IAppSettings
    {
        private JsonFileStore _store;

        public AppSettings()
        {
            string content = "";

            content = !File.Exists("appsettings.json") ? "{}" : File.ReadAllText("appsettings.json");

            SetDefaultValues();


            JsonConvert.PopulateObject(content,this);
        }

        private void SetDefaultValues()
        {
            this.FormPosition = new Point(0,0);
            this.FormSize = new Size(800, 600);
        }

        ~AppSettings()
        {
            var content=JsonConvert.SerializeObject(this,Formatting.Indented);
            File.WriteAllText("appsettings.json",content);
        }

        public string Cad3dFile { get; set; }


        public Point FormPosition { get; set; }
        public Size FormSize { get; set; }
    }
}
