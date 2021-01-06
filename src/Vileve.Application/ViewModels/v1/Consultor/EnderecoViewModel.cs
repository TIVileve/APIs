using System;

namespace Vileve.Application.ViewModels.v1.Consultor
{
    public class EnderecoViewModel
    {
        public Guid Id { get; set; }
        public string TipoEnderecoDescricao { get; set; }
        public int TipoEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Principal { get; set; }
    }
}