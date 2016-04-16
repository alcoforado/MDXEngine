﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.DrawingExtensions;
using System.Diagnostics;
using MDXEngine.Interfaces;
using MDXEngine.MMath;
using MDXEngine.ShaderResources.Textures;
using MDXEngine.ShaderResources.Textures.BinPack;

namespace MDXEngine.Textures.BinPack
{
    public class BinPackAlghorithm
    {
        BinPackNode _root;
        Size UsedDims { get; set; }

        const int MAX_SIZE = 1000000000; //An maximum upperbound for the size of the binpack region




        private BinPackAnalysis findBestNodeToInsert(BinPackNode node, int width, int height, List<IBitmap> lst, int iStart)
        {
            if (node.canFit(width, height))
            {
                if (node.IsFree())
                {
                    var analysis = new BinPackAnalysis()
                    {
                        node = node,
                        used_dims = new Size(Math.Max(node.Region.X + width, this.UsedDims.Width), Math.Max(node.Region.Y + height, this.UsedDims.Height)),
                        HorizontalExpansion = node.Region.X + width > this.UsedDims.Width,
                        VerticalExpansion = node.Region.Y + height > this.UsedDims.Height,
                        NodeHasMaximumRight = NodeHasMaximumRight(node),
                        NodeHasMaximumBottom = NodeHasMaximumBottom(node)
                    };
                    /*
                    if (analysis.NodeHasMaximumBottom)
                    {
                        analysis.SuggestedDecompositionType = Decomposition.Vertical;
                        analysis.Vertical = new DecompositionAnalysis(node, Decomposition.Vertical, width, height, lst, iStart);
                    }
                    else if (analysis.NodeHasMaximumRight)
                    {
                        analysis.SuggestedDecompositionType = Decomposition.Horizontal;
                        analysis.Horizontal = new DecompositionAnalysis(node, Decomposition.Horizontal, width, height, lst, iStart);
                    }
                    else
                     */
                    {
                        analysis.Vertical = new DecompositionAnalysis(node, Decomposition.Vertical, width, height, lst, iStart);
                        analysis.Horizontal = new DecompositionAnalysis(node, Decomposition.Horizontal, width, height, lst, iStart);

                        if (analysis.Vertical.Score >= analysis.Horizontal.Score)
                        {
                            analysis.SuggestedDecompositionType = Decomposition.Vertical;
                        }
                        else
                        {
                            analysis.SuggestedDecompositionType = Decomposition.Horizontal;
                        }

                    }
                    return analysis;

                }
                else
                {
                    Debug.Assert(node.IsDecomposed());
                    var analysis1 = findBestNodeToInsert(node.Childs[1], width, height, lst, iStart);
                    var analysis2 = findBestNodeToInsert(node.Childs[2], width, height, lst, iStart);
                    return getBestAnalysis(analysis1, analysis2);
                }
            }
            else
                return null;
        }

        private bool NodeHasMaximumBottom(BinPackNode node)
        {
            return node.Region.Bottom == MAX_SIZE;
        }

        private bool NodeHasMaximumRight(BinPackNode node)
        {
            return node.Region.Right == MAX_SIZE;
        }


        private HScore getAnalysisScore(BinPackAnalysis analysis)
        {
            var result = new HScore();

            if (analysis.HorizontalExpansion || analysis.VerticalExpansion) 
            {
                result
                    .AppendScore(1)
                    .AppendScore(-Math.Abs(analysis.used_dims.Height - analysis.used_dims.Width));
            }
            else
            {

                result.AppendScore(3).AppendScore(analysis.SuggestedDecompositionType == Decomposition.Horizontal ? analysis.Horizontal.Score : analysis.Vertical.Score);
            }
            return result;
        }

        private BinPackAnalysis getBestAnalysis(BinPackAnalysis analysis1, BinPackAnalysis analysis2)
        {
            if (analysis1 == null)
                return analysis2;
            if (analysis2 == null)
                return analysis1;

            return getAnalysisScore(analysis1) >= getAnalysisScore(analysis2) ? analysis1 : analysis2;

        }


