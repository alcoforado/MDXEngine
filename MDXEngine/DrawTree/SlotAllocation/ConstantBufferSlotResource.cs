using MDXEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.DrawTree.SlotAllocation
{
    internal class ConstantBufferSlotResource<T> : SlotResourceProvider.LoadCommandBase, IConstantBufferSlotResource<T>, ISlotPoolResourceFactory where T : struct
    {
        T _data;
        private SlotPool.SlotAllocationHandler _handler;
        private bool _dataChanged;
        public ConstantBufferSlotResource(string slotName, T data, SlotResourceProvider provider)
            : base(slotName, provider)
        {
            _data = data;
            _handler = GetSlotPool(slotName).CreateHandler(this);
            _dataChanged = true;
        }

        public override void Load()
        {
            _handler.Update(true);
        }




        public void SetData(T data)
        {
            _data = data;
            _dataChanged = true;
        }

        public override bool CanBeOnSameSlot(ILoadCommand command)
        {
            return false;
        }


        public IShaderResource CreateResource()
        {
            var resource = new CBufferResource<T>(this.GetHLSL().DxContext);
            resource.Load(_data);
            _dataChanged = false;
            return resource;
        }

        public void SetResource(IShaderResource resource)
        {
            if (_dataChanged)
                resource.Load(_data);
            _dataChanged = false;
        }
    }
}
