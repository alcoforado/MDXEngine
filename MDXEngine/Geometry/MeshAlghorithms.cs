using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Geometry
{
    public class MeshAlghorithms
    {

        //Fill the normal fields of an array of T where T is IPosition and INormal.
        //The alghorithm assumes that T.Position is already set.
        //vertices is the array of vertices and IArray<int> are the indices of the triangles mesh
        public static void ComputeAvgNormals<T>(IArray<T> vector, IArray<int> vI) where T : struct, IPosition, INormal
        {
            var vector0 = new Vector3(0);
            for (int i = 0; i < vector.Length; i++)
            {
                var el = vector[i];
                el.Normal = vector0;
                vector[i] = el;
            }

            for (int i = 0; i < vI.Length; i += 3)
            {
                int i0 = vI[i];
                int i1 = vI[i + 1];
                int i2 = vI[i + 2];

                var el0 = vector[i0];
                var el1 = vector[i1];
                var el2 = vector[i2];
                

               

                Vector3 e0 = el1.Position - el0.Position;
                Vector3 e1 = el2.Position - el0.Position; 

                Vector3 faceNormal = Vector3.Cross(e0, e1);
                faceNormal.Normalize();

                el0.Normal += faceNormal;
                el1.Normal += faceNormal;
                el2.Normal += faceNormal;

                vector[i0] = el0;
                vector[i1] = el1;
                vector[i2] = el2;


                
                
            }
            for (int i = 0; i < vector.Length; i++)
            {
                var el = vector[i];
                el.Normal = Vector3.Normalize(el.Normal);
                vector[i] = el;
            }


        }

        //Fill the normal fields of an array of T where T is IPosition and INormal.
        //The alghorithm assumes that T.Position is already set.
        //vertices is the array of vertices and IArray<int> are the indices of the triangles mesh
        public static void ComputeAvgNormals(IArray<Vector3> normals,IArray<Vector3> vector, IArray<int> vI) 
        {
            var vector0 = new Vector3(0);
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = vector0;
            }

            for (int i = 0; i < vI.Length; i += 3)
            {
                int i0 = vI[i];
                int i1 = vI[i + 1];
                int i2 = vI[i + 2];

              


                Vector3 e0 = vector[i1] - vector[i0];
                Vector3 e1 = vector[i2] - vector[i0];

                Vector3 faceNormal = Vector3.Cross(e0, e1);
                faceNormal.Normalize();

                normals[i0]+=faceNormal;
                normals[i1]+=faceNormal;
                normals[i2]+=faceNormal;
            }
            for (int i = 0; i < vector.Length; i++)
            {
                normals[i]  = SharpDX.Vector3.Normalize(normals[i]);
            }


        }

    }
}
