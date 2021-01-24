using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class SortOrder {
        private SortOrder(string value) { Value = value; }

        public string Value { get; set; }

        public static SortOrder Ascendente { get { return new SortOrder("asc"); } }
        public static SortOrder Descendente { get { return new SortOrder("desc"); } }
    }
}
