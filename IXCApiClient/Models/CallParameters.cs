using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IXCApiClient.Models {
    public class CallParameters<T> {
        public Expression<Func<T, IComparable>> Qtype { get; set; }
        public string Query { get; set; }
        public Operadores Operador { get; set; }
        public string Page { get; set; }
        public string Rp { get; set; }
        public Expression<Func<T, IComparable>> SortName { get; set; }
        public SortOrder SortOrder { get; set; }
        public List<GridParameter<T>> GridParams { get; set; }
    }
}
