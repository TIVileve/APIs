namespace Vileve.Infra.CrossCutting.Identity.Models
{
    public class ExternalLoginToken
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}