using System;
using Vileve.Domain.Core.Commands;

namespace Vileve.Domain.Commands.Cliente
{
    public abstract class ClienteCommand : Command
    {
        public Guid ClienteId { get; protected set; }

        // Cliente
        public string Cpf { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public DateTime DataNascimento { get; protected set; }
        public string Email { get; protected set; }
        public string TelefoneFixo { get; protected set; }
        public string TelefoneCelular { get; protected set; }
        public Guid? ConsultorId { get; protected set; }

        // Produto
        public string CodigoProduto { get; protected set; }

        // Endereco
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string ComprovanteBase64 { get; set; }

        // Dependente
        public Guid DependenteId { get; protected set; }
    }
}