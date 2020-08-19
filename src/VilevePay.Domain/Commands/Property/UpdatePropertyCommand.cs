using System;
using MediatR;
using VilevePay.Domain.Validations.Property;
using Type = VilevePay.Domain.Enums.Type;

namespace VilevePay.Domain.Commands.Property
{
    public class UpdatePropertyCommand : PropertyCommand, IRequest<bool>
    {
        public UpdatePropertyCommand(Guid id, string name, Type type, bool isRequired)
        {
            Id = id;
            Name = name;
            Type = type;
            IsRequired = isRequired;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePropertyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}