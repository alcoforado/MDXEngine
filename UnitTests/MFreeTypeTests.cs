using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MFreeType;
using FluentAssertions;
using MDXEngine.Painters;
using MDXEngine.SharpDXExtensions;
using System.Linq;
namespace UnitTests
{
    [TestClass]
    public class MFreeTypeTests
    {
        [TestMethod]
        public void MFreeTypeInitializationShouldNotThrowAnyExceptions()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
        }

        [TestMethod]
        public void ClearCashOfAnEmptyTestShouldNotThrowAnyExceptions()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            tp.ClearCash();
        }

        [TestMethod]
        public void LoadingAFontWithWrongPathShouldThrowExceptions()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            Action act =()=> {tp.GetFont(new System.IO.FileInfo("./noexistingfont"));};
            act.ShouldThrow<Exception>().WithMessage("cannot open resource");
        }

        [TestMethod]
        public void LoadingAFontWithValidPathShouldReturnValidFontValue()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font= tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.Should().NotBeNull();
        }

        [TestMethod]
        public void RasterizingACharactgerFontShouldNotThrowExceptions()
        {
            var colors = ColorGenerator.CreateLinearGradient(SharpDX.Color4.Black, SharpDX.Color4.White, 256).Select(x => x.ToSystemColor()).ToList();
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font = tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.SetSizeInPixels(100, 100);
            String str = "B";
            var bitmap = font.GetBitmap((int) str[0]);
            
            
            var palette = bitmap.Palette;
            for (int i=0;i<palette.Entries.Count();i++)
            {
                palette.Entries[i] = colors[i];
            }
            bitmap.Palette = palette;

            bitmap.Save("./B.bmp");

        }


        [TestMethod]
        public void GettingFontMapWithSpaceCharShouldNotThrowExceptions()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font = tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.SetSizeInPixels(100, 100);
            String fontmap_codes = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var fontmap = font.GetFontMapForChars(fontmap_codes);
        }


        [TestMethod]
        public void GettingFontMapWithRepeatedCharsShouldNotThrowExceptions()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font = tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.SetSizeInPixels(100, 100);
            String fontmap_codes = "ABCDEABCDE";

            var fontmap = font.GetFontMapForChars(fontmap_codes);
        }




        [TestMethod]
        public void GettingFontMapShouldNotThrowExceptions()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font = tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.SetSizeInPixels(100, 100);
            String fontmap_codes = " !\"#$%&'{}*+/-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

            var fontmap = font.GetFontMapForChars(fontmap_codes);
        

        }

        [TestMethod]
        public void RenderingWithFontMapShouldNotThrowExceptionsAndShouldReturnRenderedBitmap()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font = tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.SetSizeInPixels(100, 100);
            String fontmap_codes = "IMACULADA";

            var fontmap = font.GetFontMapForChars(fontmap_codes);
            var bitmap = fontmap.RenderLineText("IMACULADA", new TextRenderingOptions
            {
                padding_left = 10,
                padding_top=5
            });
            
            //set palette
            var colors = ColorGenerator.CreateLinearGradient(SharpDX.Color4.Black, SharpDX.Color4.White, 256).Select(x => x.ToSystemColor()).ToList();
            var palette = bitmap.Palette;
            for (int i = 0; i < palette.Entries.Count(); i++)
            {
                palette.Entries[i] = colors[i];
            }
            bitmap.Palette = palette;
            
            bitmap.Save("M.bmp");

        }

        [TestMethod]
        public void FontMapKerningTest()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font = tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.SetSizeInPixels(100, 100);
            String fontmap_codes = "IMACULADA";

            var fontmap = font.GetFontMapForChars(fontmap_codes);

            var str=fontmap.PrintKerningTable();

            System.Diagnostics.Debug.Write(str);
            

        }



    }
}
