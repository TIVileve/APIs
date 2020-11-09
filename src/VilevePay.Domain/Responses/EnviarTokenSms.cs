using Newtonsoft.Json;

namespace VilevePay.Domain.Responses
{
    public class EnviarTokenSms
    {
        [JsonProperty("codigo_erro")]
        public int CodigoErro { get; set; }
        public bool Sucesso { get; set; }
    }
}