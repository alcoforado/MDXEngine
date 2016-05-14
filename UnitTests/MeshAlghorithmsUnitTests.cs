using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDXEngine.Geometry;
using FluentAssertions;
using MDXEngine;
using MDXEngine.Shaders;
using MDXEngine.SharpDXExtensions;
using MDXEngine.Shapes;
using SharpDX;

namespace UnitTests
{
    [TestClass]
    public class MeshAlghorithmsUnitTests
    {
       void CreateTriangleArray(out IArray<VerticeNormal> vertices,out IArray<int> indices)
       {
           var v = new VerticeNormal[] {
               new VerticeNormal() {
                   Position = new Vector3(0,0,0),
                   Normal = new  Vector3(2)
               },
                   new VerticeNormal() {
                   Position = new Vector3(4,0,0),
                   Normal = new  Vector3(2)
               },
                   new VerticeNormal() {
                   Position = new Vector3(0,4,0),
                   Normal = new  Vector3(2)
               }
           };
           var i = new int[] {0,1,2};

           vertices = new SubArray<VerticeNormal>(v);
           indices = new SubArray<int>(i);
       }

       void CreateSquareArray(out IArray<VerticeNormal> vertices, out IArray<int> indices)
       {
           var v = new VerticeNormal[] {
               new VerticeNormal() {
                   Position = new Vector3(0,0,0),
                   Normal = new  Vector3(2)
               },
                   new VerticeNormal() {
                   Position = new Vector3(4,0,0),
                   Normal = new  Vector3(2)
               },
                   new VerticeNormal() {
                   Position = new Vector3(4,4,0),
                   Normal = new  Vector3(2)
               },
               new VerticeNormal() {
                   Position = new Vector3(0,4,0),
                   Normal = new  Vector3(2)
               }
           };
           var i = new int[] { 0, 1, 2,0,2,3 };

           vertices = new SubArray<VerticeNormal>(v);
           indices = new SubArray<int>(i);
       }

       void CreateSquareArray(out IArray<Vector3> vertices, out IArray<int> indices)
       {
           var v = new Vector3[] {
                   new Vector3(0,0,0),
                   new Vector3(4,0,0),
                   new Vector3(4,4,0),
                   new Vector3(0,4,0)
               };
           
           var i = new int[] { 0, 1, 2, 0, 2, 3 };

           vertices = new SubArray<Vector3>(v);
           indices = new SubArray<int>(i);
       }




        [TestMethod]
        public void Mesh_Of_One_Triangle_Should_Give_Correct_Normals()
        {
            IArray<VerticeNormal> v=null;
            IArray<int> i = null;

            this.CreateTriangleArray(out v,out i);

            MeshAlghorithms.ComputeAvgNormals(v, i);

            (v[0].Normal - new Vector3(0,0,1)).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[1].Normal - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f); 
            (v[2].Normal - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
        }

        [TestMethod]
        public void Mesh_Of_One_Square_Should_Give_Correct_Normals()
        {
            IArray<VerticeNormal> v = null;
            IArray<int> i = null;

            this.CreateSquareArray(out v, out i);

            MeshAlghorithms.ComputeAvgNormals(v, i);

            (v[0].Normal - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[1].Normal - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[2].Normal - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[3].Normal - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
        }

        [TestMethod]
        public void Mesh_Of_One_Square_Should_Give_Correct_Normals_2()
        {
            IArray<Vector3> v = null;
            IArray<int> i = null;
            this.CreateSquareArray(out v, out i);
            
            var n = new SubArray<Vector3>(new Vector3[v.Length]);

            MeshAlghorithms.ComputeAvgNormals(n,v,i);

            (n[0] - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[1] - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[2] - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[3] - new Vector3(0, 0, 1)).Norm2().Should().BeApproximately(0f, 0.000001f);
        }



        [TestMethod]
        public void Mesh_Of_Cube_Should_Give_Correct_Normals()
        {
            var v = new VerticeNormal[8];
            var i = new int[36];


            var sv = new SubArray<VerticeNormal>(v);
            var si = new SubArray<int>(i);

            var sh = new Cube(
                         new Vector3(0f, 0f, 0f),
                         new Vector3(2f, 2f, 2f));

            sh.Write(new Vertex3DArray<VerticeNormal>(sv), si);

            MeshAlghorithms.ComputeAvgNormals(sv, si);

            (v[0].Normal - Vector3.Normalize(new Vector3(-1, -1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[1].Normal - Vector3.Normalize(new Vector3(-1, 1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[2].Normal - Vector3.Normalize(new Vector3( 1, 1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[3].Normal - Vector3.Normalize(new Vector3( 1, -1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[4].Normal - Vector3.Normalize(new Vector3(-1, -1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[5].Normal - Vector3.Normalize(new Vector3(-1, 1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (v[6].Normal - Vector3.Normalize(new Vector3(1, 1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);        
            (v[7].Normal - Vector3.Normalize(new Vector3(1, -1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);
        
        }


        [TestMethod]
        public void Mesh_Of_Cube_Should_Give_Correct_Normals2()
        {
            var v = new VerticeNormal[8];
            var i = new int[36];


            var sv = new SubArray<VerticeNormal>(v);
            var si = new SubArray<int>(i);

            var sh = new Cube(
                         new Vector3(0f, 0f, 0f),
                         new Vector3(2f, 2f, 2f));

            sh.Write(new Vertex3DArray<VerticeNormal>(sv), si);

            var n = new SubArray<Vector3>(new Vector3[8]);

            MeshAlghorithms.ComputeAvgNormals(n,new Vertex3DArray<VerticeNormal>(sv), si);

            (n[0] - Vector3.Normalize(new Vector3(-1, -1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[1] - Vector3.Normalize(new Vector3(-1, 1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[2] - Vector3.Normalize(new Vector3(1, 1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[3] - Vector3.Normalize(new Vector3(1, -1, -1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[4] - Vector3.Normalize(new Vector3(-1, -1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[5] - Vector3.Normalize(new Vector3(-1, 1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[6] - Vector3.Normalize(new Vector3(1, 1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);
            (n[7] - Vector3.Normalize(new Vector3(1, -1, 1))).Norm2().Should().BeApproximately(0f, 0.000001f);

        }



    }
}
