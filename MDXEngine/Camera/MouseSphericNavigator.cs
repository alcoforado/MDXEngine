using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine
{
    public class MouseSphericNavigator
    {
        Camera _camera;
        CameraShpericCoordinates _sCoordinates;
        System.Windows.Forms.Control _control;
        static readonly double PI2 = Math.PI * 2;

        float _dr;
        float _minR;
        float _minAlpha;
        double _dTheta;
        double _theta;
        double _dAlpha;
        double _alpha;
        double _maxAlpha;
        bool _bTurnUpsideDown;
        System.Drawing.Point _location; 
        public MouseSphericNavigator(Camera camera,bool bTurnUpsideDown)
        {
             _camera = camera;
             _dr = 1.0f;
             _minR = 1e-04f;
             _minAlpha = 1e-04f;
             _maxAlpha = Math.PI - _minAlpha;
             _dTheta = (float) (2*Math.PI / 360);
             _dAlpha = (float)(2 * Math.PI / 360);
             _control = null;
             _bTurnUpsideDown = bTurnUpsideDown;
        }

        private void OnMouseWheel(object Sender,System.Windows.Forms.MouseEventArgs e)
        {
            _sCoordinates.R -= Math.Round((float) e.Delta/120 * _dr);
            if (_sCoordinates.R < _minR)
                _sCoordinates.R = _minR;
            _camera.SetCamera(_sCoordinates);
            _camera.OrthonormalizeUp();
        }

        private void OnMousePress(object Sender,System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _location = e.Location;
            }
                
        }

        private void OnMouseMovement(object Sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var dist = new System.Drawing.Point();
                dist.X = e.Location.X - _location.X;
                dist.Y = e.Location.Y - _location.Y;
                _location = e.Location;

                
                _alpha -= _dAlpha * dist.Y;
                if (_bTurnUpsideDown)
                {
                    if (_alpha > PI2)
                    {
                        _alpha -= PI2;
                    }
                    else if (_alpha < -PI2)
                    {
                        _alpha += PI2;
                    }
                    else if (Math.Abs(_alpha) < _minAlpha)
                        _alpha = _minAlpha;
                    _sCoordinates.Alpha = new Geometry.Angle(_alpha);

                    double sign = (_sCoordinates.Alpha.Sin < 0) ? -1 : 1;
                    _sCoordinates.Up = new SharpDX.Vector3(0,0, (float) sign);

                    _theta -= sign* _dTheta * dist.X;
                    _sCoordinates.Theta = new Geometry.Angle(_theta);

                }
                else
                {
                   
                    if (_alpha < _minAlpha)
                        _alpha = _minAlpha;
                    if (_alpha > _maxAlpha)
                        _alpha = _maxAlpha;
                     _sCoordinates.Alpha = new Geometry.Angle(_alpha);
                     _theta -=  _dTheta * dist.X;
                     _sCoordinates.Theta = new Geometry.Angle(_theta);
                }
               
                
                _camera.SetCamera(_sCoordinates);
                _camera.OrthonormalizeUp();
            }

        }


        public void AttachControl(System.Windows.Forms.Control control)
        {
            _sCoordinates = _camera.GetCameraSphericCoordinates();
            _theta =  _sCoordinates.Theta.GetRad();
            _alpha = _sCoordinates.Alpha.GetRad();
            _sCoordinates.Up = new SharpDX.Vector3(0, 0, 1);
            _control = control;
            control.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
            control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMousePress);
            control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMovement);
        }

        public void DetachControl()
        {
            if (_control != null)
            {
                _control.MouseWheel -= new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
                _control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMousePress);
                _control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMovement);
            
            }
        }

    }
}
