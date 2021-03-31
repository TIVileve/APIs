using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class ContratarProduto
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("retorno")]
        public ContratarProdutoRetorno Retorno { get; set; }
    }

    public class ContratarProdutoRetorno
    {
        [JsonProperty("codigo_proposta")]
        public int CodigoProposta { get; set; }
        [JsonProperty("codigo_cliente")]
        public int CodigoCliente { get; set; }
        [JsonProperty("valor_total")]
        public double ValorTotal { get; set; }
    }
}