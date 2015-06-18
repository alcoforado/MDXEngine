using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;
using SharpDX;

namespace MDXEngine.Shapes
{
    public class AnchorSprite : IShape<VerticeTexture2D>
    {

        private Vector2 _p;
        private float _height, _width;
        private TextureRegion _texture;


        public AnchorSprite(Vector2 BL, float width, float height, TextureRegion texture)
        {
            _p = BL;
            _width = width;
            _height = height;
            _texture = texture;
        }


        public void Write(IArray<VerticeTexture2D> vV, IArray<int> vI)
        {
            vV[0] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X, _p.Y),
                TEX = _texture.BL
            };
            vV[1] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X + _width, _p.Y),
                TEX = _texture.BR
            };
            vV[2] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X + _width, _p.Y + _height),
                TEX = _texture.UR
            };
            vV[3] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X, _p.Y + _height),
                TEX = _texture.UL
            };

            vI.CopyFrom(new int[] { 0, 1, 3, 1, 2, 3 });

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
    }
}
