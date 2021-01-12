using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ResponseError
    {
        [JsonProperty("codigo_erro")]
        public long CodigoErro { get; set; }
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
        [JsonProperty("sucesso")]
        public bool Sucesso { get; set; }
        [JsonProperty("erros")]
        public string[] Erros { get; set; }
    }
}