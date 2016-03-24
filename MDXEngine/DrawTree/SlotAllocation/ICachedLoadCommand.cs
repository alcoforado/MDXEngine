using MDXEngine.Interfaces;

namespace MDXEngine.DrawTree.SlotAllocation
{
    internal interface ICachedLoadCommand : ISlotAllocation
    {

        SlotAllocation AllocationInfo
        {
            get; set;
        }


        HLSLProgram GetHLSL();

        IShaderResource LoadData(IShaderResource resource);

    }
}