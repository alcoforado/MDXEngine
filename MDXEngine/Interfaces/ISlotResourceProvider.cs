using MDXEngine.DrawTree;
using MDXEngine.DrawTree.SlotAllocation;

namespace MDXEngine.Interfaces
{
    public interface ISlotResourceProvider
    {
        ILoadCommand CreateLoadCommand(SlotRequest request);

        IConstantBufferSlotResource<T> CreateConstantBuffer<T>(string slotName, T data);

        ITextureSlotResource CreateTexture(string slotName, string fileName);
    }
}