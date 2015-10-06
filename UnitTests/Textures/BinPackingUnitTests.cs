using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using MDXEngine.Textures.BinPack;
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


    }
}
