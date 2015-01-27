using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.SharpDXExtensions;
using SharpDX;
namespace MDXEngine.Painters
{
    public class CyclicColorizer : IPainter<Color2D>
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

        public void Write(IArray<Color2D> vV)
        {
            int k=0;
            for (int i=0;i<vV.Length;i++)
            {
                Color2D elem = vV[i];
                elem.Color = _colors[k++].ToVector4();
                vV[i] = elem;
                if (k == _colors.Length)
                    k = 0;
            }
        }


    }
}
