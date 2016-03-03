using MDXEngine.DrawTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDXEngine
{
    /// <summary>
    /// Painters are classes that are concern to data used by the shader to render a topology.
    /// These data is supposed to be loaded in the shader via vertex based data or resources slots.
    /// Although topologies can basically be used by all shaders, the painters are only restricted to 
    /// very similar shaders since the painter is kind of aware of the meaning of the vertice data and the 
    /// slots used by the shader. It is intimaly related to a shader. A developer cannot create a painter without
    /// be ablo to understand the shader alghorithm. Generally every shader comes with their own set of painters.
    /// </summary>
    /// <typeparam name="T"> The Vertice Data type</typeparam>
    public interface IPainter<T>
    {
        void Write(IArray<T> vV, IArray<int> vI, TopologyType topologyType);
        
       /// <summary>
       /// Get the Load Resources commands. If the painter don't define any ShaderResources to be loaded,
       /// just return null or empty list;
       /// </summary>
       /// <returns></returns>
        List<ResourceLoadCommand> GetLoadResourcesCommands();
    }
}
