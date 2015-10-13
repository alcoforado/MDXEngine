using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine;
using MDXEngine.SharpDXExtensions;
using SharpDX;
using FluentAssertions;
using System.Runtime.InteropServices;
using MDXEngine.DrawingExtensions;
namespace UnitTests
{
    [TestClass]
    public class UnitTestExtensions
    {
        [TestMethod]
        public void Extensions_Vector4XYZ()
        {
            Vector4 v = new Vector4(1f, 2f, 3f, 4f);
            var v3 = v.XYZ();
            v3.X.Should().Be(1f);
            v3.Y.Should().Be(2f);
            v3.Z.Should().Be(3f);
        }
        [TestMethod]
        public void Extensions_Vector3_ToVector4()
        {
            Vector3 v = new Vector3(1f, 2f, 3f);
            var v4 = v.ToVector4(7f);
            v4.X.Should().Be(1f);
            v4.Y.Should().Be(2f);
            v4.Z.Should().Be(3f);
            v4.W.Should().Be(7f);

        }

        [TestMethod]
        public void Extensions_Vector3_Norm2()
        {
            Vector3 v = new Vector3(1f, 2f, 3f);
            float c = v.Norm2();
            c.Should().BeApproximately(14f,1e-8f);

        }

        [TestMethod]
        public void Rectangle_Area_Should_Be_Correct()
        {
            var rect = new System.Drawing.Rectangle(20, 30, 10, 40);
            rect.Right.Should().Be(30);
            rect.Bottom.Should().Be(70);
            rect.Area().Should().Be(400);
        }

    }
}
