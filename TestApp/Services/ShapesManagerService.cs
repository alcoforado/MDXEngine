using MDXEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Utilities
{
    public class ShapesManager
    {
        IDxViewControl _dxControl;
        static int i = 0;

        Dictionary<string, Type> _topologyTypeMapping;
        Dictionary<string, RenderTypeInfo> _renderTypeMapping;



        public ShapesManager(IDxViewControl dxControl)
        {
            _dxControl = dxControl;

        }

        public ShapesManager RegisterTopology<T>(string name) where T: ITopology
        {
            _topologyTypeMapping[name] = typeof(T);
            return this;
        }

        
       public ShapesManager RegisterRender<RenderType,ShaderType>(string name) where ShaderType: IShader
        {
            var info = new RenderTypeInfo
            {
                RenderType = typeof(RenderType),
                ShaderType = typeof(ShaderType)
            };
            _renderTypeMapping[name] = info;
           return this;

        }

     
        

    }
}
