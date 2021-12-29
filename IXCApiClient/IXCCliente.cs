using IXCApiClient.Converters;
using IXCApiClient.Helpers;
using IXCApiClient.Models;
using ManyHelpers.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IXCApiClient {
    public class IXCCliente {
        private CosumingHelper apiConsumerHelper;

        public IXCCliente(string baseUrl, string token) {
            apiConsumerHelper = new CosumingHelper(baseUrl)
                                        .AddcontentType();
        }

        public List<Contrato> GetContratosById(string id) {
            return GetContratos(ContratoStatus.Todos, null, id);
        }

        public List<Contrato> GetContratos(ContratoStatus status, string filialId = null) {
            return GetContratos(status, filialId, null);
        }

        private List<Contrato> GetContratos(ContratoStatus status, string filialId = null, string id = null) {
            var callParameters = new CallParameters<Contrato> {
                Qtype = x=> x.id,
                Query = "",
                Operador = Operadores.Diferente,
                Page = "1",
                Rp = "400000",
                GridParams = new List<GridParameter<Contrato>>()
            };

            if (!string.IsNullOrEmpty(id)) {
                callParameters.GridParams.Add(
                       new GridParameter<Contrato> {
                           Property = x => x.id,
                           Operador = Operadores.Igual,
                           Valor = id
                       }
                 );
            }

            if (status.Value != ContratoStatus.Todos.Value) {
                callParameters.GridParams.Add(
                       new GridParameter<Contrato> {
                           Property = x => x.status,
                           Operador = Operadores.Igual,
                           Valor = status.Value
                       }
                 );
            }

            if (!string.IsNullOrEmpty(filialId)) {
                callParameters.GridParams.Add(
                       new GridParameter<Contrato> {
                           Property = x => x.id_filial,
                           Operador = Operadores.Igual,
                           Valor = filialId
                       }
                 );
            }

            var contratos = Get<Contrato>("/cliente_contrato", callParameters);
            return contratos;
        }

        public Cliente GetClienteById(string id) {
            var clientes = GetClientes(ClienteStatus.Todos, null, id, null);
            return clientes.FirstOrDefault();
        }

        public List<Cliente> GetClientePorDocumento(string cpf_cnpj) {
            var clientes = GetClientes(ClienteStatus.Todos, null, null, cpf_cnpj);
            return clientes;
        }

        public List<Cliente> GetClientes(ClienteStatus status, string filialId = null) {
            var clientes = GetClientes(status, filialId, null, null);
            return clientes;
        }


        private List<Cliente> GetClientes(ClienteStatus status, string filialId = null, string id = null, string cpf_cnpj = null) {
            var callParameters = new CallParameters<Cliente> {
                Qtype = x => x.id,
                Query = "",
                Operador = Operadores.Diferente,
                Page = "1",
                Rp = "400000",
                GridParams = new List<GridParameter<Cliente>>()
            };

            if (status.Value != ClienteStatus.Todos.Value) {
                callParameters.GridParams.Add(
                   new GridParameter<Cliente> {
                       Property = x=> x.ativo,
                       Operador = Operadores.Igual,
                       Valor = status.Value
                   }
                );
            }

            if (!string.IsNullOrEmpty(id)) {
                callParameters.GridParams.Add(
                       new GridParameter<Cliente> {
                           Property = x=> x.id,
                           Operador = Operadores.Igual,
                           Valor = id
                       }
                 );
            }

            if (!string.IsNullOrEmpty(cpf_cnpj)) {
                callParameters.GridParams.Add(
                       new GridParameter<Cliente> {
                           Property =x => x.cnpj_cpf,
                           Operador = Operadores.Igual,
                           Valor = cpf_cnpj
                       }
                 );
            }

            if (!string.IsNullOrEmpty(filialId)) {
                callParameters.GridParams.Add(
                       new GridParameter<Cliente> {
                           Property = x => x.filial_id,
                           Operador = Operadores.Igual,
                           Valor = filialId
                       }
                 );
            }

            var clientes = Get<Cliente>("/cliente", callParameters);
            return clientes;
        }



        public List<Titulo> GetTitulos(TituloStatus status, TituloLiberado liberado, DateTime initDate = default, DateTime finalDate = default, string filialId = null) {
            var callParameters = new CallParameters<Titulo> {
                Qtype =x => x.id_cliente,
                Query = "",
                Operador = Operadores.Diferente,
                Page = "1",
                Rp = "100000",
                SortName = x => x.data_vencimento,
                SortOrder = SortOrder.Ascendente,
                GridParams = new List<GridParameter<Titulo>>()
            };

            if (status.Value != TituloStatus.Todos.Value) {
                callParameters.GridParams.Add(
                       new GridParameter<Titulo> {
                           Property = x => x.status,
                           Operador = Operadores.Igual,
                           Valor = status.Value
                       }
                    );
            }

            if (liberado.Value != TituloLiberado.Todos.Value && status.Value != TituloStatus.Pago.Value) {
                callParameters.GridParams.Add(
                       new GridParameter<Titulo> {
                            Property = x => x.liberado,
                            Operador = Operadores.Igual,
                            Valor = liberado.Value
                       }
                );
            }

            if (!string.IsNullOrEmpty(filialId)) {
                callParameters.GridParams.Add(
                       new GridParameter<Titulo> {
                           Property = x => x.filial_id,
                           Operador = Operadores.Igual,
                           Valor = filialId
                       }
                    );
            }

            Expression<Func<Titulo, IComparable>> dateParam = x => x.data_vencimento;

            if (status.Value == TituloStatus.Pago.Value) {
                dateParam = x => x.pagamento_data;
            }

            if (initDate != default) {
                callParameters.GridParams.Add(
                 new GridParameter<Titulo> {
                     Property = dateParam,
                     Operador = Operadores.MaiorIgaul,
                     Valor = initDate.ToString("yyyyy-MM-dd")
                 }
              );
            }
            if (finalDate != default) {
                callParameters.GridParams.Add(
                 new GridParameter<Titulo> {
                     Property = dateParam,
                     Operador = Operadores.MenorIgual,
                     Valor = finalDate.ToString("yyyyy-MM-dd")
                 }
              );
            }

            var titulos = Get<Titulo>("/fn_areceber", callParameters);
            return titulos;
        }

        public NovoAtendimentoResponse ListarOrdensDeServico() {
            var param = new Dictionary<string, string> {
                ["qtype"] = "su_oss_chamado.id",
                ["query"] = "",
                ["oper"] = "!=",
                ["rp"] = "400000"
            };

            (NovoAtendimentoResponse result, string statusCode, string message) = apiConsumerHelper
                                                                                    .AddCustomHeaders("ixcsoft", "listar")
                                                                                    .PostAsync<Dictionary<string, string>, NovoAtendimentoResponse>("/su_oss_chamado", param).Result;

            return result;
        }

        public NovoAtendimentoResponse ListarAtendimentos() {
            var param = new Dictionary<string, string> {
                ["rp"] = "2000000"
            };

            (NovoAtendimentoResponse result, string statusCode, string message) = apiConsumerHelper
                                                                                    .AddCustomHeaders("ixcsoft", "listar")
                                                                                    .PostAsync<Dictionary<string, string>, NovoAtendimentoResponse>("/su_ticket", param).Result;

            return result;
        }

        public NovoAtendimentoResponse AdicionarAtendimento(string clienteId, string idAssunto, string titulo, string mensagem, string ststus = "S") {
            var param = new Dictionary<string, string> {
                ["protocolo"] = DateTime.Now.ToString("yyyyMMddHHmmss"),
                ["id_cliente"] = clienteId,
                ["titulo"] = titulo,
                ["id_assunto"] = idAssunto,
                ["origem_endereco"] = "M",
                ["id_ticket_setor"] = "26",
                ["prioridade"] = "M",
                ["id_ticket_origem"] = "I",
                ["menssagem"] = mensagem,
                ["interacao_pendente"] = "N",
                ["su_status"] = ststus,
                ["status"] = "T",
                ["atualizar_cliente"] = "S",
                ["atualizar_login"] = "S"
            };

            (NovoAtendimentoResponse result, string statusCode, string message) = apiConsumerHelper
                                                                                    .AddCustomHeaders("ixcsoft", "inserir")
                                                                                    .PostAsync<Dictionary<string, string>, NovoAtendimentoResponse>("/su_ticket", param).Result;

            return result;
        }

        public string GetFatura(string tituloId) {
            var param = new Dictionary<string, string> {
                ["boletos"] = tituloId,
                ["juro"] = "N",
                ["multa"] = "N",
                ["atualiza_boleto"] = "N",
                ["tipo_boleto"] = "arquivo",
                ["base64"] = "S"
            };

            (string result, string statusCode, string message) = apiConsumerHelper
                                                                    .AddCustomHeaders("ixcsoft", "inserir")
                                                                    .PostAsync<Dictionary<string, string>, string>("/get_boleto", param).Result;

            return result;
        }

        public List<T> Get<T>(string endpoint, CallParameters<T> callParameters) {
            var list = new List<T>();
            IXCResponse<T> result = Call<T>(endpoint, callParameters);
            if (result != null && result.registros != null && result.registros.Count() > 0) {
                list = result.registros;

                if (int.TryParse(result.total, out int totalRegistros) && totalRegistros > result.registros.Count()) {
                    var i = 2;
                    while (list.Count() < totalRegistros) {
                        if (result != null && result.registros != null && result.registros.Count() > 0) {
                            callParameters.Page = i.ToString();
                            result = Call<T>(endpoint, callParameters);
                            if (result != null && result.registros != null && result.registros.Count() > 0) {
                                list.AddRange(result.registros);
                            }
                            i++;
                        }
                    }
                }
            }
            return list;
        }

        private IXCResponse<T> Call<T>(string endpoint, CallParameters<T> callParameters) {
            (IXCResponse<T> result, string statusCode, string message) = apiConsumerHelper
                                                                            .AddCustomHeaders("ixcsoft", "listar")
                                                                            .PostAsync<Dictionary<string, string>, IXCResponse <T>>(endpoint, CallParametersToDictionaryConverter.Converter(callParameters)).Result;
            return result;
        }
    }
}
