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

        public delegate List<string> TypeMap(string t);
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
                    _typeMapping.Add(t.Name.ToLower(), t);
                }
                
            }

        }


        public T Resolve(string name)
        {
            var possibleTypes = _mapping(name);
            foreach (var typeName in possibleTypes)
            {
                if (_typeMapping.ContainsKey(typeName.ToLower()))
                {
                    return (T) _container.Resolve(_typeMapping[typeName.ToLower()]);
                }
            }

            throw new Exception(String.Format("Could not resolve type for id {0}\nCandidates are {1}", name, string.Join(",", possibleTypes.ToArray())));
        }


    }
}
