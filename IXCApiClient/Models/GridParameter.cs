using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IXCApiClient.Models {
    public class GridParameter<T> {
        public Expression<Func<T, IComparable>> Property { get; set; }
        public Operadores Operador { get; set; }
        public string Valor { get; set; }
    }
}
