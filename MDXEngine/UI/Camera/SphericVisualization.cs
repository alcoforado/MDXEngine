using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using MDXEngine.Geometry;
namespace MDXEngine
{
    public class SphericVisualization 
    {
        private readonly float Alpha_Min = 1e-5f;
        private readonly float R_Min = 1e-4f;
        Camera _cam;
        CameraShpericCoordinates _sphericCoordinates;
        float _dr;
        Angle _dTheta;
        Angle _dAlpha;
        public SphericVisualization(Camera camera, Vector3 focus, float dr, float dThetaRad, float dAlphaRad)
        {
            _cam = camera;
            _cam.Focus = focus;
            _sphericCoordinates = _cam.GetCameraSphericCoordinates();
            _dr = dr;
            _dTheta = new Angle(dThetaRad);
            _dAlpha = new Angle(dAlphaRad);
            UpdateCamera();
        }

        public void UpdateCamera()
        {
            if (Math.Abs(_sphericCoordinates.Theta.GetRad()) < Alpha_Min)
            {
                _sphericCoordinates.Theta = new Angle(Alpha_Min);

            }
            _sphericCoordinates.Up = new Vector3(0, 0, 1);
            _cam.SetCamera(_sphericCoordinates);
            _cam.OrthonormalizeUp();
        }

        public void MoveFar()
        {
            _sphericCoordinates.R += this._dr;
        }

        public void MoveNear()
        {
            _sphericCoordinates.R -= this._dr;
            if (Math.Abs(_sphericCoordinates.R) < R_Min)
            {
                _sphericCoordinates.R = R_Min;
            }

        }
        public void MoveRight()
        {
            _sphericCoordinates.Theta = _sphericCoordinates.Theta.Add(_dTheta);
        }

        public void MoveLeft()
        {
            _sphericCoordinates.Theta = _sphericCoordinates.Theta.Sub(_dTheta);
        }

        public void MoveUp()
        {
            _sphericCoordinates.Alpha = _sphericCoordinates.Alpha.Sub(_dAlpha);
        }

        public void MoveDown()
        {
            _sphericCoordinates.Alpha = _sphericCoordinates.Alpha.Add(_dAlpha);
            
        }
    }
}
