using System;
using Newtonsoft.Json;

namespace VilevePay.Application.ViewModels.v1.Autorizacao
{
    public class TokenViewModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public DateTime ExpiresIn { get; set; }
    }
}