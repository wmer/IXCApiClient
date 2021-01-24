using IXCApiClient.Converters;
using IXCApiClient.Helpers;
using IXCApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IXCApiClient {
    public class IXCCliente {
        private ApiConsumerHelper apiConsumerHelper;

        public IXCCliente(string baseUrl, string token) {
            apiConsumerHelper = new ApiConsumerHelper(baseUrl, token);
        }

        public List<Contrato> GetContratosById(string id) {
            return GetContratos(null, null, id);
        }

        public List<Contrato> GetContratos(string filialId = null) {
            return GetContratos(filialId);
        }

        public List<Contrato> GetContratosInativos(string filialId = null) {
            return GetContratos(filialId, "I");
        }

        public List<Contrato> GetContratosAtivos(string filialId = null) {
            return GetContratos(filialId, "A");
        }

        public List<Contrato> GetPreContratos(string filialId = null) {
            return GetContratos(filialId, "P");
        }

        public List<Contrato> GetContratosNegativados(string filialId = null) {
            return GetContratos(filialId, "N");
        }

        public List<Contrato> GetContratosDesistentes(string filialId = null) {
            return GetContratos(filialId, "D");
        }

        private List<Contrato> GetContratos(string filialId = null, string status = null, string id = null) {
            var callParameters = new CallParameters<Contrato> {
                Qtype = x=> x.id,
                Query = "",
                Operador = Operadores.Diferente,
                Page = "1",
                Rp = "200000",
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

            if (!string.IsNullOrEmpty(status)) {
                callParameters.GridParams.Add(
                       new GridParameter<Contrato> {
                           Property = x => x.status,
                           Operador = Operadores.Igual,
                           Valor = status
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

        public List<Cliente> GetClientes(string filialId = null, bool apenasAtivos = true) {
            var clientes = GetClientes(filialId, null, null, apenasAtivos);
            return clientes;
        }

        public Cliente GetClienteById(string id) {
            var clientes = GetClientes(null, id, null, false);
            return clientes.FirstOrDefault();
        }

        public List<Cliente> GetClientePorDocumento(string cpf_cnpj) {
            var clientes = GetClientes(null, null, cpf_cnpj, false);
            return clientes;
        }

        private List<Cliente> GetClientes(string filialId = null, string id = null, string cpf_cnpj = null, bool apenasAtivos = true) {
            var callParameters = new CallParameters<Cliente> {
                Qtype = x => x.id,
                Query = "",
                Operador = Operadores.Diferente,
                Page = "1",
                Rp = "200000",
                GridParams = new List<GridParameter<Cliente>>()
            };

            if (apenasAtivos) {
                callParameters.GridParams.Add(
                   new GridParameter<Cliente> {
                       Property = x=> x.ativo,
                       Operador = Operadores.Igual,
                       Valor = "S"
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


        public List<Titulo> GetTitulos(string filialId = null, bool apenasLiberado = true, DateTime initDate = default, DateTime finalDate = default) {
            var titulos = GetTitulos(null, filialId, apenasLiberado, initDate, finalDate);
            return titulos;
        }

        public List<Titulo> GetTitulosAReceber(string filialId = null, bool apenasLiberado = true, DateTime initDate = default, DateTime finalDate = default) {
            var titulos = GetTitulos("A", filialId, apenasLiberado, initDate, finalDate);
            return titulos;
        }

        public List<Titulo> GetTitulosParciais(string filialId = null, bool apenasLiberado = true, DateTime initDate = default, DateTime finalDate = default) {
            var titulos = GetTitulos("P", filialId, apenasLiberado, initDate, finalDate);
            return titulos;
        }

        public List<Titulo> GetTitulosCancelados(string filialId = null, bool apenasLiberado = true, DateTime initDate = default, DateTime finalDate = default) {
            var titulos = GetTitulos("C", filialId, apenasLiberado, initDate, finalDate);
            return titulos;
        }
        public List<Titulo> GetTitulosPagos(string filialId = null, DateTime initDate = default, DateTime finalDate = default) {
            var titulos = GetTitulos("R", filialId, false, initDate, finalDate, true);
            return titulos;
        }

        private List<Titulo> GetTitulos(string status = null, string filialId = null, bool apenasLiberado = true, DateTime initDate = default, DateTime finalDate = default, bool filterByPagDate = false) {
            var callParameters = new CallParameters<Titulo> {
                Qtype =x => x.id_cliente,
                Query = "",
                Operador = Operadores.Diferente,
                Page = "1",
                Rp = "200000",
                SortName = x => x.data_vencimento,
                SortOrder = SortOrder.Descendente,
                GridParams = new List<GridParameter<Titulo>>()

            };

            if (!string.IsNullOrEmpty(status)) {
                callParameters.GridParams.Add(
                       new GridParameter<Titulo> {
                           Property = x => x.status,
                           Operador = Operadores.Igual,
                           Valor = status
                       }
                    );
            }

            if (apenasLiberado) {
                callParameters.GridParams.Add(
                       new GridParameter<Titulo> {
                            Property = x => x.liberado,
                            Operador = Operadores.Igual,
                            Valor = "S"
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

            if (filterByPagDate) {
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
            (IXCResponse<T> result, string statusCode, string message) = apiConsumerHelper.Get<IXCResponse<T>>(endpoint, CallParametersToDictionaryConverter.Converter(callParameters));
            return result;
        }
    }
}
