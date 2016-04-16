using MDXEngine;
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
    public class HelloWorld : IAppState
    {
        public HelloWorld(DxControl control)
        {
            var dx = control.GetDxContext();
            var shaderTexture = new ShaderTexture2D(dx);
            control.AddShader(shaderTexture);
            var textRender = new GDITextRendering();
            var texture = textRender.RenderText("Hello World",new Font(),new TextWriteOptions()
            {
                color = SharpDX.Color.Red,
                font_size = 25,
            });
            var shape = new Sprite(new Vector2(-1f, -1f), 2.0f, 2.0f, new TextureRegion(texture));
            shaderTexture.Add(shape, texture);

        
        }
        public void Dispose() { }
    }
}
