using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.ShapesManagerService;

namespace TestApp.Mappers
{
    class ShapesMngrMapper : IShapesMngrMapper
    { 
   
        public string GetLabelNameFromFieldName(string fieldName)
        {


            var array = fieldName.ToCharArray();
            array[0] = Char.ToUpper(array[0]);
            var result = new List<char>();
            foreach (var ch in array)
            {
                if (Char.IsUpper(ch))
                {
                    result.Add(' ');

                }
                result.Add(ch);

            }
            var str = result.ToArray().ToString().Trim();
            return str;

        }

        public UIType ToUITypeDto(string name, Type type)
        {

            var tt = type.GetProperties().Select(p =>

                new UITypeMember()
                {
                    FieldName = p.Name,
                    LabelName = this.GetLabelNameFromFieldName(p.Name),
                    DirectiveType = this.MapTypeWithJavascriptRender(p.PropertyType)

                }
            ).ToList();
            return new UIType()
            {
                Members = tt,
                TypeName = name
            };

        }

        public string MapTypeWithJavascriptRender(Type propertyType)
        {
            throw new NotImplementedException();
        }



    }
}
