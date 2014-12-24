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
        //double _r, _alpha, _theta;
        Matrix proj; //The projection matrix
        //Matrix view; //The final view Matrix

        public Vector3 Up { get { return _up; } }


        public void SetCamera(Vector3 pos, Vector3 up)
        {
            _pos = pos;
            _up = up;
            _focus.Set(0);
            OnCameraChange();
        }
        
        /*
        public float getR() { return (float) _r; }
        public double getAlpha() { return _alpha; }
        public double getTheta() { return _theta; }

        public void setCamera(double r, double theta, double alfa)
        {
            _alpha = alfa;
            _theta = theta;
            _r = r;
            
            double sin_alpha=Math.Sin(alfa);
            double cos_alpha=Math.Cos(alfa);
            double sin_theta=Math.Sin(theta);
            double cos_theta=Math.Cos(theta);
            double cos_alpha_90 = sin_alpha;
            double sin_alpha_90 = -cos_alpha;

            _pos.X=(float)(r * cos_theta * sin_alpha);
            _pos.Y = (float)(r * sin_theta * sin_alpha);
            _pos.Z=(float)(r * cos_alpha);

            _up.X = (float)(r * cos_theta * sin_alpha_90);
            _up.Y = (float)(r * sin_theta * sin_alpha_90);
            _up.Z = (float)(r * cos_alpha_90);

            //DXApp.log.WriteLine("{0} {1}", _pos, _up);
           
            camChanged = true;
        }

        public void moveSpheric(float d_theta, float d_alpha)
        {
            _alpha += d_alpha;
            _theta += d_theta;

            setCamera(_r, _theta, _alpha);

        }

        public void moveRadius(float dr)
        {
            _r += dr;
            if (_r < 0f)
                _r = 0f;
            setCamera(_r, _theta, _alpha);
        }
        */
        public Matrix getWorldViewMatrix()
        {
               return Matrix.Multiply(Matrix.LookAtRH(_pos, _focus, _up),proj);
        }

        public Matrix getWorldMatrix()
        {
            return Matrix.LookAtRH(_pos, _focus, _up);
            

        }

        public void setLens(int X,int Y)
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
