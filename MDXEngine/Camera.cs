using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using MDXEngine.SharpDXExtensions;

namespace MDXEngine
{

    public interface ICameraObserver
    {
        void CameraChanged(Camera camera);
        void CameraReleased();
    }


    public class Camera
    {
        private List<ICameraObserver> _observers;

        Vector3 _pos;
        Vector3 _up;
        Vector3 _focus;
        
        //spheric coordinates
        double _r, _alpha, _theta;
        

        //boolean to keep track wich coordinates need updates
        bool _bNormalCoordinatesNeedUpdate;
        bool _bSphericCoordinatesNeedUpdate;

        Matrix proj; //The projection matrix
        
        
                
        //Matrix view; //The final view Matrix

        public Camera()
        {
            _observers = new List<ICameraObserver>();
        }

        public void SetCamera(Vector3 pos, Vector3 up)
        {
            _pos = pos;
            _up = up;
            _focus.Set(0);
            _bSphericCoordinatesNeedUpdate = true;
            OnCameraChange();
        }

        public void SetCamera(Vector3 pos, Vector3 up,Vector3 focus)
        {
            _pos = pos; 
            _up = up;
            _focus = focus;
            _bSphericCoordinatesNeedUpdate = true;
            OnCameraChange();
        }


#region Properties Access 

        public Vector3 Focus
        {
            get { return _focus; } 
            
            set { _focus = value; OnCameraChange();}
        }

        public Vector3 Pos
        {
            get
            {
                if (_bNormalCoordinatesNeedUpdate)
                    UpdateCameraFromSphericCoordinates();
                return _pos;
            }
            private set
            {
                if (_bNormalCoordinatesNeedUpdate)
                    UpdateCameraFromSphericCoordinates();
                _bSphericCoordinatesNeedUpdate = true;
                _pos = value;
                OnCameraChange();
            }
        }
        public Vector3 Up
        {
            get
            {
                if (_bNormalCoordinatesNeedUpdate)
                    UpdateCameraFromSphericCoordinates();
                return _up;
            }
            private set
            {
                if (_bNormalCoordinatesNeedUpdate)
                    UpdateCameraFromSphericCoordinates();
                _bSphericCoordinatesNeedUpdate = true;
                _up = value;
                OnCameraChange();
            }
        }
        public double R
        {
            get
            {
                if (_bSphericCoordinatesNeedUpdate)
                    UpdateSphericCoordinatesFromCamera();
                return _r;

            }
            private set
            {
                if (_bSphericCoordinatesNeedUpdate)
                    UpdateSphericCoordinatesFromCamera();
                _bNormalCoordinatesNeedUpdate = true;
                _r = value;
                OnCameraChange();
            }
        }
        public double Alpha
        {
            get
            {
                if (_bSphericCoordinatesNeedUpdate)
                    UpdateSphericCoordinatesFromCamera();
                return _alpha;
            }
            private set
            {
                if (_bSphericCoordinatesNeedUpdate)
                    UpdateSphericCoordinatesFromCamera();
                _bNormalCoordinatesNeedUpdate = true;
                _alpha = value;
                OnCameraChange();
            }
        }
        public double Theta
        {
            get
            {
                if (_bSphericCoordinatesNeedUpdate)
                    UpdateSphericCoordinatesFromCamera();
                return _theta;
            }
            private set
            {
                if (_bSphericCoordinatesNeedUpdate)
                    UpdateSphericCoordinatesFromCamera();
                _bNormalCoordinatesNeedUpdate = true;
                _theta = value;
                OnCameraChange();
            }
        }
        
#endregion      

        public void SetCameraFromSphericCoordinates(double r, double alpha, double theta)
        {

            _alpha = alpha;
            _theta = theta;
            _r = r;
            UpdateCameraFromSphericCoordinates();
            _bSphericCoordinatesNeedUpdate = false;
            _bNormalCoordinatesNeedUpdate = false;
            OnCameraChange();
        }

        private void UpdateCameraFromSphericCoordinates()
        {
            
            double sin_alpha=Math.Sin(_alpha);
            double cos_alpha=Math.Cos(_alpha);
            double sin_theta=Math.Sin(_theta);
            double cos_theta=Math.Cos(_theta);
            double cos_alpha_90 = sin_alpha;
            double sin_alpha_90 = -cos_alpha;

            _pos.X=(float)(_r * cos_theta * sin_alpha);
            _pos.Y = (float)(_r * sin_theta * sin_alpha);
            _pos.Z=(float)(_r * cos_alpha);
            _pos+=_focus;

            _up.X = (float)(_r * cos_theta * sin_alpha_90);
            _up.Y = (float)(_r * sin_theta * sin_alpha_90);
            _up.Z = (float)(_r * cos_alpha_90);

            //DXApp.log.WriteLine("{0} {1}", _pos, _up);
           
            _bNormalCoordinatesNeedUpdate=false;
            
        }


        private void UpdateSphericCoordinatesFromCamera()
        {
            
            double sin_alpha=Math.Sin(_alpha);
            double cos_alpha=Math.Cos(_alpha);
            double sin_theta=Math.Sin(_theta);
            double cos_theta=Math.Cos(_theta);
            double cos_alpha_90 = sin_alpha;
            double sin_alpha_90 = -cos_alpha;

            _pos.X=(float)(_r * cos_theta * sin_alpha);
            _pos.Y= (float)(_r * sin_theta * sin_alpha);
            _pos.Z=(float)(_r * cos_alpha);
            _pos+=_focus;

            _up.X = (float)(_r * cos_theta * sin_alpha_90);
            _up.Y = (float)(_r * sin_theta * sin_alpha_90);
            _up.Z = (float)(_r * cos_alpha_90);

            //DXApp.log.WriteLine("{0} {1}", _pos, _up);
           
            _bNormalCoordinatesNeedUpdate=false;
            _bSphericCoordinatesNeedUpdate=false;
            
        }



        public void MoveSpheric(float dr,float d_theta, float d_alpha)
        {
            Alpha += d_alpha;
            Theta += d_theta;
            R += dr;
            if (R < 0f)
                R = 0f;
        }

        public void OrthonormalizeUp()
        {
            var v = new Vector3();
            v = _pos - _focus;
            float c=Vector3.Dot(v, _up)/((float) Math.Sqrt(v.Norm2()));
            _up = _up - c*v;
            _up.Normalize();
        }

        public Matrix GetWorldViewMatrix()
        {
            if (_bNormalCoordinatesNeedUpdate)
                this.UpdateCameraFromSphericCoordinates();
            return Matrix.Multiply(Matrix.LookAtRH(_pos, _focus, _up),proj);
        }

        public Matrix GetWorldMatrix()
        {
            if (_bNormalCoordinatesNeedUpdate)
                this.UpdateCameraFromSphericCoordinates();
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
