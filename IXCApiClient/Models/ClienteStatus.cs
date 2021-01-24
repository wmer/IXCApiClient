using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class ClienteStatus {
        private ClienteStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static ClienteStatus Ativo { get { return new ClienteStatus("S"); } }
        public static ClienteStatus Inativo { get { return new ClienteStatus("N"); } }
        public static ClienteStatus Todos { get { return new ClienteStatus("Todos"); } }
    }
}
