using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class ObterEnderecoCommand : ConsultorCommand, IRequest<object>
    {
        public ObterEnderecoCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}