using IXCApiClient.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {

    [TableName("fn_areceber")]
    public class Titulo {
        public string id_remessa { get; set; }
        public string gateway_link { get; set; }
        public string nn_boleto { get; set; }
        public string boleto { get; set; }
        public string id { get; set; }
        public string liberado { get; set; }
        public string id_conta { get; set; }
        public string filial_id { get; set; }
        public string status { get; set; }
        public string data_emissao { get; set; }
        public string data_vencimento { get; set; }
        public string valor { get; set; }
        public string obs { get; set; }
        public string valor_recebido { get; set; }
        public string valor_aberto { get; set; }
        public string id_cliente { get; set; }
        public string pagamento_valor { get; set; }
        public string pagamento_data { get; set; }
        public string id_carteira_cobranca { get; set; }
        public string credito_data { get; set; }
        public string baixa_data { get; set; }
        public string numero_parcela_recorrente { get; set; }
        public string documento { get; set; }
        public string id_saida { get; set; }
        public string tipo_recebimento { get; set; }
        public string tipo_renegociacao { get; set; }
        public string valor_cancelado { get; set; }
        public string data_cancelamento { get; set; }
        public string id_mot_cancelamento { get; set; }
        public string id_renegociacao { get; set; }
        public string id_cobranca { get; set; }
        public string previsao { get; set; }
        public string id_renegociacao_novo { get; set; }
        public string libera_periodo { get; set; }
        public string impresso { get; set; }
        public string forma_recebimento { get; set; }
        public string arquivo_remessa_baixado { get; set; }
        public string nparcela { get; set; }
        public string tipo_cobranca { get; set; }
        public string status_cobranca { get; set; }
        public string id_contrato_principal { get; set; }
        public string id_contrato_avulso { get; set; }
        public string id_contrato { get; set; }
        public string parcela_proporcional { get; set; }
        public string id_nota_gerada { get; set; }
        public string linha_digitavel { get; set; }
        public string id_im_imovel { get; set; }
        public string tipo_pagamento_cartao { get; set; }
        public string titulo_protestado { get; set; }
        public string duplicata { get; set; }
        public string id_sip { get; set; }
        public string gerencianet_token { get; set; }
        public string motivo_alteracao { get; set; }
        public string id_remessa_alteracao { get; set; }
        public string cancelamento_id_operador { get; set; }
        public string baixa_id_operador { get; set; }
        public string titulo_importado { get; set; }
        public string origem_importacao { get; set; }
        public string ultima_atualizacao { get; set; }
    }
}
