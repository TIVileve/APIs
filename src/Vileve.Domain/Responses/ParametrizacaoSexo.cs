using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ParametrizacaoSexo
    {
        [JsonProperty("codigo_sexo")]
        public long CodigoSexo { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}