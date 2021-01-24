using IXCApiClient.Helpers;
using IXCApiClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IXCApiClient.Converters {
    public class CallParametersToDictionaryConverter {
        public static Dictionary<string, string> Converter<T>(CallParameters<T> callParameters) {
            var param = new Dictionary<string, string> {
                ["qtype"] = GetPropertyName<T>(callParameters.Qtype),
                ["query"] = callParameters.Query,
                ["oper"] = callParameters.Operador.Value,
                ["page"] = callParameters.Page,
                ["rp"] = callParameters.Rp,
                ["sortname"] = GetPropertyName<T>(callParameters.SortName),
                ["sortorder"] = callParameters.SortOrder?.Value,
                ["grid_param"] = JsonConvert.SerializeObject(GridParamsConverter<T>(callParameters.GridParams))
            };

            return param;
        }


        public static List<Dictionary<string, string>> GridParamsConverter<T>(List<GridParameter<T>> gridParameters) {
            var grid = new List<Dictionary<string, string>>();
            if (gridParameters != null && gridParameters.Count() > 0) {
                foreach (var parameter in gridParameters) {
                    var dic = new Dictionary<string, string> {
                        ["TB"] = GetPropertyName<T>(parameter.Property),
                        ["OP"] = parameter.Operador.Value,
                        ["P"] = parameter.Valor
                    };
                    grid.Add(dic);
                }
            }

            return grid;
        }

        private static string GetPropertyName<T>(Expression<Func<T, IComparable>> property) {
            var name = "";
            if (property != null) {
                var tableName = TableHelper.GetTableName<T>();
                var prop = GetPropertyInfoHelper.GetPropertyInfo<T>(property);
                name = $"{tableName}.{prop.Name}";
            }

            return name;
        }
    }
}
