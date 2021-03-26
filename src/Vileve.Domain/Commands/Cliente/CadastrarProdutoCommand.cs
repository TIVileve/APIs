using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarProdutoCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarProdutoCommand(Guid clienteId, string codigoProdutoItem)
        {
            ClienteId = clienteId;
            CodigoProdutoItem = codigoProdutoItem;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarProdutoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}