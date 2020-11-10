using Newtonsoft.Json;

namespace VilevePay.Domain.Responses
{
    public class ParametrizacaoPerfilUsuario
    {
        [JsonProperty("codigo_perfil")]
        public long CodigoPerfil { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}