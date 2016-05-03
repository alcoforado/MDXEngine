namespace TestApp.Models.ShapesManagerService.Render
{
    public abstract class RenderBaseViewModel
    {
        public abstract string GetPainterName();


        public abstract object AttachToShader(MDXEngine.IDxViewControl _dx, MDXEngine.ITopology topology);

        public abstract void DetachFromShader(MDXEngine.IDxViewControl _dx, object p);
    }
}
