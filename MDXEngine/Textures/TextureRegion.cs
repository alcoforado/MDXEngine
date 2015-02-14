using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine.Textures
{
    public class TextureRegion
    {
        ITexture _texture;
        public Vector2 P { get; set; }
        public Vector2 Q { get; set; }
        public Vector2 Dimensions { get; set; }


        public TextureRegion(ITexture texture,Vector2 P,Vector2 Q)
        {
            this.P = P;
            this.Q = Q;
            _texture = texture;
            Dimensions = P - Q;
        }



    }
}
