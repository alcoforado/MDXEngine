using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Textures.BinPack
{
    public class BinPackNode 
    {
        public Rectangle Region {get; set;}
        public Bitmap Bitmap {get; set;}
        public List<BinPackNode> Childs { get; set; }
        
        public bool IsFilled()
        {
            return Bitmap != null;
        }

        public bool IsChildless()
        {
            return Childs.Count == 0;
        }


        public BinPackNode(Rectangle rect, Bitmap bitmap=null)
        {
            this.Bitmap = bitmap;
            this.Region = rect;
            Childs = new List<BinPackNode>();
            Debug.Assert(this.canFit(bitmap));
        }



        public bool canFit(Bitmap bitmap)
        {
            return Region.Width >= bitmap.Width && Region.Height >= bitmap.Height;
        }

        public void HorizontalDecompose(Bitmap bitmap)
        {
            Debug.Assert(this.canFit(bitmap));

            

        }
        
   
    }
}
