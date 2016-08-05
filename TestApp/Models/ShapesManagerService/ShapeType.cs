using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.ShapesManagerService
{
   public  class ShapeType
   {
       public string TypeName;
       public List<ShapeMember> Members;
   }

    public class ShapeMember
    {
        public string FieldName { get; set; }
        public string LabelName { get; set; }
        public string DirectiveType { get; set; }
   }

}
