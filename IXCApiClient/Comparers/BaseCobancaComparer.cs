using IXCApiClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IXCApiClient.Comparers {
    public class BaseCobancaComparer : IEqualityComparer<BaseCobranca> {
        public bool Equals([AllowNull] BaseCobranca x, [AllowNull] BaseCobranca y) {
            return x.Documento == y.Documento &&
                        x.Valor == y.Valor &&
                        x.Vencimento == y.Vencimento;
        }

        public int GetHashCode([DisallowNull] BaseCobranca obj) {
            return obj.Documento.GetHashCode() ^
                        obj.Valor.GetHashCode() ^
                        obj.Vencimento.GetHashCode();
        }
    }
}
