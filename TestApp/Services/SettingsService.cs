using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestApp.Services;

namespace TestApp.Services
{
    public class SettingsService : ISettingsService
    {
        public string Cad3DMainFile { get; set; }


        private static Object lck = new Object();

        private static Dictionary<String, String> _dictionary;



        private Dictionary<String, String> Dictionary
        {
            get
            {
                if (_dictionary == null)
                {
                    lock(lck)
                    {
                        if (_dictionary == null) //check inside the lock as well
                        {
                            var settings = Properties.Settings.Default.Setting;
                            if (settings == null)
                            {
                                Properties.Settings.Default.Setting = new System.Collections.Specialized.StringCollection();
                                _dictionary = new Dictionary<string, string>();
                            }
                            else
                            {
                                 _dictionary = new Dictionary<string, string>();
                                foreach (string elem in settings)
                                {
                                    var match = Regex.Match(elem, "([^-]+)-");
                                    if (match.Success)
                                    {
                                        _dictionary.Add(match.Groups[1].Value,Regex.Replace(elem,"[^-]+-",""));
                                    }
                                }

                            }
                        }
                    }
                }
                return _dictionary;
            }
        }


        public void Save(string nameId, string data)
        {
            var dictionary = this.Dictionary;
            lock(lck)
            {
                dictionary[nameId] = data;
            }
        }
        
        public string Load(string nameId)
        {
            if (Dictionary.ContainsKey(nameId))
                return Dictionary[nameId];
            else
                return "";
        }

        public string Load(string nameId,string defaultValue)
        {
            if (Dictionary.ContainsKey(nameId))
                return Dictionary[nameId];
            else
                return defaultValue;
        }

        static public void PersistSettings()
        {
            if (_dictionary == null)
                return;
            var result = new System.Collections.Specialized.StringCollection();
            foreach(var elem in _dictionary)
            {
                var str = String.Format("{0}-{1}",elem.Key,elem.Value);
                result.Add(str);
            }
            Properties.Settings.Default.Setting = result;
            Properties.Settings.Default.Save();

        }

    }
}