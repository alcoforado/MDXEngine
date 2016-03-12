using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine.Painters
{
    public class ColorGenerator
    {
        public static List<Color> CreateLinearGradient(Color start,Color end,int nColors)
        {
            var result = new List<Color>();
            if (nColors == 0)
                return result;
            result.Add(start);

            if (nColors == 1)
                return result;
                
            int itSize = nColors - 2;
            float step = 1.0f / (float)(nColors-1);
            float s = 0.0f;
            for (int i = 0; i < itSize;i++)
            {
                s += step;
                var color = new Color(
                    (1.0f - s) * ((float)start.R) + s*  ((float)end.R),
                    (1.0f - s) * ((float)start.G) + s * ((float)end.G),
                    (1.0f - s) * ((float)start.B) + s * ((float)end.B),
                    (1.0f - s) * ((float)start.A) + s * ((float)end.A));
                result.Add(color);
            }
            result.Add(end);

            return result;
        }

        public static List<Color4> CreateLinearGradient(Color4 start, Color4 end, int nColors)
        {
            var result = new List<Color4>();
            if (nColors == 0)
                return result;
            result.Add(start);

            if (nColors == 1)
                return result;

            int itSize = nColors - 2;
            float step = 1.0f / (float)(nColors - 1);
            float s = 0.0f;
            for (int i = 0; i < itSize; i++)
            {
                s += step;
                var color = new Color4(
                    (1.0f - s) * ((float)start.Red) + s * ((float)end.Red),
                    (1.0f - s) * ((float)start.Green) + s * ((float)end.Green),
                    (1.0f - s) * ((float)start.Blue) + s * ((float)end.Blue),
                    (1.0f - s) * ((float)start.Alpha) + s * ((float)end.Alpha));
                result.Add(color);
            }
            result.Add(end);

            return result;
        }



    }
}
