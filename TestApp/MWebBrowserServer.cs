
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
        public string Call { get; set; }
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
        private Object _controller;
        private MethodInfo _method;

        public ControllerHandler(Object controller, MethodInfo method)
        {
            _controller = controller;
            _method = method;

        }

        public Object Execute(string data)
        {
            if (_method.GetParameters().Count() == 0)
            {
                return _method.Invoke(_controller, new Object[] { });
            }
            else if (_method.GetParameters().Count() == 1)
            {
                var param = new JavaScriptSerializer().Deserialize(data, _method.GetParameters()[0].ParameterType);
                return _method.Invoke(_controller, new Object[] { param });
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
    public class MWebBrowserServer
    {
        Dictionary<string, RouterElement> Router = new Dictionary<string, RouterElement>();
        IBrowserInterface _ibrowser;
        Object _ibrowserLock = new Object();
        public MWebBrowserServer(IBrowserInterface ibrowser)
        {
            _ibrowser = ibrowser;
        }


        public void Register(IHandler handler, String Call)
        {
            Router.Add(Call, new RouterElement()
            {
                Handler = handler,
                Call = Call
            });
        }


        private string Json(Object result)
        {
            return new JavaScriptSerializer().Serialize(result);
        }


        public string JavascriptRequest(string call, string data)
        {
            Object model;
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

        public bool JavascriptRequestAsync(string call, string request_id, string data)
        {
            Object model;
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


