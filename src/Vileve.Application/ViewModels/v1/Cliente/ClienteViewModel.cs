using System;
using System.Collections.Generic;

namespace Vileve.Application.ViewModels.v1.Cliente
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public ClienteProdutoViewModel Produto { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public IEnumerable<DependenteViewModel> Dependentes { get; set; }
    }
}