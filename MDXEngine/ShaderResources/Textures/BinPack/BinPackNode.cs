using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.DrawingExtensions;
using MDXEngine.Interfaces;

namespace MDXEngine.Textures.BinPack
{
    public class BinPackNode 
    {
        public Rectangle Region {get; set;}
        public IBitmap Bitmap {get; set;}
        public List<BinPackNode> Childs { get; set; }
        
        public bool IsFilled()
        {
            return Bitmap != null;
        }

        public bool IsChildless()
        {
            return Childs.Count == 0;
        }

        public bool IsFree()
        {
            return !this.IsFilled() && this.IsChildless();
        }

        public BinPackNode(Rectangle rect, IBitmap bitmap=null)
        {
            this.Bitmap = bitmap;
            this.Region = rect;
            Childs = new List<BinPackNode>();
            if (bitmap != null)
                Debug.Assert(this.canFit(bitmap));
        }

        public bool IsDecomposed()
        {
            return this.Childs.Count == 3;
        }

        public bool canFit(IBitmap bitmap)
        {
            return canFit(bitmap.Width,bitmap.Height);
        }

        public bool canFit(int width,int height)
        {
            return Region.Width >= width && Region.Height >= height;
        }


        public List<Rectangle> GetVerticalDecomposeRegions(int width,int height)
        {

            Debug.Assert(this.canFit(width, height));

            var rect1 = new Rectangle(this.Region.Location, new Size(width, height));
            var rect2 = new Rectangle(this.Region.Location.X + width, this.Region.Location.Y, this.Region.Width - width, height);
            var rect3 = new Rectangle(this.Region.Location.X, this.Region.Location.Y + height, this.Region.Width, this.Region.Height - height);
            return new List<Rectangle>() { rect1, rect2, rect3 };
        }
        

        public List<Rectangle> GetHorizontalDecomposeRegions(int width,int height)
        {

            Debug.Assert(this.canFit(width, height));

            var rect1 = new Rectangle(this.Region.Location, new Size(width, height));
            var rect2 = new Rectangle(this.Region.Location.X + width, this.Region.Location.Y, this.Region.Width - width, this.Region.Height);
            var rect3 = new Rectangle(this.Region.Location.X, this.Region.Location.Y + height, width, this.Region.Height - height);
            return new List<Rectangle>() { rect1, rect2, rect3 };
        }

        public void HorizontalDecompose(IBitmap bp)
        {
            Debug.Assert(IsChildless());
            var regions = this.GetHorizontalDecomposeRegions(bp);
            this.Childs.Add(new BinPackNode(regions[0], bp));
            this.Childs.Add(new BinPackNode(regions[1]));
            this.Childs.Add(new BinPackNode(regions[2]));
        }

        public void VerticalDecompose(IBitmap bp)
        {
            Debug.Assert(IsChildless());
            var regions = this.GetVerticalDecomposeRegions(bp);
            this.Childs.Add(new BinPackNode(regions[0], bp));
            this.Childs.Add(new BinPackNode(regions[1]));
            this.Childs.Add(new BinPackNode(regions[2]));
        }


        public List<Rectangle> GetVerticalDecomposeRegions(IBitmap bp)
        {
            return  this.GetVerticalDecomposeRegions(bp.Width, bp.Height);
        }

        public List<Rectangle> GetHorizontalDecomposeRegions(IBitmap bp)
        {
            return this.GetHorizontalDecomposeRegions(bp.Width, bp.Height);
        }

        
   
    }
}
