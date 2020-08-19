using MediatR;
using VilevePay.Domain.Enums;
using VilevePay.Domain.Validations.Property;

namespace VilevePay.Domain.Commands.Property
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