﻿using Newtonsoft.Json;

namespace VilevePay.Domain.Responses
{
    public class ParametrizacaoTipoEmail
    {
        [JsonProperty("tipo_email")]
        public long TipoEmail { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}