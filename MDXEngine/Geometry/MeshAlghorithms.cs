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
        static void ComputeAvgNormals<T>(IArray<T> vector, IArray<int> vI) where T : struct, IPosition, INormal
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i].Normal = new Vector3(0);
            }

            for (int i = 0; i < vI.Length; i += 3)
            {
                int i0 = vI[i];
                int i1 = vI[i + 1];
                int i2 = vI[i + 2];

                Vector3 v0 = vector[i0].Position;
                Vector3 v1 = vector[i1].Position;
                Vector3 v2 = vector[i2].Position;

                Vector3 e0 = v1 - v0;
                Vector3 e1 = v2 - v0;

                Vector3 faceNormal = Vector3.Cross(e0, e1);
                faceNormal.Normalize();

                vector[i0].Normal += faceNormal;
                vector[i1].Normal += faceNormal;
                vector[i2].Normal += faceNormal;
            }
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i].Normal.Normalize();
            }


        }
    }
}
