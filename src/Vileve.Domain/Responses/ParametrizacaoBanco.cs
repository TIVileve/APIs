using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ParametrizacaoBanco
    {
        [JsonProperty("codigo_banco")]
        public long CodigoBanco { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}