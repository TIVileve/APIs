﻿using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
{
    public class ObterSexoCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterSexoCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterSexoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}