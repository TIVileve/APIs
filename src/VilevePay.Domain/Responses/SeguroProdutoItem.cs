using Newtonsoft.Json;

namespace VilevePay.Domain.Responses
{
    public class SeguroProdutoItem
    {
        [JsonProperty("codigo_produto_item")]
        public int CodigoProdutoItem { get; set; }
        [JsonProperty("codigo_produto")]
        public int CodigoProduto { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}