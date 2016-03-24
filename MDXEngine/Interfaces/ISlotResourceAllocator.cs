﻿using MDXEngine.DrawTree;
using MDXEngine.DrawTree.SlotAllocation;

namespace MDXEngine.Interfaces
{
    public interface ISlotResourceAllocator
    {
        IConstantBufferSlotResource<T> RequestConstantBuffer<T>(string slotName, T data) where T : struct;

        ITextureSlotResource RequestTexture(string slotName, string fileName);
    }
}