using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ParametrizacaoTipoTelefone
    {
        [JsonProperty("tipo_telefone")]
        public long TipoTelefone { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}