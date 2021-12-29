using System;
using System.Collections.Generic;
using System.Text;

namespace IXCApiClient.Models {
    public class NovoAtendimentoResponse {
        public string type { get; set; }
        public string message { get; set; }
        public int id { get; set; }
        public List<CampoAtualizado> atualiza_campos { get; set; }
    }
}
