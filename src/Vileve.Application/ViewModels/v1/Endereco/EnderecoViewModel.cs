namespace Vileve.Application.ViewModels.v1.Endereco
{
    public class EnderecoViewModel
    {
        public long CodigoCidade { get; set; }
        public long CodigoUf { get; set; }
        public long IbgeMunicipio { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string TipoLogradouro { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public long Resultado { get; set; }
    }
}