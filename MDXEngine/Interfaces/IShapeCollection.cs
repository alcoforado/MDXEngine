using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Interfaces
{
    public interface IShapeCollection<VerticeDataType>
    {
        void Add(IShape<VerticeDataType> shape);
        void Remove(IShape<VerticeDataType> shape);

    }
}
