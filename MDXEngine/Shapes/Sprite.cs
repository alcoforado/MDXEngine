using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;
using SharpDX;
using MDXEngine.DrawTree;
using MDXEngine.Interfaces;

namespace MDXEngine.Shapes
{
    public class Sprite : IShape<VerticeTexture2D>
    {

        protected Vector2 _p;
        protected float _height, _width;
        protected TextureRegion _texture;
        protected Bitmap _bitmap;
        protected string _atlasId;
        protected string _slotName;
        public Sprite(Vector2 BL, float width, float height, Bitmap texture, string slotName,string atlasId=null)
        {
            _p = BL;
            _width = width;
            _height = height;
            _bitmap = texture;
            _atlasId = atlasId;
            _slotName = slotName;
        }
        
        public void Draw(IDrawContext<VerticeTexture2D> context)
        {
            var vV = context.Vertices;
            var vI = context.Indices;
            vV[0] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X, _p.Y),
                TEX = _texture.BL
            };
            vV[1]=new VerticeTexture2D(){
                Position2D = new Vector2(_p.X+_width,_p.Y), 
                TEX= _texture.BR
            };
            vV[2]=new VerticeTexture2D(){
                Position2D = new Vector2(_p.X+_width,_p.Y+_height), 
                TEX= _texture.UR
            };
            vV[3]=new VerticeTexture2D(){
                Position2D = new Vector2(_p.X,_p.Y+_height), 
                TEX= _texture.UL
            };

            vI.CopyFrom(new int[]{0,1,3,1,2,3});

        }

        public int NVertices()
        {
            return 4;
        }

        public int NIndices()
        {
            return 6;
        }

        public TopologyType GetTopology()
        {
            return TopologyType.TRIANGLES;

        }

        public void RequestSlotResources(ISlotResourceAllocator provider)
        {
            provider.RequestTexture(_slotName, _bitmap);
        }

         
    }
}
