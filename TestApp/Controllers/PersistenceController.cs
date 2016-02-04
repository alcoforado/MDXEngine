using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class PersistenceController : IController
    {
        ISettingsService _settings;
        public PersistenceController(ISettingsService settings )
        {
            _settings = settings;
        }

       

        void Save(string nameId, string data)
        {
            _settings.Save(nameId, data);
        }
        
        string Load(string nameId)
        {
            return _settings.Load(nameId);
        }
        
        string Load(string nameId, string defaultValue)
        {
            return _settings.Load(nameId, defaultValue);
        }


    }
}
