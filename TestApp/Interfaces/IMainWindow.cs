using System;
namespace TestApp
{
    interface IMainWindow
    {
        System.Windows.Forms.MenuStrip GetMenus();
        System.Windows.Forms.Control RenderControl();
        void SetDxApp(DxApp app);
    }
}
