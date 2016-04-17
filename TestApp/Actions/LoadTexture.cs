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
    public class LoadTexture : IAppState
    {
        public LoadTexture(DxControl control)
        {
            var dx = control.GetDxContext();
            var shaderTexture = new ShaderTexture2D(dx);
            control.AddShader(shaderTexture);
            var file = Utils.TextureSelect();
            
            if (String.IsNullOrEmpty(file))
                return;

            var texture = GDIBitmap.LoadFromFile(file);

            var shape = new Sprite(new Vector2(-1f, -1f), 2.0f, 2.0f, texture, ShaderTexture2D.TextureSlotName);

            shaderTexture.Add(shape);
            texture.Dispose();
        }

        public void Dispose()
        {
            
        }
    }
}
