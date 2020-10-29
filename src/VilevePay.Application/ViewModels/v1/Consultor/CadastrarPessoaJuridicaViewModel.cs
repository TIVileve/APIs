namespace VilevePay.Application.ViewModels.v1.Consultor
{
    public class CadastrarPessoaJuridicaViewModel
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoEstadual { get; set; }
        public CadastrarDadosBancariosViewModel DadosBancarios { get; set; }
        public string ContratoSocialBase64 { get; set; }
    }
}