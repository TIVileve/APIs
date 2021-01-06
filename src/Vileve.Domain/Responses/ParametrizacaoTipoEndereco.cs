using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ParametrizacaoTipoEndereco
    {
        [JsonProperty("tipo_endereco")]
        public long TipoEndereco { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}