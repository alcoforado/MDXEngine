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
    public class TextureDisplay : IApp
    {
        public TextureDisplay(IDxContext dx)
        {
            var shaderTexture = new ShaderTexture2D(dx);
            dx.AddShader(shaderTexture);
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
