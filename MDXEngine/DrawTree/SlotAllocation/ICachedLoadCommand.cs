using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree.SlotAllocation
{
    internal interface ICachedLoadCommand : ISlotResource
    {

        SlotAllocation AllocationInfo
        {
            get; set;
        }


        HLSLProgram GetHLSL();

        IShaderResource LoadData(IShaderResource resource);

    }
}