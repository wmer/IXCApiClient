using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class Operadores {
        private Operadores(string value) { Value = value; }

        public string Value { get; set; }

        public static Operadores Igual { get { return new Operadores("="); } }
        public static Operadores MaiorQue { get { return new Operadores(">"); } }
        public static Operadores MenorQue { get { return new Operadores("<"); } }
        public static Operadores MaiorIgaul { get { return new Operadores(">="); } }
        public static Operadores MenorIgual { get { return new Operadores("<="); } }
        public static Operadores Diferente { get { return new Operadores("!="); } }
    }
}
