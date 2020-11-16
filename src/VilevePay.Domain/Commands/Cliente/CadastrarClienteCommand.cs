﻿using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class CadastrarClienteCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarClienteCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}