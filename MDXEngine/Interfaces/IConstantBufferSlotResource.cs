namespace MDXEngine.Interfaces
{
    public interface IConstantBufferSlotResource<T> : ISlotAllocation
    {
        void SetData(T data);
    }
}