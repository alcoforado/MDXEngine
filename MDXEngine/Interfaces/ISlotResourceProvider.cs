using MDXEngine.DrawTree;
using MDXEngine.DrawTree.SlotAllocation;

namespace MDXEngine.Interfaces
{
    public interface ISlotResourceProvider
    {
        ILoadCommand CreateLoadCommand(SlotRequest request);

        IConstantBufferSlotResource<T> CreateConstantBufferSlotResource<T>(string slotName, T data);

        ITextureSlotResource CreateTextureSlotResource(string slotName, string fileName);

    }
}