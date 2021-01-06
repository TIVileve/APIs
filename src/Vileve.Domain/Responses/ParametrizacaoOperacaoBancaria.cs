using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ParametrizacaoOperacaoBancaria
    {
        [JsonProperty("codigo_operacao")]
        public long CodigoOperacao { get; set; }
        [JsonProperty("tipo")]
        public string Tipo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}