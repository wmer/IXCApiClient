using IXCApiClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IXCApiClient.Converters {
    public class CallParametersToDictionaryConverter {
        public static Dictionary<string, string> Converter(CallParameters callParameters) {
            var param = new Dictionary<string, string> {
                ["qtype"] = callParameters.Qtype,
                ["query"] = callParameters.Query,
                ["oper"] = callParameters.Operador.Value,
                ["page"] = callParameters.Page,
                ["rp"] = callParameters.Rp,
                ["sortname"] = callParameters.SortName,
                ["sortorder"] = callParameters.SortOrder,
                ["grid_param"] = JsonConvert.SerializeObject(GridParamsConverter(callParameters.GridParams))
            };

            return param;
        }


        public static List<Dictionary<string, string>> GridParamsConverter(List<GridParameter> gridParameters) {
            var grid = new List<Dictionary<string, string>>();
            if (gridParameters != null && gridParameters.Count() > 0) {
                foreach (var parameter in gridParameters) {
                    var dic = new Dictionary<string, string> {
                        ["TB"] = parameter.Property,
                        ["OP"] = parameter.Operador.Value,
                        ["P"] = parameter.Valor
                    };
                    grid.Add(dic);
                }
            }

            return grid;
        }
    }
}
