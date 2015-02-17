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


        public Vector2 UL { get; private set; }
        public Vector2 BR { get; private set; }
        public Vector2 UR {get {return new Vector2(BR.X,UL.Y);}}
        public Vector2 BL {get {return new Vector2(UL.X,BR.Y);}}

        public Vector2 Dimensions { get; set; }

         

        public TextureRegion(ITexture texture,Vector2 p,Vector2 q)
        {
            this.UL = new Vector2(Math.Min(p.X,q.X),Math.Min(p.Y,q.Y));
            this.BR = new Vector2(Math.Max(p.X,q.X),Math.Max(p.Y,q.Y));
            _texture = texture;
            Dimensions = BR - UL;
        }



    }
}
