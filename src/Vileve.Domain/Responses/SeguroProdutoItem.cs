using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class SeguroProdutoItem
    {
        [JsonProperty("codigo_produto_item")]
        public int CodigoProdutoItem { get; set; }
        [JsonProperty("codigo_produto")]
        public int CodigoProduto { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("valor")]
        public double Valor { get; set; }
        [JsonProperty("descricao")]
        public string Descricao { get; set; }
        [JsonProperty("quantidade_maxima_dependente")]
        public int QuantidadeMaximaDependente { get; set; }
        [JsonProperty("parentescos_avulsos")]
        public IEnumerable<SeguroProdutoParentescos> ParentescosAvulsos { get; set; }
    }
}