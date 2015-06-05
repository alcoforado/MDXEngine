using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine;
using FluentAssertions;
using SharpDX;
using Moq;
namespace UnitTests
{
    [TestClass]
    public class UnitTestDrawTree
    {
        public struct ColorVerticeData : IPosition
        {
            public Vector3 Position {get; set;}
            public Vector3 Color;
        }

        Mock<IShape<ColorVerticeData>> mShapeIV10;
        Mock<IShape<ColorVerticeData>> mShapeI10V4;
        Mock<IShape<ColorVerticeData>> mShapeI5V3;
        Mock<IShape<ColorVerticeData>> mShapeIV4;
        Mock<IShape<ColorVerticeData>> mShapeV4;
        Mock<IShape<ColorVerticeData>> mPointsV4;


        [TestInitialize]
        public void setMoqs()
        {

            mShapeI10V4 = new Mock<IShape<ColorVerticeData>>();
            mShapeI10V4.Setup(x => x.NIndices()).Returns(10);
            mShapeI10V4.Setup(x => x.NVertices()).Returns(4);
            mShapeI10V4.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);

            mShapeI5V3 = new Mock<IShape<ColorVerticeData>>();
            mShapeI5V3.Setup(x => x.NIndices()).Returns(5);
            mShapeI5V3.Setup(x => x.NVertices()).Returns(3);
            mShapeI5V3.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);
            
            
            mShapeIV10 = new Mock<IShape<ColorVerticeData>>();
            mShapeIV10.Setup(x => x.NIndices()).Returns(10);
            mShapeIV10.Setup(x => x.NVertices()).Returns(10);
            mShapeIV10.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);

            
            mShapeIV4  = new Mock<IShape<ColorVerticeData>>();
            mShapeIV4.Setup(x => x.NIndices()).Returns(4);
            mShapeIV4.Setup(x => x.NVertices()).Returns(4);
            mShapeIV4.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);

            mShapeV4 = new Mock<IShape<ColorVerticeData>>();
            mShapeV4.Setup(x => x.NIndices()).Returns(0);
            mShapeV4.Setup(x => x.NVertices()).Returns(4);
            mShapeV4.Setup(x => x.GetTopology()).Returns(TopologyType.TRIANGLES);

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
            tree.Add(mShapeIV10.Object);
            tree.Add(mShapeIV4.Object);
            
            //Check
            var it = tree.BeginIterator();
            it.GotoChild().Should().BeTrue();
            it.GotoNextSibling().Should().BeFalse();
            it.GetData().IsShapeGroupNode().Should().Be(true);
            it.GetData().ShapeGroupNode.IsIndexed().Should().BeTrue();
            it.GotoChild().Should().BeTrue();
            it.GetData().IsShapeNode().Should().Be(true);
            it.GetData().ShapeNode.Should().NotBeNull();
            it.GetData().ShapeNode.Shape.NIndices().Should().Be(10);
            it.GotoNextSibling().Should().BeTrue();
            it.GetData().IsShapeNode().Should().Be(true);
            it.GetData().ShapeNode.Shape.NIndices().Should().Be(4);
             
        }

        [TestMethod]
        public void DrawTree_ShapesWithDifferentTypeShouldBeAddedToDifferentGroups()
        {
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mShapeIV10.Object);
            tree.Add(mShapeIV4.Object);
            tree.Add(mShapeV4.Object);
            tree.Add(mPointsV4.Object);

            //Check
            var it = tree.BeginIterator();
            it.GotoChild().Should().BeTrue();
            it.GetData().IsShapeGroupNode().Should().BeTrue();
            it.GetData().ShapeGroupNode.IsIndexed().Should().BeTrue();
            it.GetData().ShapeGroupNode.GetTopology().Should().Be(TopologyType.TRIANGLES);
            it.GetNode().GetChilds().Count.Should().Be(2);
            it.GetData().Changed.Should().BeTrue();



            it.GotoNextSibling().Should().BeTrue();
            it.GetData().IsShapeGroupNode().Should().BeTrue();
            it.GetData().ShapeGroupNode.IsIndexed().Should().BeFalse();
            it.GetData().ShapeGroupNode.GetTopology().Should().Be(TopologyType.TRIANGLES);
            it.GetNode().GetChilds().Count.Should().Be(1);
            it.GetData().Changed.Should().BeTrue();


            it.GotoNextSibling().Should().BeTrue();
            it.GetData().IsShapeGroupNode().Should().BeTrue();
            it.GetData().ShapeGroupNode.IsIndexed().Should().BeFalse();
            it.GetData().ShapeGroupNode.GetTopology().Should().Be(TopologyType.POINTS);
            it.GetNode().GetChilds().Count.Should().Be(1);
            it.GetData().Changed.Should().BeTrue();

            it.GotoNextSibling().Should().BeFalse();
            

        }

        [TestMethod]
        public void DrawTree_ComputeSizesOfOneShape()
        {
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mShapeI10V4.Object);

            tree.ComputeSizes();
            
            var it = tree.BeginIterator();

            //root
            it.GetData().RootNode.SizeI.Should().Be(10);
            it.GetData().RootNode.SizeV.Should().Be(4);
            it.GetData().RootNode.OffV.Should().Be(0);
            it.GetData().RootNode.OffI.Should().Be(0);
            //group
            it.GotoChild().Should().BeTrue();
            it.GetData().ShapeGroupNode.SizeI.Should().Be(10);
            it.GetData().ShapeGroupNode.SizeV.Should().Be(4);
            it.GetData().ShapeGroupNode.OffV.Should().Be(0);
            it.GetData().ShapeGroupNode.OffI.Should().Be(0);
            //the only shape
            it.GotoChild().Should().BeTrue();
            it.GetData().ShapeNode.SizeI.Should().Be(10);
            it.GetData().ShapeNode.SizeV.Should().Be(4);
            it.GetData().ShapeNode.OffV.Should().Be(0);
            it.GetData().ShapeNode.OffI.Should().Be(0);


        }

        [TestMethod]
        public void DrawTree_ComputeSizesOfO2ShapesSameType()
        {
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mShapeI10V4.Object);
            tree.Add(mShapeI5V3.Object);

            tree.ComputeSizes();
            
            
            //root
            var it = tree.BeginIterator();
            it.GetData().RootNode.SizeI.Should().Be(15);
            it.GetData().RootNode.SizeV.Should().Be(7);
            it.GetData().RootNode.OffV.Should().Be(0);
            it.GetData().RootNode.OffI.Should().Be(0);

            //shape group
            it.GotoChild().Should().BeTrue();
            it.GetData().ShapeGroupNode.SizeI.Should().Be(15);
            it.GetData().ShapeGroupNode.SizeV.Should().Be(7);
            it.GetData().ShapeGroupNode.OffV.Should().Be(0);
            it.GetData().ShapeGroupNode.OffI.Should().Be(0);

            //shape 1
            it.GotoChild().Should().BeTrue();
            it.GetData().ShapeNode.SizeI.Should().Be(10);
            it.GetData().ShapeNode.SizeV.Should().Be(4);
            it.GetData().ShapeNode.OffV.Should().Be(0);
            it.GetData().ShapeNode.OffI.Should().Be(0);

            //shape 2
            it.GotoNextSibling().Should().BeTrue();
            it.GetData().ShapeNode.SizeI.Should().Be(5);
            it.GetData().ShapeNode.SizeV.Should().Be(3);
            it.GetData().ShapeNode.OffI.Should().Be(10);
            it.GetData().ShapeNode.OffV.Should().Be(4);

        }



        [TestMethod]
        public void DrawTree_ComputeSizesOfO3ShapesSameTypePlusOneDifferentShape()
        {
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mShapeI10V4.Object);
            tree.Add(mShapeI5V3.Object);
            tree.Add(mShapeIV10.Object);
            tree.Add(mPointsV4.Object);
            tree.ComputeSizes();


            //root
            var it = tree.BeginIterator();
            it.GetData().RootNode.SizeI.Should().Be(25); 
            it.GetData().RootNode.SizeV.Should().Be(21); 
            it.GetData().RootNode.OffV.Should().Be(0); 
            it.GetData().RootNode.OffI.Should().Be(0); 

            //shape group
            it.GotoChild().Should().BeTrue();
            it.GetData().ShapeGroupNode.SizeI.Should().Be(25);
            it.GetData().ShapeGroupNode.SizeV.Should().Be(17);
            it.GetData().ShapeGroupNode.OffV.Should().Be(0);
            it.GetData().ShapeGroupNode.OffI.Should().Be(0);

            //shape 1
            it.GotoChild().Should().BeTrue();
            it.GetData().ShapeNode.SizeI.Should().Be(10);
            it.GetData().ShapeNode.SizeV.Should().Be(4);
            it.GetData().ShapeNode.OffV.Should().Be(0);
            it.GetData().ShapeNode.OffI.Should().Be(0);

            //shape 2
            it.GotoNextSibling().Should().BeTrue();
            it.GetData().ShapeNode.SizeI.Should().Be(5);
            it.GetData().ShapeNode.SizeV.Should().Be(3);
            it.GetData().ShapeNode.OffI.Should().Be(10);
            it.GetData().ShapeNode.OffV.Should().Be(4);


            //shape 2
            it.GotoNextSibling().Should().BeTrue();
            it.GetData().ShapeNode.SizeI.Should().Be(10);
            it.GetData().ShapeNode.SizeV.Should().Be(10);
            it.GetData().ShapeNode.OffI.Should().Be(15);
            it.GetData().ShapeNode.OffV.Should().Be(7);


            it = tree.BeginIterator();
            it.GotoChild();
            it.GotoNextSibling().Should().BeTrue();
            it.GetData().ShapeGroupNode.SizeI.Should().Be(0);
            it.GetData().ShapeGroupNode.SizeV.Should().Be(4);
            it.GetData().ShapeGroupNode.OffI.Should().Be(25);
            it.GetData().ShapeGroupNode.OffV.Should().Be(17);

            it.GotoChild();
            it.GetData().ShapeNode.SizeI.Should().Be(0);
            it.GetData().ShapeNode.SizeV.Should().Be(4);
            it.GetData().ShapeNode.OffI.Should().Be(25);
            it.GetData().ShapeNode.OffV.Should().Be(17);

        }

        [TestMethod]
        public void DrawTree_FullSyncTreeOfJustOneIndexedShapeShouldPassIndicesAndVerticesArraysOfCorrectSize()
        {
            this.mShapeI5V3.Setup(x => x.Write(It.IsAny<IArray<ColorVerticeData>>(), It.IsAny<IArray<int>>()))
                .Callback( (IArray<ColorVerticeData> v, IArray<int> i) =>
                {
                    i.Length.Should().Be(5);
                    v.Length.Should().Be(3);
                });
            
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mShapeI5V3.Object);
            tree.FullSyncTree();
            mShapeI5V3.Verify(x => x.Write(It.IsAny<IArray<ColorVerticeData>>(), It.IsAny<IArray<int>>()));
        }

        [TestMethod]
        public void DrawTree_FullSyncTreeOfJustOneIndexedShapeShouldCopyShapeIndices()
        {
            this.mShapeI5V3.Setup(x => x.Write(It.IsAny<IArray<ColorVerticeData>>(), It.IsAny<IArray<int>>()))
                .Callback((IArray<ColorVerticeData> v, IArray<int> i) =>
                {
                    i[0]=0;
                    i[1]=1;
                    i[2]=2;
                    i[3]=3;
                    i[4]=4;
                });

            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mShapeI5V3.Object);
            tree.FullSyncTree();
            tree.Indices.Should().Equal(new int[]{0,1,2,3,4},(int left,int right) => left==right);
            mShapeI5V3.Verify(x => x.Write(It.IsAny<IArray<ColorVerticeData>>(), It.IsAny<IArray<int>>()));
        }

        [TestMethod]
        public void DrawTree_FullSyncTreeOfJustOneIndexedShapeShouldCopyShapeVertices()
        {
            this.mShapeI5V3.Setup(x => x.Write(It.IsAny<IArray<ColorVerticeData>>(), It.IsAny<IArray<int>>()))
                .Callback((IArray<ColorVerticeData> v, IArray<int> i) =>
                {
                    ColorVerticeData aux = new ColorVerticeData();
                    
                    aux.Position = new Vector3(0f);
                    v[0] = aux;

                    aux.Position = new Vector3(1f);
                    v[1] = aux;

                    aux.Position = new Vector3(2f);
                    v[2] = aux;

                });
            var tree = new DrawTree<ColorVerticeData>();
            tree.Add(mShapeI5V3.Object);
            tree.FullSyncTree();
            
            var result = new ColorVerticeData[]{
                new ColorVerticeData(){Position=new Vector3(0f)},
                new ColorVerticeData(){Position=new Vector3(1f)},
                new ColorVerticeData(){Position=new Vector3(2f)}
            };

            tree.Vertices.Should().Equal(result, (left,right) => left.Position == right.Position);
            mShapeI5V3.Verify(x => x.Write(It.IsAny<IArray<ColorVerticeData>>(), It.IsAny<IArray<int>>()));
        }



    
    }
}
