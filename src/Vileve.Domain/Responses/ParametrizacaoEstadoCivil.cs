using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ParametrizacaoEstadoCivil
    {
        [JsonProperty("codigo_estado_civil")]
        public long CodigoEstadoCivil { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}