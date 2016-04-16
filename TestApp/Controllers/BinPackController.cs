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
using MDXEngine;
using MDXEngine.Textures.BinPack;
using TestApp.Actions;
using MDXEngine.Textures;
using MDXEngine.Shapes;
using SharpDX;

namespace TestApp.Controllers
{
    public class BinPackController : IController
    {
        IDxViewControl _dx;
        IAppStateProvider _appStateProvider;
        public BinPackController(IDxViewControl dx,IAppStateProvider appState)
        {
            _dx = dx;
            _appStateProvider = appState;
        }

        private Bitmap CreateRandomBitmap(Interval Width, Interval Height)
        {
            var result = new Bitmap(Width.RandomInt(), Height.RandomInt(), PixelFormat.Format32bppArgb);
            var color = ColorExtension.RandomColor().ToSystemColor();
            using (var g = Graphics.FromImage(result))
            {
                g.FillRectangle(new SolidBrush(color), new System.Drawing.Rectangle(0, 0, result.Width, result.Height));
                g.DrawRectangle(new Pen(new SolidBrush(System.Drawing.Color.White)), new System.Drawing.Rectangle(0, 0, result.Width - 1, result.Height - 1));

            }
            return result;


        }


        private List<Bitmap> CreateRandomBitmapList(Interval Width, Interval Height, int numElements)
        {
            var result = new List<Bitmap>();
            for (int i = 0; i < numElements; i++)
            {
                result.Add(CreateRandomBitmap(Width, Height));
            }
            return result;

        }


        public double RandomRun(BinPackRandomRun model)
        {
            var list = CreateRandomBitmapList(
                new Interval(model.minWidth, model.maxWidth),
                new Interval(model.minHeight, model.maxHeight),
                model.NumElements);
            var binPack = new BinPackAlghorithm(list);
            var state = _appStateProvider.GetAppState<BinPackAppState>();
            
            if (state.Text != null )
            {
                state.Text.Dispose();
                state.Text = null;
            }

            var bp = binPack.CreateBitmapWithWireframe();
            bp.Save("Test.png");
           var shader = _dx.ResolveShader<ShaderTexture2D>();
            shader.RemoveAll();
            
            var shape = new Sprite(new Vector2(-1f, -1f), 2.0f, 2.0f, bp,ShaderTexture2D.TextureSlotName);
            shader.Add(shape);
            bp.Dispose();
            
            return binPack.Efficiency();

        }


    }
}
