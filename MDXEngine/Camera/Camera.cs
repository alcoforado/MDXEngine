using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using MDXEngine.SharpDXExtensions;
using MDXEngine.Geometry;
namespace MDXEngine
{

    public interface ICameraObserver
    {
        /// <summary>
        /// Event called everytime the camera changes
        /// </summary>
        /// <param name="camera"></param>
        void CameraChanged(Camera camera);
        //void CameraReleased();
    }

    public struct CameraShpericCoordinates
    {
        //spheric coordinates
       public  double R;
       public  Angle Alpha;
       public  Angle Theta;
       public  Vector3 Up;
       public  Vector3 Focus;
    }

    public class Camera
    {
        private List<ICameraObserver> _observers;

        Vector3 _pos;
        Vector3 _up;
        Vector3 _focus;

        bool _bUpdateWorldMatrix;

        Matrix proj; //The projection matrix
        
        
                
        //Matrix view; //The final view Matrix

        public Camera(int  viewportX,int viewportY)
        {
            _observers = new List<ICameraObserver>();
            SetLens(viewportX,viewportY);
        }

        public void SetCamera(Vector3 pos, Vector3 up)
        {
            _pos = pos;
            _up = up;
            _focus.Set(0);
            OnCameraChange();
        }

        public void SetCamera(Vector3 pos, Vector3 up,Vector3 focus)
        {
            _pos = pos; 
            _up = up;
            _focus = focus;
            OnCameraChange();
        }


#region Properties Access 

        public Vector3 Focus
        {
            get { return _focus; } 
            
            set {
                _focus = value;
                OnCameraChange();
            }
        }

        public Vector3 Pos
        {
            get
            {
                return _pos;
            }
            private set
            {
                _pos = value;
                OnCameraChange();
            }
        }
        public Vector3 Up
        {
            get
            {
                return _up;
            }
            private set
            {
                _up = value;
                OnCameraChange();
            }
        }
     
#endregion      


        public void SetCamera(CameraShpericCoordinates s)
        {
            _pos.X=(float)(s.R *s.Theta.Cos * s.Alpha.Sin);
            _pos.Y=(float)(s.R *s.Theta.Sin*  s.Alpha.Sin);
            _pos.Z=(float)(s.R *s.Alpha.Cos);
            _pos+=s.Focus;

            _up = s.Up;
            _focus = s.Focus;
            OnCameraChange();
        }

        public CameraShpericCoordinates GetCameraSphericCoordinates()
        {
            CameraShpericCoordinates result;

            Vector3 r = _pos - _focus;
            var XYNorm2 = r.X*r.X + r.Y*r.Y;
            var ZXYNorm2 = r.Z * r.Z + XYNorm2;

            result.R =  Math.Sqrt(ZXYNorm2);
            result.Theta = new Angle(r.X,r.Y);
            result.Alpha = new Angle(r.Z,Math.Sqrt(XYNorm2));
            result.Focus = _focus;
            result.Up = _up;

            return result;
        }


     


        private void UpdateSphericCoordinatesFromCamera()
        {
        }

       
        public void OrthonormalizeUp()
        {
            var v = new Vector3();
            v = _pos - _focus;
            float c=Vector3.Dot(v, _up)/((float) v.Norm2());
            _up = _up - c*v;
            _up.Normalize();
        }

        public Matrix GetWorldViewMatrix()
        {
            return Matrix.Multiply(Matrix.LookAtRH(_pos, _focus, _up),proj);
        }

        public Matrix GetWorldMatrix()
        {
             return Matrix.LookAtRH(_pos, _focus, _up);
        }

        public void SetLens(int X,int Y)
        {
            proj = Matrix.Identity;
            proj=Matrix.PerspectiveFovRH((float)Math.PI / 4.0f, (float) X / (float) Y, 0.1f, 100.0f);
            OnCameraChange();
        }

       

        private void OnCameraChange()
        {
            _bUpdateWorldMatrix = true;
            foreach(var obs in _observers)
            {
                obs.CameraChanged(this);
            }
        }
       

        public void AddObserver(ICameraObserver obs)
        {
            if (!_observers.Contains(obs))
                _observers.Add(obs);
        }

        public void RemoveObserver(ICameraObserver obs)
        {
            if (_observers.Contains(obs))
                _observers.Remove(obs);
        }

    }
}
