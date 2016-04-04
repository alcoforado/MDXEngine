

using SharpDX;

namespace MDXEngine.Interfaces
{
    
    

    /// <summary>
    ///The texture slot resource has a different mechanism.
    ///Since textures can have intense heavy memory copy between CPU and 
    ///and GPU and they are resource intensive we try to minimize
    ///copy. Textures are generally grouped in groups. A group of textures are internally
    ///inside one huge texture using a bin pack alghorithm. Be in mind that a group is only deleted
    ///from the gpu memory once all subtextures are disposed so when grouping your textures make sure
    ///all the subtextures have similar life spans. If two different shapes create a texture with the same bitmap
    ///object the system is smart enough to use the same GPU texture resource. Because of this functionality 
    ///texures were made immutable. A shape cannot change the bitmap of a texture once created and not create new ones.
    ///So a shape should allocate all bitmaps it needs before hand. This is done when by calling the shape method RequestSlotResources.
    ///  
    /// </summary>
    public interface ITextureSlotResource :ISlotAllocation
    {
       RectangleF BitmapRegion { get; }
    }
}