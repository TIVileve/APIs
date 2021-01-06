using System;
using MediatR;
using Vileve.Domain.Validations.Property;

namespace Vileve.Domain.Commands.Property
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