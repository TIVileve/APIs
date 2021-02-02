using System;

namespace Vileve.Application.ViewModels.v1.Cliente
{
    public class AtualizarDependenteViewModel
    {
        public string CodigoParentesco { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TelefoneCelular { get; set; }
        public AtualizarEnderecoViewModel Endereco { get; set; }
    }
}