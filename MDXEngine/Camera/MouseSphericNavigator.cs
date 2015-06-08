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

        float _dr;
        public MouseSphericNavigator(Camera camera)
        {
             _camera = camera;
             _dr = 1.0f;      

        }

        private void OnMouseWheel(object Sender,System.Windows.Forms.MouseEventArgs e)
        {
            _sCoordinates.R = Math.Round((float) e.Delta * _dr);
            _camera.SetCamera(_sCoordinates);
        }


        void AttachControl(System.Windows.Forms.Control control)
        {
            _sCoordinates = _camera.GetCameraSphericCoordinates();

            control.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
        }

        void DetachControl(System.Windows.Forms.Control control)
        {
            control.MouseWheel -= new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
        }

    }
}
