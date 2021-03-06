﻿using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class ObterEnderecoCommand : ConsultorCommand, IRequest<object>
    {
        public ObterEnderecoCommand(string codigoConvite, string numeroCelular)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}