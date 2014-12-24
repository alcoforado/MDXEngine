using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine;
using Should.Fluent;
using SharpDX;
using Moq;
namespace UnitTests
{
    [TestClass]
    public class UnitTestDrawTree
    {
        public class ColorVerticeData : IPosition
        {
            public Vector3 Position {get; set;}
            public Vector3 Color;
        }

        Mock<IShape<ColorVerticeData>> mTriangleIV10;
        Mock<IShape<ColorVerticeData>> mTriangleIV4;
        Mock<IShape<ColorVerticeData>> mTriangleV4;
        Mock<IShape<ColorVerticeData>> mPointsV4;


        [TestInitialize]
        public void setMoqs()
        {
            mTriangleIV10 = new Mock<IShape<ColorVerticeData>>();
            mTriangleIV10.Setup(x => x.NIndices()).Returns(10);
            mTriangleIV10.Setup(x => x.NVertices()).Returns(10);
            mTriangleIV10.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);

            
            mTriangleIV4  = new Mock<IShape<ColorVerticeData>>();
            mTriangleIV4.Setup(x => x.NIndices()).Returns(4);
            mTriangleIV4.Setup(x => x.NVertices()).Returns(4);
            mTriangleIV4.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);

            mTriangleV4 = new Mock<IShape<ColorVerticeData>>();
            mTriangleV4.Setup(x => x.NIndices()).Returns(0);
            mTriangleV4.Setup(x => x.NVertices()).Returns(4);
            mTriangleV4.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);

            mPointsV4 = new Mock<IShape<ColorVerticeData>>();
            mPointsV4.Setup(x => x.NIndices()).Returns(0);
            mPointsV4.Setup(x => x.NVertices()).Returns(4);
            mPointsV4.Setup(x => x.GetTopology()).Returns(TopologyType.POINTS);
            



        }

        [TestMethod]
        public void DrawTree_TwoShapesWithEqualTypeShouldBeAddedToTheSameGroup()
        {
            //Set
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mTriangleIV10.Object);
            tree.Add(mTriangleIV4.Object);
            
            //Check
            var it = tree.BeginIterator();
            it.GotoChild().Should().Be.True();
            it.GotoNextSibling().Should().Be.False();
            it.GetData().IsIndexed().Should().Be.True();
            it.GetData().GetType().Should().Be.Equals(DrawInfoType.SHAPE_GROUP);
            it.GotoChild().Should().Be.True();
            it.GetData().GetType().Should().Be.Equals(DrawInfoType.SHAPE);
            it.GetData().Shape.Should().Not.Be.Null();
            it.GetData().Shape.NIndices().Should().Be.Equals(10);
            it.GotoNextSibling().Should().Be.True();
            it.GetData().GetType().Should().Be.Equals(DrawInfoType.SHAPE);
            it.GetData().Shape.Should().Not.Be.Null();
            it.GetData().Shape.NIndices().Should().Be.Equals(4);
             
        }

        [TestMethod]
        public void DrawTree_ShapesWithDifferentTypeShouldBeAddedToDifferentGroups()
        {
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mTriangleIV10.Object);
            tree.Add(mTriangleIV4.Object);
            tree.Add(mTriangleV4.Object);
            tree.Add(mPointsV4.Object);

            //Check
            var it = tree.BeginIterator();
            it.GotoChild().Should().Be.True();
            it.GetData().GetType().Should().Be.Equals(DrawInfoType.SHAPE_GROUP);
            it.GetData().IsIndexed().Should().Be.True();
            it.GetData().GetTopology().Should().Be.Equals(TopologyType.TRIANGLES);
            it.GetNode().GetChilds().Count.Should().Be.Equals(2);
            it.GetData().Changed.Should().Be.True();



            it.GotoNextSibling().Should().Be.True();
            it.GetData().GetType().Should().Be.Equals(DrawInfoType.SHAPE_GROUP);
            it.GetData().IsIndexed().Should().Be.False();
            it.GetData().GetTopology().Should().Be.Equals(TopologyType.TRIANGLES);
            it.GetNode().GetChilds().Count.Should().Be.Equals(1);
            it.GetData().Changed.Should().Be.True();


            it.GotoNextSibling().Should().Be.True();
            it.GetData().GetType().Should().Be.Equals(DrawInfoType.SHAPE_GROUP);
            it.GetData().IsIndexed().Should().Be.False();
            it.GetData().GetTopology().Should().Be.Equals(TopologyType.POINTS);
            it.GetNode().GetChilds().Count.Should().Be.Equals(1);
            it.GetData().Changed.Should().Be.True();

            it.GotoNextSibling().Should().Be.False();
            

        }



    
    }
}
