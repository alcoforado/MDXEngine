using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.SharpDXExtensions;
using SharpDX;
using MDXEngine.DrawTree;
using MDXEngine.Interfaces;

namespace MDXEngine.Painters
{
    public class CyclicColorizer : IPainter<VerticeColor>
    {
        Color[] _colors;
        
        public CyclicColorizer(Color[] colors)
        {
            _colors = colors;
        }

        public CyclicColorizer(Color color)
        {
            _colors = new Color[]{color};
        }

        public void Draw(IDrawContext<VerticeColor> context)
        {
            var vV = context.Vertices;
            int k=0;
            for (int i=0;i<vV.Length;i++)
            {
                VerticeColor elem = vV[i];
                elem.Color = _colors[k++].ToVector4();
                vV[i] = elem;
                if (k == _colors.Length)
                    k = 0;
            }
        }

        public void RequestSlotResources(ISlotResourceAllocator provider)
        {
        }


    }
}
