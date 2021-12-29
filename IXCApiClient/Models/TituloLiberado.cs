using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class TituloLiberado {
        private TituloLiberado(string value) { Value = value; }

        public string Value { get; set; }

        public static TituloLiberado Sim { get { return new TituloLiberado("S"); } }
        public static TituloLiberado Nao { get { return new TituloLiberado("N"); } }
        public static TituloLiberado Todos { get { return new TituloLiberado("Todos"); } }
    }
}
