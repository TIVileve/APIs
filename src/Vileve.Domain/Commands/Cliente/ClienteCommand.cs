using System;
using Vileve.Domain.Core.Commands;

namespace Vileve.Domain.Commands.Cliente
{
    public abstract class ClienteCommand : Command
    {
        public Guid ClienteId { get; protected set; }

        // Endereco
        public string Cep { get; protected set; }
        public string Logradouro { get; protected set; }
        public int Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }

        // Dependente
        public Guid DependenteId { get; protected set; }
    }
}