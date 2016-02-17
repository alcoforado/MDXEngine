using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
namespace TestApp
{

    /// </summary>
    public partial class MWebBrowser : System.Windows.Forms.WebBrowser, IBrowserInterface
    {
        MWebBrowserServer _server;

        public MWebBrowser(IUnityContainer container)
        {
            RegisterIEBrowserVersion();


            //Initialize mwebbrowserserver
            _server = new MWebBrowserServer(this, container);
            this.ObjectForScripting = _server;

        }
       

        void RegisterIEBrowserVersion()
        {
           //Get registry;
           var value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION","TestApp.exe",null);
            if (value == null)
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION","TestApp.exe",11000,RegistryValueKind.DWord);
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION","TestApp.vshost.exe",11000,RegistryValueKind.DWord);
            }
           
        }
       
        private String _source;
        public String SourceURL
        {
            get
            {
                return _source;
            }

            set 
            {
                _source = value.Trim();
                if (_source[0] == '/')
                {
                    _source=_source.TrimStart('/');
                    _source = System.IO.Path.GetFullPath(_source);
                   
                    var uri = new UriBuilder(_source);
                    uri.Host = "127.0.0.1";
                    _source = uri.ToString();

                    var pattern = "/(.):/";
                    _source = Regex.Replace(_source,pattern,"/$1$$/");

                   
                }
                else
                    _source = new Uri(value).AbsolutePath;
                this.Navigate(new Uri(_source));
            }

        }
           
       
       
      

        public void RunScript(string functionName, params object[] parameters)
        {
            this.Document.InvokeScript(functionName, parameters);
        }

       

    }




     


}
    