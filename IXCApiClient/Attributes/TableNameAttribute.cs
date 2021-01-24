using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Attributes {
    [AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = true) ]
    public class TableNameAttribute : Attribute {
        string name;

        public TableNameAttribute(string name) {
            this.name = name;
        }

        public string GetName() {
            return name;
        }
    }
}
