﻿using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
{
    public class ObterBancoCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterBancoCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterBancoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}