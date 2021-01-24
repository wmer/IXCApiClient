using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class CallParameters {
        public string Qtype { get; set; }
        public string Query { get; set; }
        public Operadores Operador { get; set; }
        public string Page { get; set; }
        public string Rp { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }
        public List<GridParameter> GridParams { get; set; }
    }
}
