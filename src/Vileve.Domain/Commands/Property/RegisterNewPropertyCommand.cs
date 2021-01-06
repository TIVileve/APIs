using MediatR;
using Vileve.Domain.Enums;
using Vileve.Domain.Validations.Property;

namespace Vileve.Domain.Commands.Property
{
    public class RegisterNewPropertyCommand : PropertyCommand, IRequest<object>
    {
        public RegisterNewPropertyCommand(string name, Type type, bool isRequired)
        {
            Name = name;
            Type = type;
            IsRequired = isRequired;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewPropertyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}