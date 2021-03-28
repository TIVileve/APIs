using System;
using Vileve.Domain.Core.Models;

namespace Vileve.Domain.Models
{
    public class ClienteProduto : Entity
    {
        public ClienteProduto(Guid id, string codigoProdutoItem, Guid clienteId)
        {
            Id = id;
            CodigoProdutoItem = codigoProdutoItem;
            ClienteId = clienteId;
        }

        // Empty constructor for EF
        protected ClienteProduto()
        {
        }

        public string CodigoProdutoItem { get; set; }

        public virtual Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }

        public ClienteProduto Update(string codigoProdutoItem)
        {
            CodigoProdutoItem = codigoProdutoItem;

            return this;
        }
    }
}