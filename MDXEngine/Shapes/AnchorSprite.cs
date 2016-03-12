using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;
using SharpDX;
using System.Drawing;
using MDXEngine.SharpDXExtensions;
using MDXEngine.DrawTree;
namespace MDXEngine.Shapes
{
    public class AnchorSprite : IShape<VerticeTexture2D>, ICameraObserver 
    {
        private Vector2 _p; //final point;
        private Vector2 _d; //displacement
        private Vector3 _o; //origin
        private double _height, _width;
        private TextureRegion _texture;
       

        public AnchorSprite(Vector3 origin, Vector2 displacement,  Texture texture,Camera cam, Size screenSize)
        {
            _d = displacement;
            _o = origin;
            _height = (2.0* (double)  texture.Height/(double) screenSize.Height);
            _width =  (2.0* (double) texture.Width  /(double) screenSize.Width);

            _height = Math.Min(_height, 2.0);
            _width = Math.Min(_width, 2.0);
            cam.AddObserver(this);
            _texture = new TextureRegion(texture);
        }

        public void CameraChanged(Camera cam)
        {
            var projectedPoint =cam.ProjectPoint(_o);
            _p=projectedPoint.XY();
            _p += _d;
        }

        public void Write(SubArray<VerticeTexture2D> vV, IArray<int> vI)
        {
            float width  = (float)_width;
            float height = (float)_height;
            vV[0] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X, _p.Y),
                TEX = _texture.BL
            };
            vV[1] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X + width, _p.Y),
                TEX = _texture.BR
            };
            vV[2] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X + width, _p.Y + height),
                TEX = _texture.UR
            };
            vV[3] = new VerticeTexture2D()
            {
                Position2D = new Vector2(_p.X, _p.Y + height),
                TEX = _texture.UL
            };

            vI.CopyFrom(new int[] { 0, 1, 3, 1, 2, 3 });

        }

        public List<SlotData> GetResourcesLoadCommands()
        {
            return new List<SlotData>();
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
