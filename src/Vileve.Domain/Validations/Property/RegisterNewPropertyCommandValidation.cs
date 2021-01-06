using Vileve.Domain.Commands.Property;

namespace Vileve.Domain.Validations.Property
{
    public class RegisterNewPropertyCommandValidation : PropertyValidation<RegisterNewPropertyCommand>
    {
        public RegisterNewPropertyCommandValidation()
        {
            ValidateName();
            ValidateType();
            ValidateIsRequired();
        }
    }
}