using MDXEngine.MMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class BinPackRandomRun
    {
        public int NumElements { get; set; }
        public Interval WidthRange { get; set; }
        public Interval HeightRange { get; set; }
    }
}
