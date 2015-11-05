
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Web.Script.Serialization;
using Microsoft.Practices.Unity;
using TestApp.Interfaces;
namespace TestApp
{

    public struct Response
    {
        public int MessageID { get; set; }
        public string Call { get; set; }
        public Object Data { get; set; }
        public int Code { get; set; }
        public String ErrorMessage { get; set; }
    }

    public class RouterElement
    {
        public IHandler Handler { get; set; }
    }

    public interface IHandler
    {
        object Execute(string data);
    }

    public class DelegateHandler : IHandler
    {
        public delegate Object Exec(string data);
        Exec _f;

        public DelegateHandler(Exec f)
        {
            _f = f;
        }
        public Object Execute(string data)
        {
            return _f(data);
        }

    }

    public class ControllerHandler : IHandler
    {
        private Object _controllerCashed;
        private string _controllerId;
        private MethodInfo _method;
        private IFactory<IController> _factory;
        

        //If this constructor is used the controller will be created every time
        //we execute. (It is like Asp.NET). Stateless controllers tend to be better code.
        public ControllerHandler(IFactory<IController> factory, string controllerId, MethodInfo method)
        {
            _controllerCashed = null;
            _controllerId = controllerId;
            _method = method;
            _factory = factory;
        }

        public Object Execute(string data)
        {
            if (_method.Name == "Dispose" && _method.GetParameters().Count() == 0 && _controllerCashed is IControllerSingleton)
            {
                if (_controllerCashed != null)
                {
                    ((IControllerSingleton)_controllerCashed).Dispose();
                    _controllerCashed = null;
                }
                return null;
            }                  
             
            var targetController = _controllerCashed;
            if (_controllerCashed == null)
            {
                targetController = _factory.Resolve(_controllerId);
                if (targetController is IControllerSingleton)
                {
                    _controllerCashed = targetController;
                }
            }

            if (_method.GetParameters().Count() == 0)
            {
                return _method.Invoke(targetController, new Object[] { });
            }
            else if (_method.GetParameters().Count() == 1)
            {
                var param = new JavaScriptSerializer().Deserialize(data, _method.GetParameters()[0].ParameterType);
                return _method.Invoke(targetController, new Object[] { param });
            }
            else
            {
                Object dataObj = new JavaScriptSerializer().DeserializeObject(data);
                var parameters = new List<Object>();
                throw new Exception("Not supported yet");
            }
        }

    }
    public interface IBrowserInterface
    {
        void RunScript(string functionName, params object[] parameters);
    }
   



    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class MWebBrowserServer : TestApp.IAjaxServer 
    {
        Dictionary<string, RouterElement> Router = new Dictionary<string, RouterElement>();
        IBrowserInterface _ibrowser;
        Object _ibrowserLock = new Object();
        ImplementationFactory<IController> _factory;
        public MWebBrowserServer(IBrowserInterface ibrowser,IUnityContainer container)
        {
            _factory = new ImplementationFactory<IController>(container,(Type t) => {
                var result = new List<String>() { t.Name.ToLower(), t.Name.ToLower().Replace("controller","")};
                return result.Distinct().ToList();
            });
            _ibrowser = ibrowser;
            Register(_factory);

        }





        private void Register(ImplementationFactory<IController> container)
        {
            var types = container.GetAllTypes();
            foreach (var type in types)
            {
                var typeNames = container.GetTypeMapping(type);
                foreach (var method in type.GetMethods())
                {
                    if (method.GetParameters().Count() == 0 || method.GetParameters().Count() == 1)
                    {
                        foreach(var typeName in typeNames)
                        {
                        var el = new RouterElement() {
                            Handler= new ControllerHandler(container, typeName, method),
                        };
                        string call = String.Format("/{0}/{1}",typeName,method.Name.ToLower());
                         Router.Add(call,el);

                        }
                    }
                }
            }
        }



        private string Json(Object result)
        {
            return new JavaScriptSerializer().Serialize(result);
        }


        public string JavascriptRequest(string urlPath, string data)
        {
            var call = urlPath.ToLower();
            if (!Router.ContainsKey(call))
            {
                var result = Json(new Response()
                {
                    Call = call,
                    Code = 404,
                    Data = null
                });
                return result;
            }
            try
            {
                Object result = Router[call].Handler.Execute(data);
                return Json(new Response()
                {
                    Call = call,
                    Code = 201,
                    Data = result
                });
            }
            catch (Exception e)
            {
                return Json(new Response()
                {
                    Call = call,
                    Code = 503,
                    Data = null,
                    ErrorMessage = e.Message
                });

            }

        }

        public bool JavascriptRequestAsync(string urlPath, string request_id, string data)
        {
            var call = urlPath.ToLower();
            if (!Router.ContainsKey(call))
            {
                return false;

            }



            Task<string> requestTask = new Task<string>(() =>
            {
                string json = null;
                try
                {
                    var result = Router[call].Handler.Execute(data);
                    json = Json(new Response()
                    {
                        Call = call,
                        Code = 201,
                        Data = result
                    });
                }
                catch (Exception e)
                {
                    json = Json(new Response()
                    {
                        Call = call,
                        Code = 503,
                        Data = null,
                        ErrorMessage = e.Message
                    });
                }

                lock (_ibrowserLock)
                {


                    //  _ibrowser.RunScriptAsync("Interop.WpfAjaxManager.Instance.finishRequest",  call, request_id, json );
                    _ibrowser.RunScript("WpfFinishRequest", call, request_id, json);

                }

                return json;

            });
            requestTask.Start();
            //Add request task
            return true;
        }


    }
}


