using VilevePay.Domain.Commands.Property;

namespace VilevePay.Domain.Validations.Property
{
    public class UpdatePropertyCommandValidation : PropertyValidation<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateType();
            ValidateIsRequired();
        }
    }
}