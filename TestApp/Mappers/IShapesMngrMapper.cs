using System;
using TestApp.Models.ShapesManagerService;

namespace TestApp.Mappers
{
   public  interface IShapesMngrMapper
    {
        string GetLabelNameFromFieldName(string fieldName);
        string MapTypeWithJavascriptRender(Type propertyType);
        UIType ToUITypeDto(string name, Type type);
    }
}