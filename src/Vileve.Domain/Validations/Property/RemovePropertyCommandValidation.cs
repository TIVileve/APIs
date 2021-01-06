using Vileve.Domain.Commands.Property;

namespace Vileve.Domain.Validations.Property
{
    public class RemovePropertyCommandValidation : PropertyValidation<RemovePropertyCommand>
    {
        public RemovePropertyCommandValidation()
        {
            ValidateId();
        }
    }
}