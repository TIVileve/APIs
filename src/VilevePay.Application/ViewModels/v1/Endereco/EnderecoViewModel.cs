namespace VilevePay.Application.ViewModels.v1.Endereco
{
    public class EnderecoViewModel
    {
        public int CodigoCidade { get; set; }
        public int CodigoUf { get; set; }
        public int IbgeMunicipio { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string TipoLogradouro { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public int Resultado { get; set; }
    }
}