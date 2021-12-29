using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class TituloStatus {
        private TituloStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static TituloStatus AReceber { get { return new TituloStatus("A"); } }
        public static TituloStatus Parcial { get { return new TituloStatus("P"); } }
        public static TituloStatus Cancelado { get { return new TituloStatus("C"); } }
        public static TituloStatus Pago { get { return new TituloStatus("R"); } }
        public static TituloStatus Todos { get { return new TituloStatus("Todos"); } }
    }
}
