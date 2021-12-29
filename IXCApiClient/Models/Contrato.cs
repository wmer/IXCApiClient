using IXCApiClient.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    [TableName("cliente_contrato")]
    public class Contrato {

        public string id_instalador { get; set; }
        public string indicacao_contrato_id { get; set; }
        public string id { get; set; }
        public string id_filial { get; set; }
        public string status { get; set; }
        public string status_internet { get; set; }
        public string id_cliente { get; set; }
        public string data_ativacao { get; set; }
        public string data { get; set; }
        public string data_renovacao { get; set; }
        public string id_vd_contrato { get; set; }
        public string tipo_condicao_pag { get; set; }
        public string bloqueio_automatico { get; set; }
        public string tipo { get; set; }
        public string imp_carteira { get; set; }
        public string contrato { get; set; }
        public string id_tipo_contrato { get; set; }
        public string id_carteira_cobranca { get; set; }
        public string pago_ate_data { get; set; }
        public string status_velocidade { get; set; }
        public string id_vendedor { get; set; }
        public string comissao { get; set; }
        public string nao_avisar_ate { get; set; }
        public string nao_bloquear_ate { get; set; }
        public string tipo_doc_opc { get; set; }
        public string id_tipo_documento { get; set; }
        public string aviso_atraso { get; set; }
        public string tipo_doc_opc2 { get; set; }
        public string tipo_doc_opc3 { get; set; }
        public string obs { get; set; }
        public string tipo_doc_opc4 { get; set; }
        public string id_modelo { get; set; }
        public string desbloqueio_confianca { get; set; }
        public string cc_previsao { get; set; }
        public string desbloqueio_confianca_ativo { get; set; }
        public string data_negativacao { get; set; }
        public string data_acesso_desativado { get; set; }
        public string motivo_cancelamento { get; set; }
        public string data_cancelamento { get; set; }
        public string desconto_fidelidade { get; set; }
        public string obs_cancelamento { get; set; }
        public string id_vendedor_ativ { get; set; }
        public string fidelidade { get; set; }
        public string taxa_improdutiva { get; set; }
        public string id_responsavel { get; set; }
        public string taxa_instalacao { get; set; }
        public string dt_ult_bloq_auto { get; set; }
        public string dt_ult_bloq_manual { get; set; }
        public string dt_ult_des_bloq_conf { get; set; }
        public string dt_ult_ativacao { get; set; }
        public string dt_ult_finan_atraso { get; set; }
        public string tipo_cobranca { get; set; }
        public string dt_utl_negativacao { get; set; }
        public string data_cadastro_sistema { get; set; }
        public string ultima_atualizacao { get; set; }
        public string endereco { get; set; }
        public string protocolo_negativacao { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string renovacao_automatica { get; set; }
        public string descricao_aux_plano_venda { get; set; }
        public string avalista_1 { get; set; }
        public string avalista_2 { get; set; }
        public string imp_importacao { get; set; }
        public string imp_rede { get; set; }
        public string imp_bkp { get; set; }
        public string imp_treinamento { get; set; }
        public string imp_status { get; set; }
        public string imp_obs { get; set; }
        public string imp_realizado { get; set; }
        public string imp_motivo { get; set; }
        public string imp_inicial { get; set; }
        public string imp_final { get; set; }
        public string ativacao_numero_parcelas { get; set; }
        public string ativacao_vencimentos { get; set; }
        public string ativacao_valor_parcela { get; set; }
        public string id_tipo_doc_ativ { get; set; }
        public string id_produto_ativ { get; set; }
        public string id_cond_pag_ativ { get; set; }
        public string endereco_padrao_cliente { get; set; }
        public string numero { get; set; }
        public string cep { get; set; }
        public string complemento { get; set; }
        public string referencia { get; set; }
        public string id_condominio { get; set; }
        public string nf_info_adicionais { get; set; }
        public string assinatura_digital { get; set; }
        public string tipo_produtos_plano { get; set; }
        public string bloco { get; set; }
        public string apartamento { get; set; }
        public string motivo_inclusao { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string liberacao_bloqueio_manual { get; set; }
        public string num_parcelas_atraso { get; set; }
        public string dt_ult_desiste { get; set; }
        public string id_contrato_principal { get; set; }
        public string gerar_finan_assin_digital_contrato { get; set; }
        public string credit_card_recorrente_token { get; set; }
        public string credit_card_recorrente_bandeira_cartao { get; set; }
        public string id_motivo_negativacao { get; set; }
        public string obs_negativacao { get; set; }
        public string restricao_auto_desbloqueio { get; set; }
        public string motivo_restricao_auto_desbloq { get; set; }
    }
}
