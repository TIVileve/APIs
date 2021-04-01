using System;
using Vileve.Domain.Core.Commands;

namespace Vileve.Domain.Commands.Cliente
{
    public abstract class ClienteCommand : Command
    {
        // Cliente e Dependente
        public Guid ClienteId { get; protected set; }
        public string Cpf { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public DateTime DataNascimento { get; protected set; }
        public string Email { get; protected set; }
        public string TelefoneFixo { get; protected set; }
        private string _telefoneCelular;

        public string TelefoneCelular
        {
            get => _telefoneCelular;
            protected set => _telefoneCelular = string.IsNullOrWhiteSpace(value) ? null : value.Contains("+") ? value : $"+{value}";
        }

        public Guid? ConsultorId { get; protected set; }

        // Fonte Pagadora
        public long? InssNumeroBeneficio { get; protected set; }
        public double? InssSalario { get; protected set; }
        public int? InssEspecie { get; protected set; }
        public int? OutrosDiaPagamento { get; protected set; }

        // Produto
        public string CodigoProdutoItem { get; protected set; }

        // Endereco
        public Guid EnderecoId { get; protected set; }
        public string Cep { get; protected set; }
        public string Logradouro { get; protected set; }
        public int Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }
        public string ComprovanteBase64 { get; protected set; }

        // Dependente
        public Guid DependenteId { get; protected set; }
        public string CodigoParentesco { get; protected set; }
    }
}