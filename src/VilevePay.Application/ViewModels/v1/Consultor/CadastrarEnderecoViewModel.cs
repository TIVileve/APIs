namespace VilevePay.Application.ViewModels.v1.Consultor
{
    public class CadastrarEnderecoViewModel
    {
        public int TipoEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Principal { get; set; }
        public string ComprovanteBase64 { get; set; }
    }
}