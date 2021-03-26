using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class SeguroProduto
    {
        [JsonProperty("codigo_produto")]
        public int CodigoProduto { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("meios_pagamento")]
        public IEnumerable<SeguroProdutoMeioPagamento> MeiosPagamentos { get; set; }
        [JsonProperty("produto_itens")]
        public IEnumerable<SeguroProdutoItem> ProdutoItens { get; set; }
    }
}