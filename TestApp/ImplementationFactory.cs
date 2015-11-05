using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    /// <summary>
    /// This Factory uses a mapping from string to type to decide with implementation of the interface
    /// passed as type parameter to create
    /// </summary>
    


    public class ImplementationFactory<T>  : IFactory<T>
    {
        Dictionary<string, Type> _typeMapping;
        private IUnityContainer _container;

        public delegate List<string> TypeMap(Type t);
        TypeMap _mapping;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="map"> A function that given a string, it returns the possible type names associated with it</param>
        public ImplementationFactory(IUnityContainer container,TypeMap idToTypeMap)
        {
            if (!typeof(T).IsInterface)
            {
                throw new Exception("Error ImplementationFactory<T> expect T to be an interface");
            }

            _container = container;
            _typeMapping = new Dictionary<string, Type>();
            _mapping = idToTypeMap;
            //Register Controllers
            foreach (Type t in this.GetType().Assembly.GetTypes())
            {
                if (t.IsInterface || t.IsAbstract)
                    continue;
                if (typeof(T).IsAssignableFrom(t))
                {
                    container.RegisterType(typeof(T),t);  
                    var possibleNames = _mapping(t);
                    foreach(var name in possibleNames)
                    {
                        _typeMapping.Add(name, t);
                    }
                }
                
            }
         }

        public List<Type> GetAllTypes()
        {
            var result = new List<Type>();
            foreach (var entry in _typeMapping)
            {
                result.Add(entry.Value);
            }
            return result.Distinct().ToList();
        }

        public List<string> GetTypeMapping(Type t)
        {

            if (!_typeMapping.ContainsKey(_mapping(t)[0]))
                throw new Exception("Type is not in the factory");
            return _mapping(t);
        }
        
     



        public T Resolve(string name)
        {
           
                if (_typeMapping.ContainsKey(name))
                {
                    return (T) _container.Resolve(_typeMapping[name]);
                }
            

            throw new Exception(String.Format("Could not resolve type for id {0}", name));
        }

        public Type GetType(string typeName)
        {
            return _typeMapping[typeName];
        }
    }
}
