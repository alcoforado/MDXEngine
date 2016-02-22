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

        /*
       public ShapesManager RegisterRender<PainterType,ShaderType>(string name) where T: IPainter<U>
        {
            var info = new RenderTypeInfo
            {
                RenderType = typeof(T),
                ShaderType = typeof(U)
            };
            _renderTypeMapping[name] = info;
           return this;

        }

        public ShapesManager RegisterShader<T>(IShader shader)
       {

       }
        */

    }
}
