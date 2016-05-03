using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUtils.Reflection
{
    static public class Extension
    {
        //Go throw all public properties of src and assign to dst
        //if the values are not null.
        public static void CopyPublicPropertiesFrom(this object dst, object src)
        {

            var srcProperties = TypeDescriptor.GetProperties(src.GetType()).Cast<PropertyDescriptor>();
            var dstProperties = TypeDescriptor.GetProperties(dst.GetType()).Cast<PropertyDescriptor>();

            foreach (var dstP in dstProperties)
            {
                var srcP = srcProperties.FirstOrDefault(prop => prop.Name == dstP.Name);
               
                if (srcP != null )
                {
                    var value = srcP.GetValue(src);
                    if (value!=null)
                        dstP.SetValue(dst, Convert.ChangeType(value, dstP.PropertyType));
                }
            
            }

        }

    }
}
