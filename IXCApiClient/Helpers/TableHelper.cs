using IXCApiClient.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Helpers {
    public class TableHelper {
        public static string GetTableName<T>() {
            var name = "";
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(T));

            // Displaying output.  
            foreach (System.Attribute attr in attrs) {
                if (attr is TableNameAttribute) {
                    TableNameAttribute a = (TableNameAttribute)attr;
                    name = a.GetName();
                }
            }

            return name;
        }
    }
}
