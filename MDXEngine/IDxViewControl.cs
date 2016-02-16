using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public interface IDxViewControl
    {
        IDxContext GetDxContext();
        T CreateShader<T>() where T : IShader;
        void AddShader(IShader shader);
        /// <summary>
        /// Return the shader if it is already created, or create one
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ResolveShader<T>() where T : IShader; 
        
    }
}
