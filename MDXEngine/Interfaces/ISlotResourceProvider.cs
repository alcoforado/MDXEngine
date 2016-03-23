using MDXEngine.DrawTree;
using MDXEngine.DrawTree.SlotAllocation;

namespace MDXEngine.Interfaces
{
    public interface ISlotResourceProvider
    {


        IConstantBufferSlotResource<T> CreateConstantBuffer<T>(string slotName, T data) where T : struct;

        ITextureSlotResource CreateTexture(string slotName, string fileName);
    }
}