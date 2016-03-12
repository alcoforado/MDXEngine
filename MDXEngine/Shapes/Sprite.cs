using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;
using SharpDX;
using MDXEngine.DrawTree;

namespace MDXEngine.Shapes
{
    public class Sprite : IShape<VerticeTexture2D>
    {

        protected Vector2 _p;
        protected float _height, _width;
        protected TextureRegion _texture;


        public Sprite(Vector2 BL, float width, float height, TextureRegion texture)
        {
            _p = BL;
            _width = width;
            _height = height;
            _texture = texture;
        }
        
        public void Write(SubArray<VerticeTexture2D> vV, IArray<int> vI)
        {
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

        public List<SlotData> GetResourcesLoadCommands()
        {

            return new List<SlotData>();
        }
         
    }
}
