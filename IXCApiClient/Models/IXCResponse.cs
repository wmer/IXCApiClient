using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class IXCResponse<T> {
        public string page { get; set; }
        public string total { get; set; }
        public List<T> registros { get; set; }
    }
}
