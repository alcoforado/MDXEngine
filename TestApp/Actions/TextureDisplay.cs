using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Shapes;
using MDXEngine.Textures;
using SharpDX;
using MDXEngine;

namespace TestApp.Actions
{
    public class TextureDisplay : IActionMenu
    {
        public TextureDisplay(DxControl control)
        {
            var dx = control.GetDxContext();
            var shaderTexture = new ShaderTexture2D(dx);
            control.AddShader(shaderTexture);
            var file = Utilities.TextureSelect();
            
            if (String.IsNullOrEmpty(file))
                return;

            var texture = new Texture(dx,file);

            var shape = new Sprite(new Vector2(-1f, -1f), 2.0f, 2.0f, new TextureRegion(texture));

            shaderTexture.Add(shape,texture);

        }

        public void Dispose()
        {
            
        }
    }
}
