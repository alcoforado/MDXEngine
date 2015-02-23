using SharpDX.Direct3D11;

namespace MDXEngine
{
    public interface IShaderResource
    {
        void Load(HLSLProgram program, int slotId);
        ShaderResourceView GetResourceView();
    }
}
