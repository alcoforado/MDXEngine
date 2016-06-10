using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine;
using FluentAssertions;
using SharpDX;
using Moq;
using MDXEngine.Interfaces;
using MUtils.DefragArray;

namespace UnitTests
{
    [TestClass]
    public class DefragArrayTests
    {
        private int[] CreateIncArray(int size)
        {
            var t = new int[size];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = i;
            }
            return t;
        }



        [TestMethod]
        public void TestRemovalAndAddingElements()
        {
            var src = CreateIncArray(26);

            var cps = new List<CopyPlan>()
            {
                new CopyPlan()
                {
                    Orig = new FragmentRegion()
                    {
                        offI = 0,
                        size = 3
                    },
                    Dst = new FragmentRegion()
                    {
                        offI = 0,
                        size = 3
                    }
                },
                 new CopyPlan()
                {
                    Orig = new FragmentRegion()
                    {
                        offI = 7,
                        size = 1
                    },
                    Dst = new FragmentRegion()
                    {
                        offI = 4,
                        size = 1
                    }
                },
                  new CopyPlan()
                {
                    Orig = new FragmentRegion()
                    {
                        offI = 8,
                        size = 5
                    },
                    Dst = new FragmentRegion()
                    {
                        offI = 5,
                        size = 5
                    }
                },
                   new CopyPlan()
                {
                    Orig = new FragmentRegion()
                    {
                        offI = 16,
                        size = 2
                    },
                    Dst = new FragmentRegion()
                    {
                        offI = 10,
                        size = 2
                    }
                },
                    new CopyPlan()
                {
                    Orig = new FragmentRegion()
                    {
                        offI = 18,
                        size = 2
                    },
                    Dst = new FragmentRegion()
                    {
                        offI = 15,
                        size = 2
                    }
                },
                 new CopyPlan()
                {
                    Orig = new FragmentRegion()
                    {
                        offI = 20,
                        size = 3
                    },
                    Dst = new FragmentRegion()
                    {
                        offI = 18,
                        size = 3
                    }
                },
                  new CopyPlan()
                {
                    Orig = new FragmentRegion()
                    {
                        offI = 25,
                        size = 1
                    },
                    Dst = new FragmentRegion()
                    {
                        offI = 21,
                        size = 1
                    }
                }
            };

           DefragArray.ReorganizeArray(src,cps);

            src[0].Should().Be(0);
            src[4].Should().Be(7);
            src[5].Should().Be(8);
            src[6].Should().Be(9);
            src[7].Should().Be(10);
            src[8].Should().Be(11);
            src[9].Should().Be(12);
            src[10].Should().Be(16);
            src[11].Should().Be(17);
            src[15].Should().Be(18);
            src[16].Should().Be(19);
            src[18].Should().Be(20);
            src[19].Should().Be(21);
            src[20].Should().Be(22);
            src[21].Should().Be(25);



        }
    }
}
