using MDXEngine.MMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;
using MDXEngine.SharpDXExtensions;
namespace TestApp.Controllers
{
    public class BinPackController : IController
    {
        
        private Bitmap CreateRandomBitmap(Interval Width,Interval Height)
        {
            var result = new Bitmap(Width.RandomInt(),Height.RandomInt(),PixelFormat.Format32bppArgb);
            var color = ColorExtension.RandomColor().ToSystemColor();
            using (var g = Graphics.FromImage(result))
            {
                g.FillRectangle(new SolidBrush(color),new Rectangle(0,0,result.Width,result.Height));


            }
            return result;

            
        }


        public void RandomRun(BinPackRandomRun model)
        {
            var bitmap = CreateRandomBitmap(model.WidthRange, model.HeightRange);
            bitmap.Save("./randomBit.png");

        }


    }
}
