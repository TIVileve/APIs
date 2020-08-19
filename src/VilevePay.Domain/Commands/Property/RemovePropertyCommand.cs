using System;
using MediatR;
using VilevePay.Domain.Validations.Property;

namespace VilevePay.Domain.Commands.Property
{
    public class RemovePropertyCommand : PropertyCommand, IRequest<bool>
    {
        public RemovePropertyCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemovePropertyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}