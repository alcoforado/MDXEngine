using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using MDXEngine.Geometry;
using FluentAssertions;
namespace UnitTests
{
    /// <summary>
    /// Summary description for MathUnitTest
    /// </summary>
    [TestClass]
    public class MathUnitTest
    {
        [TestMethod]
        public void AndgleShouldReturnCorrectRadForTheFourCardinatePoints()
        {
            Angle angle1 = new Angle(1,0);
            Angle angle2 = new Angle(0, 1);
            Angle angle3 = new Angle(-1, 0);
            Angle angle4 = new Angle(0, -1);
            
            
            angle1.GetRad().Should().BeApproximately(0,1e-07);
            angle2.GetRad().Should().BeApproximately(Angle.PI_2, 1e-07);
            angle3.GetRad().Should().BeApproximately(Angle.PI, 1e-07);
            angle4.GetRad().Should().BeApproximately(Angle.PI*3.0/2.0, 1e-07);
   
        }

        [TestMethod]
        public void AndgleShouldReturnCorrectRadForNE()
        {
            Angle angle1 = new Angle(1, 1);
            angle1.GetRad().Should().BeApproximately(Angle.PI_4, 1e-07);
        }

        [TestMethod]
        public void AndgleShouldReturnCorrectRadForNW()
        {
            Angle angle1 = new Angle(-1, 1);
            angle1.GetRad().Should().BeApproximately(Angle.PI_4*3, 1e-07);
        }


        [TestMethod]
        public void AndgleShouldReturnCorrectRadForSW()
        {
            Angle angle1 = new Angle(-1, -1);
            angle1.GetRad().Should().BeApproximately(Angle.PI_4*5, 1e-07);
        }

        [TestMethod]
        public void AndgleShouldReturnCorrectRadForSE()
        {
            Angle angle1 = new Angle(1, -1);
            angle1.GetRad().Should().BeApproximately(Angle.PI_4*7, 1e-07);
        }

      
        [TestMethod]
        public void Add90ShouldPoint30TO120()
        {
            Angle angle = new Angle(Angle.PI / 6.0);
            var angle2=angle.Add90();
            angle2.GetRad().Should().BeApproximately(Angle.PI * 4.0 / 6.0,1e-08);
        }

        [TestMethod]
        public void fluentAdd90ShouldPointNeg30TO60()
        {
            Angle angle = new Angle(-Angle.PI / 6.0);
            angle.fAdd90();
            angle.GetRad().Should().BeApproximately(Angle.PI / 3.0, 1e-08);
        }

        [TestMethod]
        public void Add60Plus30ShouldPointTO90()
        {
            Angle angle1 = new Angle(Angle.PI_3);
            Angle angle2 = new Angle(Angle.PI_6);

            Angle angle = angle1.Add(angle2);
            angle.GetRad().Should().BeApproximately(Angle.PI_2,1e-08);
        }

        [TestMethod]
        public void Neg60Plus30ShouldBe270()
        {
            Angle angle1 = new Angle(-Angle.PI_3);
            Angle angle2 = new Angle(Angle.PI_6);

            Angle angle = angle1.Add(angle2);
            angle.GetRad().Should().BeApproximately(2*Angle.PI-Angle.PI_6, 1e-08);
        }



    }
 
}
