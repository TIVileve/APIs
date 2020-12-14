using System;

namespace VilevePay.Application.ViewModels.v1.Cliente
{
    public class CadastrarDependenteViewModel
    {
        public int CodigoParentesco { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TelefoneCelular { get; set; }
        public CadastrarEnderecoViewModel Endereco { get; set; }
    }
}