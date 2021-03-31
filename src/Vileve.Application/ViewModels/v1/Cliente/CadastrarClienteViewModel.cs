using System;

namespace Vileve.Application.ViewModels.v1.Cliente
{
    public class CadastrarClienteViewModel
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public long? InssNumeroBeneficio { get; set; }
        public double? InssSalario { get; set; }
        public int? InssEspecie { get; set; }
        public int? OutrosDiaPagamento { get; set; }
    }
}