using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Textures.BinPack
{
    enum Decomposition { Horizontal,Vertical,NotNeeded};
    internal class BinPackAnalysis
    {
        public BinPackNode node { get; set; }
        public Size used_dims {get; set; }
        public bool HorizontalExpansion { get; set; }
        public bool VerticalExpansion { get; set; }
        public Decomposition DecompositionType { get; set; }
    }


}
