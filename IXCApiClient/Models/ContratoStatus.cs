using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class ContratoStatus {
        private ContratoStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static ContratoStatus Ativo { get { return new ContratoStatus("A"); } }
        public static ContratoStatus Inativo { get { return new ContratoStatus("I"); } }
        public static ContratoStatus PreContrato { get { return new ContratoStatus("P"); } }
        public static ContratoStatus Negativado { get { return new ContratoStatus("N"); } }
        public static ContratoStatus Desistente { get { return new ContratoStatus("D"); } }
        public static ContratoStatus Todos { get { return new ContratoStatus("Todos"); } }
    }
}
