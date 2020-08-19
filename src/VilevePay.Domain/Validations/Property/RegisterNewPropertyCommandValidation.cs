using VilevePay.Domain.Commands.Property;

namespace VilevePay.Domain.Validations.Property
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