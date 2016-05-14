using SharpDX;
namespace MDXEngine.SharpDXExtensions
{
    public static class Vector4Extension
        {
            public static Vector3 XYZ(this Vector4 v)
            {
                return new Vector3(v.X,v.Y,v.Z);
            }

            public static Vector2 XY(this Vector4 v)
            {
                return new Vector2(v.X, v.Y);
            }
        }
}
