﻿using Newtonsoft.Json;

namespace Vileve.Domain.Responses
{
    public class SeguroProdutoParentesco
    {
        [JsonProperty("codigo_tipo_dependente")]
        public int CodigoTipoDependente { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}