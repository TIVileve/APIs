﻿using System;
using Newtonsoft.Json;

namespace Vileve.Application.ViewModels.v1.Autorizacao
{
    public class TokenViewModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public DateTime? ExpiresIn { get; set; }
        public string CodigoConvite { get; set; }
        public string NumeroCelular { get; set; }
        public string StatusOnboardingDescricao { get; set; }
        public int? StatusOnboarding { get; set; }
        public Guid? ConsultorId { get; set; }
        public string RepresentanteNomeCompleto { get; set; }
        public bool Valido { get; set; }
    }
}