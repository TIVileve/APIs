using System;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class ClienteProduto : Entity
    {
        public ClienteProduto(Guid id, string codigoProduto, Guid clienteId)
        {
            Id = id;
            CodigoProduto = codigoProduto;
            ClienteId = clienteId;
        }

        // Empty constructor for EF
        protected ClienteProduto()
        {
        }

        public string CodigoProduto { get; set; }

        public virtual Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }

        public ClienteProduto Update(string codigoProduto)
        {
            CodigoProduto = CodigoProduto;

            return this;
        }
    }
}