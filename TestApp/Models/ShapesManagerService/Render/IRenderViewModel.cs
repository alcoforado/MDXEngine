namespace TestApp.Models.ShapesManagerService.Render
{
    public interface IRenderViewModel
    {

        string GetPainterName();

        string Id { get; set; }

        object CreateRender();

        System.Type GetShaderType();
    }
}
