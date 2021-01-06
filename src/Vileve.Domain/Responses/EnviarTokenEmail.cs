using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class EnviarTokenEmail
    {
        [JsonProperty("codigo_erro")]
        public int CodigoErro { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }
    }
}