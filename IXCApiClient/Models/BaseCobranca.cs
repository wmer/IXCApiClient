using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class BaseCobranca {
        public string CodigoCliente { get; set; }
        public string CNPJ_CNPF { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string TelCelular { get; set; }
        public string TelComercial { get; set; }
        public string TelResidencial { get; set; }
        public string WhatsApp { get; set; }
        public string Email { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public string Documento { get; set; }
        public string DocStatus { get; set; }
        public string Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public string ValorPago { get; set; }
        public DateTime Pagamento { get; set; }
        public string LinhaDigitavel { get; set; }
        public DateTime DataAtivacao { get; set; }
        public string CarteiraCobranca { get; set; }
        public string CodigoContrato { get; set; }
        public string ClienteStatusAtivo { get; set; }
        public string ContratoStatus { get; set; }
        public string VendedorId { get; set; }
    }
}
