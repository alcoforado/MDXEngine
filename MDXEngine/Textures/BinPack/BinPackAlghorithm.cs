using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.DrawingExtensions;
using System.Diagnostics;
namespace MDXEngine.Textures.BinPack
{
    public class BinPackAlghorithm
    {
        BinPackNode _root;
        Size UsedDims { get; set; }

        const int MAX_SIZE = 1000000000; //An maximum upperbound for the size of the binpack region




        private BinPackAnalysis findBestNodeToInsert(BinPackNode node, int width, int height)
        {
            if (node.canFit(width, height))
            {
                if (node.IsFree())
                {
                    return new BinPackAnalysis()
                    {
                        node = node,
                        used_dims = new Size(Math.Max(node.Region.X + width, this.UsedDims.Width), Math.Max(node.Region.Y + height, this.UsedDims.Height)),
                        HorizontalExpansion = node.Region.X + width > this.UsedDims.Width,
                        VerticalExpansion = node.Region.Y + height > this.UsedDims.Height,
                    };
                }
                else
                {
                    Debug.Assert(node.IsDecomposed());
                    var analysis1 = findBestNodeToInsert(node.Childs[1], width, height);
                    var analysis2 = findBestNodeToInsert(node.Childs[2], width, height);
                    return getBestAnalysis(analysis1, analysis2);
                }
            }
            else
                return null;
        }

        private bool NodeIsExpandableToBottom(BinPackNode node)
        {
            return node.Region.Bottom == MAX_SIZE;
        }

        private bool NodeIsExpandableToRight(BinPackNode node)
        {
            return node.Region.Right == MAX_SIZE;
        }

        private BinPackAnalysis getBestAnalysis(BinPackAnalysis analysis1, BinPackAnalysis analysis2)
        {
            throw new NotImplementedException();
        }


        public BinPackAlghorithm(List<Bitmap> lst)
        {
            _root = new BinPackNode(new Rectangle(0, 0, MAX_SIZE, MAX_SIZE));
            UsedDims = new Size(0, 0);

            for(int i=0;i<lst.Count;i++)
            {
                var bitmap = lst[i];
                var analysis = findBestNodeToInsert(_root, bitmap.Width,bitmap.Height);
                var node = analysis.node;
                if (NodeIsExpandableToBottom(node))
                {
                    node.VerticalDecompose(bitmap);
                }
                else if (NodeIsExpandableToRight(node))
                {
                    node.HorizontalDecompose(bitmap);
                }
                else //We have to decide horizontal or vertical decomposition.
                {
                    var regions = new List<Rectangle>();
                    var vr = node.GetVerticalDecomposeRegions(bitmap.Width,bitmap.Height);
                    var hr = node.GetHorizontalDecomposeRegions(bitmap.Width, bitmap.Height);
                    regions.AddRange(vr);
                    regions.AddRange(hr);
                    var v = CountBitmapsThatFitsIntoRegion(regions, lst, i + 1);


                    
                }
            }

        }

        class FitAnalysisResult
        {
            public int totalMatches { get; set; }
            public int greaterThan85Percent { get; set; }

            public FitAnalysisResult()
            {
                totalMatches = 0;
                greaterThan85Percent = 0;
            }

        }

      

       private FitAnalysisResult[] CountBitmapsThatFitsIntoRegion(List<Rectangle> regions, List<Bitmap> lst, int iStart)
        {
            var v = new FitAnalysisResult[regions.Count];
            for (int j = 0; j < regions.Count; j++)
            {
                for (int k = iStart; k < lst.Count; k++)
                {
                    if (regions[j].canFit(lst[k].Width, lst[k].Height))
                    {
                        v[j].totalMatches++;
                    }
                    if (regions[j].areaFilledBy(lst[k].Width,lst[k].Height)>=0.85)
                    {
                        v[j].greaterThan85Percent += 1;
                    }
                }
            }
            return v;
        }

    }
}
