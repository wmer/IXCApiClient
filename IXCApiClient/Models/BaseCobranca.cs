using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IXCApiClient.Models {
    public class BaseCobranca : IEquatable<BaseCobranca> {
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
        public string IDCondominio { get; set; }
        public string FilialId { get; set; }
        public string PlanoId { get; set; }
        public string Plano { get; set; }
        public string StatusAcesso { get; set; }
        public string TipoCliente { get; set; }
        public string MotivoCancelamento { get; set; }
        public DateTime DataCancelamento { get; set; }
        public DateTime DataDesativacao { get; set; }
        public string CondicaoPagamento { get; set; }
        public string TipoPagamento { get; set; }
        public string DescricaoFatura { get; set; }
        public string ValorAberto { get; set; }
        public string IDRenogociacao { get; internal set; }
        public string IDRenogociacaoNovo { get; internal set; }

        public bool Equals([AllowNull] BaseCobranca other) {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Documento == other.Documento &&
                        Valor == other.Valor &&
                        Vencimento == other.Vencimento;

        }
    }
}
