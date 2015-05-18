using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MDXEngine.Textures
{
    public enum TextAlignment {Right,Left,Center};
    public class TextWriteOptions
    {
        public int padding_bottom;
        public int padding_right;
        public int padding_top;
        public int padding_left;
        public TextAlignment text_align;
        public SharpDX.Color color=SharpDX.Color.White;
        public String font_type = "Arial";
        public int font_size=12;
    }
}
