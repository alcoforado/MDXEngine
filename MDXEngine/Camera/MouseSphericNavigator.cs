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
        float _dr;
        public MouseSphericNavigator(Camera camera)
        {
             _camera = camera;
             _dr = 1.0f;
             _control = null;
        }

        private void OnMouseWheel(object Sender,System.Windows.Forms.MouseEventArgs e)
        {
            _sCoordinates.R -= Math.Round((float) e.Delta/120 * _dr);
            _camera.SetCamera(_sCoordinates);
            _camera.OrthonormalizeUp();
        }


        public void AttachControl(System.Windows.Forms.Control control)
        {
            _sCoordinates = _camera.GetCameraSphericCoordinates();
            _sCoordinates.Up = new SharpDX.Vector3(0, 0, 1);
            _control = control;
            control.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
        }

        public void DetachControl()
        {
            if (_control != null)
                _control.MouseWheel -= new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
        }

    }
}
