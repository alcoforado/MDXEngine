using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.SharpDXExtensions
{
    public static class ColorExtension
    {
        public static System.Drawing.Color ToSystemColor(this SharpDX.Color cl)
        {
            return System.Drawing.Color.FromArgb(cl.A,cl.R,cl.G,cl.B);
        }

        public static System.Drawing.Color ToSystemColor(this SharpDX.Color4 cl)
        {
            
            return System.Drawing.Color.FromArgb(
                (int) (cl.Alpha*255f), 
                (int) (cl.Red*255f), 
                (int) (cl.Green*255f), 
                (int) (cl.Blue*255f));
        }
    }
}
