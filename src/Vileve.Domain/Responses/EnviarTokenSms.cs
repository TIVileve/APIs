using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class EnviarTokenSms
    {
        [JsonProperty("codigo_erro")]
        public int CodigoErro { get; set; }
        public bool Sucesso { get; set; }
    }
}