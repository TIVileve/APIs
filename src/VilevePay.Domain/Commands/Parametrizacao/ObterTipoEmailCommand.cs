﻿using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
{
    public class ObterTipoEmailCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterTipoEmailCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterTipoEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}