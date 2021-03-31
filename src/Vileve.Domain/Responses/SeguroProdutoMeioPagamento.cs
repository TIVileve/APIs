using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class SeguroProdutoMeioPagamento
    {
        [JsonProperty("codigo_id_banco")]
        public int CodigoIdBanco { get; set; }
        [JsonProperty("codigo_bancario")]
        public int CodigoBancario { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("meio_pagamento")]
        public string MeioPagamento { get; set; }
        [JsonProperty("prefixo")]
        public string Prefixo { get; set; }
    }
}