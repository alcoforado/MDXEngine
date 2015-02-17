using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;
using MDXEngine;
using FluentAssertions;
namespace UnitTests
{


    [TestClass]
    public class SubArraysTest
    {
        public struct ColorVerticeData : IPosition, IPosition2D
        {
            public Vector2 Position2D { get; set; }
            public Vector3 Position { get; set; }
            public Vector3 Color;
        }



        
        public ColorVerticeData[] returnInitialArray()
        {
            return new ColorVerticeData[]{
                new ColorVerticeData(){Position=new Vector3(0f),Color=new Vector3(0f)},
                new ColorVerticeData(){Position=new Vector3(1f),Color=new Vector3(0f)},
                new ColorVerticeData(){Position=new Vector3(2f),Color=new Vector3(0f)},
                new ColorVerticeData(){Position=new Vector3(3f),Color=new Vector3(0f)}

            };

        }


        [TestMethod]
        public void ASubArrayForPosition1Size2ShouldHaveSize2()
        {
            var array = returnInitialArray();
            var subArray = new SubArray<ColorVerticeData>(array,1,2);

            subArray.Length.Should().Be(2);
        }

        [TestMethod]
        public void ASubArrayForPosition1Size2ShouldHaveElements1And2()
        {
            var array = returnInitialArray();
            var subArray = new SubArray<ColorVerticeData>(array,1,2);

            subArray[0].ShouldBeEquivalentTo(array[1]);
            subArray[1].ShouldBeEquivalentTo(array[2]);
        }

        [TestMethod]
        public void ASubArrayElementWhenSetWithAValueShouldBeSet()
        {
            var array = returnInitialArray();
            var subArray = new SubArray<ColorVerticeData>(array, 1, 2);
            var data = new ColorVerticeData() { Position = new Vector3(5f, 6f, 7f), Color = new Vector3(8f, 9f, 10f) };
            
            subArray[1] = data;
            subArray[1].ShouldBeEquivalentTo(data);
        }
        
        [TestMethod]
        public void ASubArrayElementWhenSetWithAValueShouldBeSetInTheOriginalArray()
        {
            var array = returnInitialArray();
            var subArray = new SubArray<ColorVerticeData>(array, 1, 2);
            var data = new ColorVerticeData() { Position = new Vector3(5f, 6f, 7f), Color = new Vector3(8f, 9f, 10f) };

            subArray[1] = data;
            
            array[1].ShouldBeEquivalentTo(data);
        }


        [TestMethod]
        public void AVertex2DArrayElementWhenSetShouldConserveTheValue()
        {
            var array = returnInitialArray();
            var subArray = new SubArray<ColorVerticeData>(array, 1, 2);
            var sub2D = new Vertex2DArray<ColorVerticeData>(subArray);

            var p0 = new Vector2(13F, 14F);
            var p1 = new Vector2(15F, 16f);
            
            sub2D[0]=p0;
            sub2D[1]=p1;

            sub2D[0].ShouldBeEquivalentTo(p0);
            sub2D[1].ShouldBeEquivalentTo(p1);
            

        }

        [TestMethod]
        public void AVertex2DArrayElementWhenSetShouldSetTheValueInTheOriginalArray()
        {
            var array = returnInitialArray();
            var subArray = new SubArray<ColorVerticeData>(array, 1, 2);
            var sub2D = new Vertex2DArray<ColorVerticeData>(subArray);

            var p0 = new Vector2(13F, 14F);
            var p1 = new Vector2(15F, 16f);

            sub2D[0] = p0;
            sub2D[1] = p1;

            array[1].Position2D.ShouldBeEquivalentTo(p0);
            array[2].Position2D.ShouldBeEquivalentTo(p1);
   

        }

        [TestMethod]
        public void SubArrayCopyFromShouldCopyElementsFromArray()
        {
        var t = new int[] {1, 2, 3, 4, 5, 6};

            var sub = new SubArray<int>(t, 1, 3);
            var t2 = new int[] {6, 6, 6};

            sub.CopyFrom(t2);

            t.Should().ContainInOrder(new[] {1, 6, 6, 6, 5, 6});




        }


    }
}
