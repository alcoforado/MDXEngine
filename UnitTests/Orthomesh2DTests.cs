using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;

namespace UnitTests
{
    [TestClass]
    public class Orthomesh2DTests
    {
        private OrthoMesh2D _mesh;

        [TestInitialize]
        public void Initialize()
        {
            _mesh = new OrthoMesh2D(3,2,Vector2.Zero,new Vector2(3f,2f));



        }


        [TestMethod]
        public void VerticeIteratorShouldIterateCorrectly()
        {
            var vIt = _mesh.BeginVertice();

            vIt.Index().Should().Be(0);
            vIt.Vertice()[0].Should().Be(0f);
            vIt.Vertice()[1].Should().Be(0f);

            vIt.Next();

            vIt.Index().Should().Be(1);
            vIt.Vertice()[0].Should().Be(1f);
            vIt.Vertice()[1].Should().Be(0f);

            vIt.Next();

            vIt.Index().Should().Be(2);
            vIt.Vertice()[0].Should().Be(2f);
            vIt.Vertice()[1].Should().Be(0f);
            vIt.Next();

            vIt.Index().Should().Be(3);
            vIt.Vertice()[0].Should().Be(3f);
            vIt.Vertice()[1].Should().Be(0f);
            vIt.Next();

            vIt.Index().Should().Be(4);
            vIt.Vertice()[0].Should().Be(0f);
            vIt.Vertice()[1].Should().Be(1f);
            vIt.Next();

            vIt.Index().Should().Be(5);
            vIt.Vertice()[0].Should().Be(1f);
            vIt.Vertice()[1].Should().Be(1f);
            vIt.Next();

            vIt.Index().Should().Be(6);
            vIt.Vertice()[0].Should().Be(2f);
            vIt.Vertice()[1].Should().Be(1f);
            vIt.Next();

            vIt.Index().Should().Be(7);
            vIt.Vertice()[0].Should().Be(3f);
            vIt.Vertice()[1].Should().Be(1f);
            vIt.Next();


            vIt.Index().Should().Be(8);
            vIt.Vertice()[0].Should().Be(0f);
            vIt.Vertice()[1].Should().Be(2f);
            vIt.Next();

            vIt.Index().Should().Be(9);
            vIt.Vertice()[0].Should().Be(1f);
            vIt.Vertice()[1].Should().Be(2f);
            vIt.Next();

            vIt.Index().Should().Be(10);
            vIt.Vertice()[0].Should().Be(2f);
            vIt.Vertice()[1].Should().Be(2f);
            vIt.Next();


            vIt.Index().Should().Be(11);
            vIt.Vertice()[0].Should().Be(3f);
            vIt.Vertice()[1].Should().Be(2f);
            var b = vIt.Next();

            b.Should().BeFalse();


        }



        [TestMethod]
        public void VerticeIteratorAtTheEndThroughInvalidIndex()
        {
            var vIt = _mesh.BeginVertice();
            while (vIt.Next());

            vIt.Index().Should().Be(uint.MaxValue);
        }


        [TestMethod]
        public void VerticeIteratorAtTheEndThroughExceptionForVerticeMethod()
        {
            var vIt = _mesh.BeginVertice();
            while (vIt.Next()) ;
            Action action = () => vIt.Vertice();
            (action).ShouldThrow<Exception>();
        }

        [TestMethod]
        public void CellIteratorShouldReturnCorrectIndices()
        {
            var cell = _mesh.BeginCell();
            cell.Index().Should().Be(0);
            cell.Next();

            cell.Index().Should().Be(1);
            cell.Next();

            cell.Index().Should().Be(2);
            cell.Next();

            cell.Index().Should().Be(3);
            cell.Next();

            cell.Index().Should().Be(4);
            cell.Next();


            cell.Index().Should().Be(5);
            var last = cell.Next();
            last.Should().BeFalse();
            cell.Index().Should().Be(OrthoMesh2D.InvalidIndex);
            
            cell.Next();
            cell.Index().Should().Be(OrthoMesh2D.InvalidIndex);


        }

        [TestMethod]
        public void CellIteratorShouldReturnCorrectVerticesIndices()
        {
            var cell = _mesh.BeginCell();

            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomLeft).Should().Be(0);
            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomRight).Should().Be(1);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpLeft).Should().Be(4);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpRight).Should().Be(5);

            cell.Next();

            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomLeft).Should().Be(1);
            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomRight).Should().Be(2);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpLeft).Should().Be(5);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpRight).Should().Be(6);


            cell.Next();

            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomLeft).Should().Be(2);
            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomRight).Should().Be(3);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpLeft).Should().Be(6);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpRight).Should().Be(7);

            cell.Next();

            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomLeft).Should().Be(4);
            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomRight).Should().Be(5);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpLeft).Should().Be(8);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpRight).Should().Be(9);

            cell.Next();

            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomLeft).Should().Be(5);
            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomRight).Should().Be(6);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpLeft).Should().Be(9);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpRight).Should().Be(10);

            cell.Next();

            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomLeft).Should().Be(6);
            cell.VertexIndex(OrthoMesh2D.CellVertice.BottomRight).Should().Be(7);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpLeft).Should().Be(10);
            cell.VertexIndex(OrthoMesh2D.CellVertice.UpRight).Should().Be(11);

        }


    
    }
}
