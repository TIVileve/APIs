using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class PessoaJuridica
    {
        [JsonProperty("status")]
        public long Status { get; set; }
        [JsonProperty("codigo_erro")]
        public long CodigoErro { get; set; }
        [JsonProperty("codigo_pessoa")]
        public long CodigoPessoa { get; set; }
        [JsonProperty("sucesso")]
        public bool Sucesso { get; set; }
    }
}