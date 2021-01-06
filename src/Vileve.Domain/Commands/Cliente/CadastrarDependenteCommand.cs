using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarDependenteCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarDependenteCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarDependenteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}