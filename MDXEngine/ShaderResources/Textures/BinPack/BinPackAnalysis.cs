using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.DrawingExtensions;
namespace MDXEngine.Textures.BinPack
{
    enum Decomposition { Horizontal, Vertical };
    internal class BinPackAnalysis
    {
        public BinPackNode node { get; set; }
        public Size used_dims { get; set; }
        public bool HorizontalExpansion { get; set; }
        public bool VerticalExpansion { get; set; }
        public bool NodeHasMaximumRight { get; set; }
        public bool NodeHasMaximumBottom { get; set; }
        public DecompositionAnalysis Horizontal { get; set; }
        public DecompositionAnalysis Vertical { get; set; }
        public Decomposition SuggestedDecompositionType { get; set; }
    }

    internal class DecompositionAnalysis
    {
        public FitAnalysisResult RightRegion { get; set; }
        public FitAnalysisResult BottomRegion { get; set; }
        public long Score { get; set; }
        public Decomposition DecompositionType { get; set; }
        private long GetDecompositionScore(long pts)
        {
            long acum = 0;
            long pts2 = pts * pts;
            long pts3 = pts2 * pts;
            long pts4 = pts3 * pts;
            long pts5 = pts4 * pts;
            if (RightRegion.TotalMatches > 0 && BottomRegion.TotalMatches > 0)
            {
                acum = pts2 * (Math.Min(RightRegion.TotalMatches, BottomRegion.TotalMatches) + (BottomRegion.GoldenMatch + RightRegion.GoldenMatch)) +
                       Math.Max(RightRegion.TotalMatches, BottomRegion.TotalMatches) * pts;
            }
            else
            {
                acum = Math.Max(RightRegion.TotalMatches, BottomRegion.TotalMatches) + 4 * (BottomRegion.GoldenMatch + RightRegion.GoldenMatch) * pts;
            }
            if (RightRegion.GoldenMatch > 0 && BottomRegion.GoldenMatch > 0)
            {
                acum += Math.Min(RightRegion.GoldenMatch, BottomRegion.GoldenMatch) * pts5 + (RightRegion.GoldenMatch + BottomRegion.GoldenMatch) * pts4;
            }
            return acum;


        }

        public DecompositionAnalysis(BinPackNode node, Decomposition type, int width, int height, List<Bitmap> lst, int iStart)
        {
            List<Rectangle> v;

            if (type == Decomposition.Vertical)
                v = node.GetVerticalDecomposeRegions(width, height);
            else
                v = node.GetHorizontalDecomposeRegions(width, height);
            this.DecompositionType = type;
            this.RightRegion = new FitAnalysisResult(v[1], lst, iStart);
            this.BottomRegion = new FitAnalysisResult(v[2], lst, iStart);
            this.Score = GetDecompositionScore(lst.Count * 4);
        }

    }

    internal class FitAnalysisResult
    {
        public Rectangle Region { get; set; }
        public int TotalMatches { get; set; }
        public int GoldenMatch { get; set; }

        public FitAnalysisResult(Rectangle region, List<Bitmap> lst, int iStart)
        {
            TotalMatches = 0;
            GoldenMatch = 0;

            for (int k = iStart; k < lst.Count; k++)
            {
                if (Region.canFit(lst[k].Width, lst[k].Height))
                {
                    this.TotalMatches++;
                }
                if (Region.areaFilledBy(lst[k].Width, lst[k].Height) >= 0.85)
                {
                    this.GoldenMatch++;
                }
            }
        }

    }


}



