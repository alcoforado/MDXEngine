using MDXEngine.DrawTree;
using MDXEngine.DrawTree.SlotAllocation;

namespace MDXEngine.Interfaces
{
    public interface ISlotResourceProvider
    {
        ILoadCommand CreateLoadCommand(SlotRequest request);
    }
}