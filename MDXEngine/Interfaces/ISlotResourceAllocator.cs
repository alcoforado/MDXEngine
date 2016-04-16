using MDXEngine.DrawTree;
using MDXEngine.DrawTree.SlotAllocation;

namespace MDXEngine.Interfaces
{
    public interface ISlotResourceAllocator
    {
        IConstantBufferSlotResource<T> RequestConstantBuffer<T>(string slotName, T data) where T : struct;

        IConstantBufferSlotResource<T> RequestConstantBuffer<T>(string slotName) where T : struct;

        ITextureSlotResource RequestTexture(string slotName, string fileName);

        ITextureSlotResource RequestTextureInAtlas(string slotName, System.Drawing.Bitmap bp, string atlasName);

        ITextureSlotResource RequestTexture(string slotName, System.Drawing.Bitmap bp);

    }
}