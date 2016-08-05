using System;
using TestApp.Models.ShapesManagerService;

namespace TestApp.Mappers
{
   public  interface IShapesMngrMapper
    {
        string GetLabelNameFromFieldName(string fieldName);
        string MapTypeWithJavascriptRender(Type propertyType);
        ShapeType ToShapeTypeDto(string name, Type type);
    }
}