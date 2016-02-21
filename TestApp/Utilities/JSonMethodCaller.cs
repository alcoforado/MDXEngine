using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TestApp.Utilities
{
    public class JSonMethodCaller
    {
        static public T CallConstructor<T>(string json)
        {
            var type = typeof(T);
            var serializer = new JavaScriptSerializer();

            var obj = (Dictionary<string,object>) serializer.DeserializeObject(json);
            var dict = new Dictionary<string, object>();
            foreach (var pair in obj)
            {
                dict[pair.Key.ToLower()] = pair.Value;
            }
            
            var constructors = type.GetConstructors();
            var candidateConstructors = constructors.Where((x) =>
            {
                foreach (var param in x.GetParameters())
                {
                    if (!dict.ContainsKey(param.Name.ToLower()))
                    {
                            return false;
                    }
                    
                }
                return true;
            });
            
            if (candidateConstructors.Count() == 0)
            {
                throw new Exception(String.Format("Error trying to create object {0}. No public constructor completely mathinc the object was found",typeof(T).Name));
            }

            var Max = candidateConstructors.Max(cnst => cnst.GetParameters().Count());
            var constructor = candidateConstructors.Where(x=>x.GetParameters().Count() == Max).FirstOrDefault();

               
            //now create the arguments
            var args = constructor.GetParameters().Select(
                param => serializer.Deserialize(serializer.Serialize(dict[param.Name]) , param.ParameterType)
                ).ToArray();

            return (T) constructor.Invoke(args);

        }

        static public object CallMethod(object target, MethodInfo method, string json)
        {

            var serializer = new JavaScriptSerializer();
            var obj = (Dictionary<string, object>)serializer.DeserializeObject(json);
            var dict = new Dictionary<string, object>();
            foreach (var pair in obj)
            {
                dict[pair.Key.ToLower()] = pair.Value;
            }

            var args = method.GetParameters().Select(
                param => 
                    {
                        if (dict.ContainsKey(param.Name.ToLower()))
                        {
                            return serializer.Deserialize(serializer.Serialize(dict[param.Name]), param.ParameterType);
                        }
                        else
                        {
                            throw new Exception(String.Format("Parameter {0}  for method {1} is not present in the json data {2}",param.Name,method.Name,json));
                        }
                    }
                 ).ToArray();


            return  method.Invoke(target,args);

        }


    }
}
