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


        public class SaveRequest
        {
            public string key;
            public string json;

        }


        public class LoadRequest
        {
            public string key;
            public string defaultValue;
        }



        public void Save(SaveRequest request)
        {
            _settings.Save(request.key, request.json);
        }
        
        public string Load(LoadRequest request)
        {
            if (request.defaultValue != null)
                return _settings.Load(request.key);
            else
                return _settings.Load(request.key, request.defaultValue);
        }
        
       


    }
}
