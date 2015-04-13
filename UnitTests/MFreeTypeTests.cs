using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MFreeType;
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

       

    }
}
