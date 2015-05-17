using MDXEngine;
using MDXEngine.Shaders;
using MDXEngine.Shapes;
using MDXEngine.Textures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Actions
{
    public class HelloWorld : IApp
    {
        public HelloWorld(IDxContext dx)
        {
            var shaderTexture = new ShaderTexture2D(dx);
            dx.AddShader(shaderTexture);
            
            using (var bitmap = new Bitmap(300, 300, PixelFormat.Format32bppArgb))
            {
                using(var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(
                        new SolidBrush(System.Drawing.Color.Red),
                        new System.Drawing.Rectangle(0,0,bitmap.Width,bitmap.Height)
                        );
                    graphics.Flush();
                    bitmap.Save("hello.png");
                    var texture = new Texture(dx, bitmap);
                    var shape = new Sprite(new Vector2(-1f, -1f), 2.0f, 2.0f, new TextureRegion(texture));
                    shaderTexture.Add(shape, texture);
                }
            }
        }
        public void Dispose() { }
    }
}
