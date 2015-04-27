using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MFreeType;
using FluentAssertions;
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
        public void RasterizingAFontShouldNotThrowExceptions()
        {
            MFreeType.MFreeType tp = new MFreeType.MFreeType();
            var font = tp.GetFont(new System.IO.FileInfo("./data/fonts/verdana.ttf"));
            new System.IO.FileInfo("./data/fonts/verdana.ttf").Exists.Should().BeTrue();
            font.SetSizeInPixels(100, 100);
            var bitmap = font.Rasterize("AB");
            
            bitmap.Save("./Hello.bmp");


        }




    }
}
