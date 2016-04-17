using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Shapes;
using MDXEngine.Textures;
using SharpDX;
using MDXEngine;
using MDXEngine.ShaderResources.Textures.BinPack;

namespace TestApp.Actions
{
    public class TextureDisplay : IAppState
    {
        public TextureDisplay(DxControl control)
        {
            var dx = control.GetDxContext();
            var shaderTexture = new ShaderTexture2D(dx);
            control.AddShader(shaderTexture);
            var file = Utils.TextureSelect();
            
            if (String.IsNullOrEmpty(file))
                return;

            var texture = (Bitmap) Image.FromFile(file);

            var shape = new Sprite(new Vector2(-1f, -1f), 2.0f, 2.0f, new GDIBitmap(texture), ShaderTexture2D.TextureSlotName);

            shaderTexture.Add(shape);

        }

        public void Dispose()
        {
            
        }
    }
}
