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
                g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(0, 0, result.Width-1, result.Height-1));

            }
            return result;

            
        }


        public void RandomRun(BinPackRandomRun model)
        {
            var bitmap = CreateRandomBitmap(new Interval(model.minWidth,model.maxWidth), new Interval(model.minHeight,model.maxHeight));
            bitmap.Save("./randomBit.png");

        }


    }
}
