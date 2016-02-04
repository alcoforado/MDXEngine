using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp.Services
{
    public interface ISettingsService
    {
       void Save(string nameId, string data);
       string Load(string nameId);
       string Load(string nameId, string defaultValue);
    }
}