        public int UsedArea()
        {
            int area = 0;
            Action<BinPackNode> f = null;
            f = (BinPackNode node) =>
                {
                    if (node.IsFilled())
                        area += node.Region.Area();
                    else if (!node.IsChildless())
                    {
                        foreach(var child in node.Childs)
                        {
                            f(child);
                        }
                    }
                };
            f(_root);
            return area;
        }

        public double Efficiency()
        {
            return ((double) this.UsedArea())/(this.UsedDims.Height*this.UsedDims.Width);
        }

        public BinPackAlghorithm(List<IBitmap> lstBp)
        {
            _root = new BinPackNode(new Rectangle(0, 0, MAX_SIZE, MAX_SIZE));
            UsedDims = new Size(0, 0);
            var lst = (from element in lstBp
                      orderby element.Height descending
                      select element).ToList();

            for (int i = 0; i < lst.Count; i++)
            {
                var bitmap = lst[i];
                var analysis = this.findBestNodeToInsert(_root, bitmap.Width, bitmap.Height, lst, i + 1);
                var node = analysis.node;

                if (analysis.SuggestedDecompositionType == Decomposition.Horizontal)
                {
                    UsedDims = analysis.used_dims;
                    node.HorizontalDecompose(bitmap);
                }
                else if (analysis.SuggestedDecompositionType == Decomposition.Vertical)
                {
                    UsedDims = analysis.used_dims;
                    node.VerticalDecompose(bitmap);
                }
            }
        }

        /// <summary>
        /// Used for debugging mainly
        /// </summary>
        /// <returns></returns>
        public IBitmap CreateBitmapWithWireframe()
        {
            var result = new GDIBitmap(this.UsedDims.Width, this.UsedDims.Height);
            using (var painter = new BitmapPainter(result))
            {


                Action<BinPackNode> aux = null;
                aux = (BinPackNode node) =>
                {
                    if (node == null)
                        return;
                    if (node.IsFilled())
                    {
                        System.Diagnostics.Debug.Assert(node.IsChildless());
                        painter.DrawImage(node.Bitmap, node.Region.Location);
                        painter.DrawRectangle(Color.White,
                            new Rectangle(node.Region.Location,
                                new Size(node.Region.Size.Width - 1, node.Region.Size.Height - 1)));
                    }
                    else
                    {
                        var width = Math.Min(node.Region.Width, UsedDims.Width - node.Region.Location.X);
                        var height = Math.Min(node.Region.Height, UsedDims.Height - node.Region.Location.Y);

                        var rect = new Rectangle(node.Region.Location, new Size(width - 1, height - 1));
                        if (rect.Size.Width >= 2 && rect.Size.Height >= 2)
                            painter.DrawRectangle(Color.White, rect);
                    }

                    if (!node.IsChildless())
                    {
                        foreach (var child in node.Childs)
                            aux(child);
                    }
                };

                aux(_root);
                painter.Dispose();
                return result;
            }

        }


        public IBitmap CreateBitmap()
             {
            var result = new GDIBitmap(this.UsedDims.Width, this.UsedDims.Height);
            using (var painter = new BitmapPainter(result))
            {


                Action<BinPackNode> aux = null;
                aux = (BinPackNode node) =>
                {
                    if (node == null)
                        return;
                    if (node.IsFilled())
                    {
                        System.Diagnostics.Debug.Assert(node.IsChildless());
                        painter.DrawImage(node.Bitmap, node.Region.Location);
                    }
                    else if (!node.IsChildless())
                    {
                        foreach (var child in node.Childs)
                            aux(child);
                    }
                };
                aux(_root);
                painter.Dispose();
                return result;
            }

        }
       
        public Dictionary<IBitmap, Rectangle> GetBitmapsRegions()
        {
            var result = new Dictionary<IBitmap, Rectangle>();

            Action<BinPackNode, Dictionary<IBitmap, Rectangle>> aux = null;
            aux = (BinPackNode node, Dictionary<IBitmap, Rectangle> regions) =>
            {
                if (node == null)
                    return;
                if (node.IsFilled())
                {
                    System.Diagnostics.Debug.Assert(node.IsChildless());
                    result.Add(node.Bitmap, node.Region);
                }
                else if (!node.IsChildless())
                {
                    foreach (var child in node.Childs)
                        aux(child, regions);
                }
            };

            aux(_root, result);
            return result;


        }




    }
}
