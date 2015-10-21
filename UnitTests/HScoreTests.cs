using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine.MMath;
using FluentAssertions;
namespace UnitTests
{
    [TestClass]
    public class HScoreTests
    {
        [TestMethod]
        public void HScoreLessThanShoulReturnCorrectResult()
        {
            var a = new HScore(new long[] { 1, 2, 3 });
            var b = new HScore(new long[] { 2, 2, 3 });
            var c = new HScore(new long[] { 1, 2, 4 });
            var d = a;
            (d < a).Should().BeFalse();
            (a < b).Should().BeTrue();
            (a < c).Should().BeTrue();
        }

        [TestMethod]
        public void HScoreGreaterThanShoulReturnCorrectResult()
        {
            var a = new HScore(new long[] { 1, 2, 3 });
            var b = new HScore(new long[] { 2, 2, 3 });
            var c = new HScore(new long[] { 1, 2, 4 });
            var d = a;
            (d > a).Should().BeFalse();
            (b > a).Should().BeTrue();
            (c > a).Should().BeTrue();
        }

        [TestMethod]
        public void HScoreAddScoreShouldSetAScoreAsExpected()
        {
            var a = new HScore(new long[] { 1, 2, 3 });
            var b = new HScore();

            b.AppendScore(1).AppendScore(2).AppendScore(3);

            (a > b).Should().BeFalse();
            (a < b).Should().BeFalse();
            (a == b).Should().BeTrue();
        }

        [TestMethod]
        public void HScoreEqualOperatorShouldGiveCorrectResults()
        {
            var a = new HScore(new long[] { 1, 2, 3 });
            var b = new HScore(new long[] { 1, 2, 3 });
            var c = new HScore(new long[] { 1, 7, 3 });


            (a == b).Should().BeTrue();
            (a == c).Should().BeFalse();
        }


    }
}
