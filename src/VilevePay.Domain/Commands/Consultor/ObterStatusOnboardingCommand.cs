﻿using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class ObterStatusOnboardingCommand : ConsultorCommand, IRequest<object>
    {
        public ObterStatusOnboardingCommand(string email)
        {
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterStatusOnboardingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}