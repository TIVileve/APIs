using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class EnderecoCep
    {
        [JsonProperty("codigo_cidade")]
        public long CodigoCidade { get; set; }
        [JsonProperty("codigo_uf")]
        public long CodigoUf { get; set; }
        [JsonProperty("ibge_municipio")]
        public long IbgeMunicipio { get; set; }
        [JsonProperty("cidade")]
        public string Cidade { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("tipo_logradouro")]
        public string TipoLogradouro { get; set; }
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("resultado")]
        public long Resultado { get; set; }
    }
}