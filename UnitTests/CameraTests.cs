using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;
using MDXEngine;
using FluentAssertions;
using Moq;
using MDXEngine.Geometry;

namespace UnitTests
{
    [TestClass]
    public class CameraTests
    {
        #region TestCallToObservers

        [TestMethod]
        public void CameraShouldCallObserverIfSetCameraIsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);

            cam.SetCamera(new Vector3(), new Vector3());

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));

        }

        [TestMethod]
        public void CameraShouldCallObserverIfSetCamera2IsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);

            cam.SetCamera(new Vector3(), new Vector3(), new Vector3());

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));

        }

        [TestMethod]
        public void CameraShouldCallObserverIfSetSetCamera2IsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);

            cam.SetCamera(new Vector3(), new Vector3(), new Vector3());

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));

        }

        [TestMethod]
        public void CameraShouldCallObserverIfFocusIsSet()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);

            cam.Focus = new Vector3();

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));
        }



        [TestMethod]
        public void CameraShouldCallObserverIfSetLensIsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);

            cam.SetLens(0, 0);

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));
        }

        [TestMethod]
        public void CameraShouldCallObserverIfMoveSphericIsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);

            cam.MoveSpheric(0f, 0f, 0f);

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));
        }

        [TestMethod]
        public void CameraShouldCallObserverIfSetCameraFromSphericCoordinatesIsCalled()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);

            cam.SetCameraFromSphericCoordinates(0.0, new Angle(0.0), new Angle(0.0));

            observer.Verify(x => x.CameraChanged(It.IsAny<Camera>()));
        }


        [TestMethod]
        public void CameraShouldNotCallObserverIfObserverIsAddedAndDeleted()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
            cam.AddObserver(observer.Object);
            cam.RemoveObserver(observer.Object);
            cam.SetCameraFromSphericCoordinates(0.0, new Angle(0.0), new Angle(0.0));

       

            observer.Invoking( obs => obs.Verify(x => x.CameraChanged(It.IsAny<Camera>()))).ShouldThrow<Exception>();
        }


        #endregion

        [TestMethod]
        public void CameraShouldUpdateSphericCoordinatesWhenChangedNormalCoordinates()
        {
            var observer = new Mock<ICameraObserver>();
            var cam = new Camera();
          // cam.SetCamera(new Vector3(1.0),new Vector3(0.0) )



            observer.Invoking(obs => obs.Verify(x => x.CameraChanged(It.IsAny<Camera>()))).ShouldThrow<Exception>();
        }
    
        

    
    }
}
