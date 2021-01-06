using Vileve.Domain.Commands.Property;

namespace Vileve.Domain.Validations.Property
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