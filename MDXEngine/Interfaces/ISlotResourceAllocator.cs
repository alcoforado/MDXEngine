namespace MDXEngine.Interfaces
{
    public interface ISlotResourceAllocator
    {
        void RequestConstantBuffer<T>(string slotName, T data) where T : struct;

        void RequestTexture(string slotName, string fileName);
    }
}