using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawingExtensions
{
    static public class RectangleExtension
    {

        static public int Area(this Rectangle rect)
        {
            return rect.Width * rect.Height;
        }
        static public bool canFit(this Rectangle rect,int width,int height)
        {
            return rect.Width  >= width &&  rect.Height>= height;
        }
        
        static public double areaFilledBy(this Rectangle rect,int width,int height)
        {
            return (double)(width * height) / (double)rect.Area();

        }
        
    }
}
