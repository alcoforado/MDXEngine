using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;
using MDXEngine;
using FluentAssertions;
using Moq;
using MDXEngine.Geometry;
using MDXEngine.SharpDXExtensions;
namespace UnitTests
{
    [TestClass]
    public class CameraTests
    {
        #region TestCallToObservers

        static private  readonly float tol = 1e-6f;


        [TestMethod]
        public void CameraShouldCallObserverIfSetCameraIsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera(640,480);
            cam.AddObserver(observer.Object);

            cam.SetCamera(new Vector3(), new Vector3());

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));

        }

        [TestMethod]
        public void CameraShouldCallObserverIfSetCamera2IsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera(640,480);
            cam.AddObserver(observer.Object);

            cam.SetCamera(new Vector3(), new Vector3(), new Vector3());

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));

        }

        [TestMethod]
        public void CameraShouldCallObserverIfSetSetCamera2IsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera(640,480);
            cam.AddObserver(observer.Object);

            cam.SetCamera(new Vector3(), new Vector3(), new Vector3());

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));

        }

        [TestMethod]
        public void CameraShouldCallObserverIfFocusIsSet()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera(640,480);
            cam.AddObserver(observer.Object);

            cam.Focus = new Vector3();

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));
        }



        [TestMethod]
        public void CameraShouldCallObserverIfSetLensIsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera(640,480);
            cam.AddObserver(observer.Object);

            cam.SetLens(0, 0);

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));
        }



        [TestMethod]
        public void CameraShouldCallObserverIfSetCameraFromSphericCoordinatesIsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera(640,480);
            cam.AddObserver(observer.Object);

            cam.SetCamera(new CameraShpericCoordinates());

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));
        }


        [TestMethod]
        public void CameraShouldNotCallObserverIfObserverIsAddedAndDeleted()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera(640,480);
            cam.AddObserver(observer.Object);
            cam.RemoveObserver(observer.Object);

            cam.SetCamera(new CameraShpericCoordinates());



            observer.Invoking(obs => obs.Verify(x => x.CameraChanged(It.IsAny<Camera>()))).ShouldThrow<Exception>();
        }


        #endregion

        [TestMethod]
        public void CameraShouldPassUpAndFocusToSphericCoordinates()
        {
            Vector3 pos = new Vector3(2, 2, 2);
            Vector3 up = new Vector3(1, 2, 3);
            Vector3 focus = new Vector3(4, 5, 6);

            var cam = new Camera(640,480);
            cam.SetCamera(
                new Vector3(2, 2, 2),
                up,
                focus
                );

            var coordinates = cam.GetCameraSphericCoordinates();
            // cam.SetCamera(new Vector3(1.0),new Vector3(0.0) )

            (coordinates.Up - up).Norm().Should().BeApproximately(0f, 1e-6f);
            (coordinates.Focus - focus).Norm().Should().BeApproximately(0f, 1e-6f);


        }



        [TestMethod]
        public void CameraShouldPassRAsNormOfPosMinusFocus()
        {
            Vector3 pos = new Vector3(1, 2, 5);
            Vector3 up = new Vector3(1, 2, 3);
            Vector3 focus = new Vector3(4, 5, 6);
            var cam = new Camera(640,480);
            cam.SetCamera(
                pos,
                up,
                focus
                );
            var coordinates = cam.GetCameraSphericCoordinates();
            coordinates.R.Should().BeApproximately((pos - focus).Norm(), 1e-6f);
        }

        [TestMethod]
        public void CameraShouldPassThetaAngleCorrectly()
        {
            Vector3 pos = new Vector3(4 + (float)Math.Sqrt(3), 6, 5);
            Vector3 up = new Vector3(1, 2, 3);
            Vector3 focus = new Vector3(4, 5, 6);
            var cam = new Camera(640,480);
            cam.SetCamera(
                pos,
                up,
                focus
                );
            var coordinates = cam.GetCameraSphericCoordinates();
            coordinates.Theta.GetRad().Should().BeApproximately(Angle.PI_6, 1e-6f);
        }

        [TestMethod]
        public void CameraShouldPassAlphaAngleCorrectly()
        {
            Vector3 pos = new Vector3(5, 7, 6 + (float)Math.Sqrt(5));
            Vector3 up = new Vector3(1, 2, 3);
            Vector3 focus = new Vector3(4, 5, 6);
            var cam = new Camera(640,480);
            cam.SetCamera(
                pos,
                up,
                focus
                );
            var coordinates = cam.GetCameraSphericCoordinates();
            coordinates.Alpha.GetRad().Should().BeApproximately(Angle.PI_4, 1e-6f);
        }

        [TestMethod]
        public void CameraShouldSetPosCorrectlyForSphericCoordinates()
        {
            var cd = new CameraShpericCoordinates
            {
                Alpha = new Angle(Angle.PI_4),
                Theta = new Angle(Angle.PI_4),
                R = 2,
                Up = new Vector3(2, 5, 87),
                Focus = new Vector3(4, 5, 9)
            };
            var cam = new Camera(640,480);
            cam.SetCamera(cd);

            (cam.Pos - new Vector3(5, 6, 9 + (float)Math.Sqrt(2))).Norm().Should().BeApproximately(0.0f, 1e-6f);
        }

         [TestMethod]
        public void TransformToNormalCoordinatesAndComeBackIsAnIdentityOperation()
        {
            float tol = 1e-6f;
            var cd = new CameraShpericCoordinates
            {
                Alpha = new Angle(Angle.PI_6),
                Theta = new Angle(Angle.PI_2),
                R = 5,
                Up = new Vector3(2, 5, 87),
                Focus = new Vector3(4, 6, 19)
            };
            var cam = new Camera(640,480);
            cam.SetCamera(cd);

            var cd2 = cam.GetCameraSphericCoordinates();

            cd.Up.AEqual(cd2.Up, tol).Should().BeTrue();
            cd.Focus.AEqual(cd2.Focus, tol).Should().BeTrue();
            cd.R.Should().BeApproximately(cd2.R,tol);
            cd.Alpha.GetRad().Should().BeApproximately(cd2.Alpha.GetRad(),tol);
            cd.Theta.GetRad().Should().BeApproximately(cd2.Theta.GetRad(),tol);
        }

        [TestMethod]
        public void OrthogonizeUpShouldWork()
         {

             var cam = new Camera(640,480);
             cam.SetCamera(new Vector3(1,3,4), new Vector3(0, 0, 1));
             cam.OrthonormalizeUp();

             cam.Up.Norm().Should().BeApproximately(1f, tol);
             cam.Up.Dot(cam.Pos).Should().BeApproximately(0f, tol);

         }

        [TestMethod]
        public void OrthogonizeUpShouldWork2()
        {

            var cam = new Camera(640,480);
            cam.SetCamera(new Vector3(1, 3, 4), new Vector3(2, 7, 1));
            cam.OrthonormalizeUp();

            cam.Up.Norm().Should().BeApproximately(1f, tol);
            cam.Up.Dot(cam.Pos).Should().BeApproximately(0f, tol);

        }

        [TestMethod]
        public void OrthogonizeWithNonZeroFocusShouldWork()
        {

            var cam = new Camera(640,480);
            cam.SetCamera(new Vector3(1, 3, 4), new Vector3(2, 7, 1),new Vector3(4,7,2));
            cam.OrthonormalizeUp();

            cam.Up.Norm().Should().BeApproximately(1f, tol);
            cam.Up.Dot(cam.Pos - cam.Focus).Should().BeApproximately(0f, tol);

        }


    }
}
