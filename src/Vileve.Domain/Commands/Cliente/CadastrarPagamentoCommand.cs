using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarPagamentoCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarPagamentoCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarPagamentoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}