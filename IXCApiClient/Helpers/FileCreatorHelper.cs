using IXCApiClient.Comparers;
using IXCApiClient.Models;
using ManyHelpers.Collection;
using ManyHelpers.Extras;
using ManyHelpers.Strings;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace IXCApiClient.Helpers {
    public class FileCreatorHelper {
        public FileCreatorHelper() {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void CreateFilesCobranca(DateTime data, string path, string nome, List<Titulo> titulos, List<Cliente> clientes, List<Contrato> contratos, IEnumerable<string> excludStatus = null, IEnumerable<string> carteiras = null, bool ignoreStatusAcessoNull = false, bool excludeFaturaAvulsa = false, bool excludeIdRenegociacao = true) {
            var timeStr = new ExecutionHelper().CalculeTime(() => {

                var fileName = $"{path}\\{nome}_{data:yyyyMMdd}.xlsx";

                Console.WriteLine("Preparando Documentos...");

                IEnumerable<BaseCobranca> result = CreateBase(titulos, clientes, contratos, excludeFaturaAvulsa);

                if (excludStatus != null && excludStatus.Any()) {
                    result = result.Where(x => string.IsNullOrEmpty(x.StatusAcesso) || !excludStatus.Contains(x.StatusAcesso));
                    if (ignoreStatusAcessoNull) {
                        result = result.Where(x => !string.IsNullOrEmpty(x.StatusAcesso));
                    }
                }

                if (carteiras != null && carteiras.Any()) {
                    result = result.Where(x => string.IsNullOrEmpty(x.CarteiraCobranca) || carteiras.Contains(x.CarteiraCobranca));
                }

                if (excludeIdRenegociacao) {
                    result = result.Where(x => string.IsNullOrEmpty(StringHelper.GetOnlyPositiveNumbers(x.IDRenogociacao)) &&
                                                    string.IsNullOrEmpty(StringHelper.GetOnlyPositiveNumbers(x.IDRenogociacaoNovo)));
                }

                CreateFile(result, fileName);
            });

            Console.WriteLine($"Extração de {nome} finalizada em: {timeStr}");
        }

        public void CreateFile(IEnumerable<BaseCobranca> result, string fileName) {
            try {
                var i = 1;

                var parts = result.SplitList(800000).ToList();

                foreach (var prt in parts) {
                    try {
                        if (i > 1) {
                            var filInfo = new FileInfo(fileName);
                            var dir = filInfo.Directory;
                            var name = filInfo.Name;
                            fileName = $"{dir}\\Part_{i}_{name}";
                        }

                        Console.WriteLine($"Salvando {fileName}...");

                        var json = JsonConvert.SerializeObject(prt, Formatting.Indented);
                        DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                        using (ExcelPackage pck = new ExcelPackage(new FileInfo(fileName))) {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Central de Cobrança");
                            ws.Cells["A1"].LoadFromDataTable(dt, true);
                            ws.Column(4).Style.Numberformat.Format = "dd/MM/yyyy";
                            ws.Column(17).Style.Numberformat.Format = "dd/MM/yyyy";
                            ws.Column(19).Style.Numberformat.Format = "dd/MM/yyyy";
                            ws.Column(21).Style.Numberformat.Format = "dd/MM/yyyy";
                            ws.Column(34).Style.Numberformat.Format = "dd/MM/yyyy";
                            ws.Column(35).Style.Numberformat.Format = "dd/MM/yyyy";
                            ws.Cells["A1:AJ1"].AutoFilter = true;
                            pck.Save();
                        }


                        Console.WriteLine($"{fileName} foi salvo!!");
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }


                    i++;
                }

            } catch (Exception e) {
                Console.WriteLine($"Um erro ocorreu: {e.Message}");
            }
        }


        public IEnumerable<BaseCobranca> CreateBase(List<Titulo> docs, List<Cliente> clientes, List<Contrato> contratos, bool excludeAvuse = false) {
            var baseCob = from doc in docs
                          join cliente in clientes on StringHelper.GetOnlyPositiveNumbers(doc.id_cliente) equals StringHelper.GetOnlyPositiveNumbers(cliente.id)
                          join c in contratos on StringHelper.GetOnlyPositiveNumbers(doc.id_contrato.ToString()) equals StringHelper.GetOnlyPositiveNumbers(c.id)
                          into eC from contrato in eC.DefaultIfEmpty()
                          select new BaseCobranca {
                              CodigoCliente = cliente.id,
                              CNPJ_CNPF = cliente.cnpj_cpf,
                              Nome = cliente.razao,
                              Nascimento = DateTime.TryParse(cliente.data_nascimento, out DateTime dataNascimento) ? dataNascimento : new DateTime(),
                              TelCelular = cliente.telefone_celular,
                              TelComercial = cliente.telefone_comercial,
                              TelResidencial = cliente.fone,
                              WhatsApp = cliente.whatsapp,
                              Email = cliente.email,
                              Bairro = cliente.bairro,
                              Cidade = cliente.cidade,
                              Endereco = cliente.endereco,
                              CEP = cliente.cep,
                              Documento = doc.id,
                              DocStatus = doc.status,
                              Valor = doc.valor.Replace(".", ","),
                              Vencimento = DateTime.TryParse(doc.data_vencimento, out DateTime dataVenc) ? dataVenc : new DateTime(),
                              ValorPago = doc.pagamento_valor,
                              Pagamento = DateTime.TryParse(doc.pagamento_data, out DateTime dataPgto) ? dataPgto : new DateTime(),
                              LinhaDigitavel = doc.linha_digitavel,
                              DataAtivacao = DateTime.TryParse(contrato?.data_ativacao, out DateTime dataAtivacao) ? dataAtivacao : new DateTime(),
                              CarteiraCobranca = doc.id_carteira_cobranca,
                              CodigoContrato = contrato?.id,
                              ClienteStatusAtivo = cliente.ativo,
                              ContratoStatus = contrato?.status,
                              VendedorId = contrato?.id_vendedor,
                              IDCondominio = cliente.id_condominio,
                              FilialId = doc.filial_id,
                              PlanoId = contrato?.id_vd_contrato,
                              Plano = contrato?.contrato,
                              StatusAcesso = contrato?.status_internet,
                              MotivoCancelamento = contrato?.motivo_cancelamento,
                              DataCancelamento = DateTime.TryParse(contrato?.data_cancelamento, out DateTime dataCancelamento) ? dataCancelamento : new DateTime(),
                              DataDesativacao = DateTime.TryParse(contrato?.data_acesso_desativado, out DateTime dataDesativacao) ? dataDesativacao : new DateTime(),
                              CondicaoPagamento = contrato?.tipo_condicao_pag,
                              IDRenogociacao = doc.id_renegociacao,
                              IDRenogociacaoNovo = doc.id_renegociacao_novo
                          };

            var bcob = baseCob.Distinct(new BaseCobancaComparer());

            if (excludeAvuse) {
                bcob = bcob.Where(x => !string.IsNullOrEmpty(x.CodigoContrato));
            }

            return bcob;
        }
    }
}
