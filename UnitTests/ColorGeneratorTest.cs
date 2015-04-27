using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine.Painters;
using SharpDX;
using FluentAssertions;
namespace UnitTests
{
    [TestClass]
    public class ColorGeneratorTest
    {
        [TestMethod]
        public void LinearGradientOfTwoColorsShouldReturnTheTwoColors()
        {
            Color4 start = Color.Green.ToColor4();
            Color4 end = Color.Red.ToColor4();

            var result = ColorGenerator.CreateLinearGradient(start, end, 2);

            result.Count.Should().Be(2);
            result[0].Should().Be(start);
            result[1].Should().Be(end);



        }

        [TestMethod]
        public void LinearGradientOfThreeColorsShouldReturnAMidPointColorTogetherWithTheTwoColors()
        {
            Color4 start = new Color4(1.0f,0.4f,0.2f,0.6f);
            Color4 end = new Color4(0f, 0f, 0f, 0f);
            Color4 mid = new Color4(0.5f, 0.2f, 0.1f, 0.3f);
            var result = ColorGenerator.CreateLinearGradient(start, end, 3);



            result.Count.Should().Be(3);
            result[0].Should().Be(start);
            result[2].Should().Be(end);

            mid.Red.Should().BeApproximately(result[1].Red, 5);
            mid.Green.Should().BeApproximately(result[1].Green, 5);
            mid.Blue.Should().BeApproximately(result[1].Blue, 5);
            mid.Alpha.Should().BeApproximately(result[1].Alpha, 5);
         



        }
    }
}
