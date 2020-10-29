namespace VilevePay.Application.ViewModels.v1.Consultor
{
    public class CadastrarDadosBancariosViewModel
    {
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string ContaSemDigito { get; set; }
        public string Digito { get; set; }
        public int TipoConta { get; set; }
    }
}