using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.ShapesManagerService
{
   public  class UIType
   {
       public string TypeName;
       public List<UITypeMember> Members;
   }

    public class UITypeMember
    {
        public string FieldName { get; set; }
        public string LabelName { get; set; }
        public string DirectiveType { get; set; }
   }

}
