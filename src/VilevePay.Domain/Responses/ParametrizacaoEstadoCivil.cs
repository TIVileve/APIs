using Newtonsoft.Json;

namespace VilevePay.Domain.Responses
{
    public class ParametrizacaoEstadoCivil
    {
        [JsonProperty("codigo_estado_civil")]
        public long CodigoEstadoCivil { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}