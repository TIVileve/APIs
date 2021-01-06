using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ParametrizacaoNacionalidade
    {
        [JsonProperty("codigo_nacionalidade")]
        public long CodigoNacionalidade { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("prefixo_pais")]
        public string PrefixoPais { get; set; }
    }
}