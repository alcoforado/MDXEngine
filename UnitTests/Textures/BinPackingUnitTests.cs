﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using MDXEngine.Textures.BinPack;
using System.Drawing;
using MDXEngine.DrawingExtensions;
using System.Collections.Generic;
namespace UnitTests.Textures
{
    [TestClass]
    public class BinPackingUnitTests
    {
        [TestMethod]
        public void BinPackNodeShouldAlwaysBeCreatedAsChildless()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 40, 40), new System.Drawing.Bitmap(40, 40));
            node.IsChildless().Should().BeTrue();
        }
        [TestMethod]
        public void BinPackNodeIsFilledShouldBeTrueIfConstructorReceivesABitmap()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 40, 40), new System.Drawing.Bitmap(40, 40));
            node.IsFilled().Should().BeTrue();
        }

        [TestMethod]
        public void BinPackNodeIsFilledShouldBeFalseIfConstructorReceivesNoBitmap()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 40, 40));
            node.IsFilled().Should().BeFalse();
        }

        [TestMethod]
        public void BinPackNodeCanFitShouldReturnCorrectResults()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 30, 50));
           
            
            node.canFit(new Bitmap(20, 20)).Should().BeTrue();
            node.canFit(new Bitmap(40, 40)).Should().BeFalse();
            node.canFit(new Bitmap(20, 60)).Should().BeFalse();
            node.canFit(new Bitmap(60, 87)).Should().BeFalse();

        
        }


        [TestMethod]
        public void BinPackNodeVerticalDecomposeShouldPreserveAreaOfTheOriginalNode()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 30, 80));

            var result = node.GetVerticalDecomposeRegions(new Bitmap(20, 40));

            node.Region.Area().Should().Be(result[0].Area()+result[1].Area() + result[2].Area());


        }

        [TestMethod]
        public void BinPackNodeVerticalDecomposeAllElementsShouldHaveCorrectSize()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 30, 80));

            var result = node.GetVerticalDecomposeRegions(new Bitmap(20, 40));

            result[0].Width.Should().Be(20);
            result[0].Height.Should().Be(40);

            result[1].Width.Should().Be(10);
            result[1].Height.Should().Be(40);
            
            result[2].Width.Should().Be(30);
            result[2].Height.Should().Be(40);
        }

        [TestMethod]
        public void BinPackNodeVerticalDecomposeWithDisplacedNodeAllElementsShouldHaveCorrectSize()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(20, 30, 30, 80));

            var result = node.GetVerticalDecomposeRegions(new Bitmap(20, 40));

            result[0].Width.Should().Be(20);
            result[0].Height.Should().Be(40);

            result[1].Width.Should().Be(10);
            result[1].Height.Should().Be(40);

            result[2].Width.Should().Be(30);
            result[2].Height.Should().Be(40);
        }

        [TestMethod]
        public void BinPackNodeVerticalDecomposeNodeAllElementsShouldHaveCorrectPosition()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 30, 80));

            var result = node.GetVerticalDecomposeRegions(new Bitmap(20, 40));

            result[0].X.Should().Be(0);
            result[0].Y.Should().Be(0);

            result[1].X.Should().Be(20);
            result[1].Y.Should().Be(0);

            result[2].X.Should().Be(0);
            result[2].Y.Should().Be(40);
        }

        
        [TestMethod]
        public void BinPackNodeVerticalDecomposeWithDisplacedNodeAllElementsShouldHaveCorrectPosition()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(20, 30, 30, 80));

            var result = node.GetVerticalDecomposeRegions(new Bitmap(20, 40));

            result[0].X.Should().Be(20);
            result[0].Y.Should().Be(30);

            result[1].X.Should().Be(40);
            result[1].Y.Should().Be(30);

            result[2].X.Should().Be(20);
            result[2].Y.Should().Be(70);
        }


        [TestMethod]
        public void BinPackNodeHorizonatlDecomposeShouldPreserveAreaOfTheOriginalNode()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 30, 80));

            var result = node.GetHorizontalDecomposeRegions(new Bitmap(20, 40));

            node.Region.Area().Should().Be(result[0].Area() + result[1].Area() + result[2].Area());
        }

        [TestMethod]
        public void BinPackNodeHorizontalDecomposeNodeAllElementsShouldHaveCorrectPosition()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 30, 90));

            var result = node.GetHorizontalDecomposeRegions(new Bitmap(20, 40));

            result[0].X.Should().Be(0);
            result[0].Y.Should().Be(0);

            result[1].X.Should().Be(20);
            result[1].Y.Should().Be(0);

            result[2].X.Should().Be(0);
            result[2].Y.Should().Be(40);
        }

        [TestMethod]
        public void BinPackNodeHorizontalDecomposeWithDisplacedNodeAllElementsShouldHaveCorrectPosition()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(20, 30, 30, 90));

            var result = node.GetHorizontalDecomposeRegions(new Bitmap(20, 40));

            result[0].X.Should().Be(20);
            result[0].Y.Should().Be(30);

            result[1].X.Should().Be(40);
            result[1].Y.Should().Be(30);

            result[2].X.Should().Be(20);
            result[2].Y.Should().Be(70);
        }


        [TestMethod]
        public void BinPackNodeHorizonatalDecomposeAllElementsShouldHaveCorrectSize()
        {
            var node = new BinPackNode(new System.Drawing.Rectangle(0, 0, 30, 90));

            var result = node.GetHorizontalDecomposeRegions(new Bitmap(20, 40));

            result[0].Width.Should().Be(20);
            result[0].Height.Should().Be(40);

            result[1].Width.Should().Be(10);
            result[1].Height.Should().Be(90);

            result[2].Width.Should().Be(20);
            result[2].Height.Should().Be(50);
        }

        private Bitmap CreateBitmap(int width,int height, Color color)
        {
            var bit = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(bit);
            g.FillRectangle(new SolidBrush(color), new Rectangle(0, 0, width, height));
            return bit;
        }

        [TestMethod]
        public void BinPackWithOneNodeShouldHaveDimensionsOfTheNode()
        {
            var lst = new List<Bitmap>();
            lst.Add(CreateBitmap(200,500,Color.Red));

            var binPack = new BinPackAlghorithm(lst);

            var result = binPack.CreateBitmap();
            result.Width.Should().Be(200);
            result.Height.Should().Be(500);

            result.Save("t1.png");
        }

        [TestMethod]
        public void BinPackWith4NodesShouldReturnCorrectUsedAreas()
        {
            var lst = new List<Bitmap>();
            lst.Add(CreateBitmap(200, 500, Color.Red));
            lst.Add(CreateBitmap(400, 400, Color.Blue));
            lst.Add(CreateBitmap(400, 400, Color.Yellow));
           // lst.Add(CreateBitmap(100, 50, Color.Green));
            var binPack = new BinPackAlghorithm(lst);

            var result = binPack.CreateBitmapWithWireframe();
           // binPack.UsedArea().Should().Be(200*500+2*400*400 + 100*50);
           // result.Height.Should().Be(500);

            result.Save("t2.png");
        }
        
    }
}
