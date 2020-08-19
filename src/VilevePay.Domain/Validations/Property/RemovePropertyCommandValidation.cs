using VilevePay.Domain.Commands.Property;

namespace VilevePay.Domain.Validations.Property
{
    public class RemovePropertyCommandValidation : PropertyValidation<RemovePropertyCommand>
    {
        public RemovePropertyCommandValidation()
        {
            ValidateId();
        }
    }
}