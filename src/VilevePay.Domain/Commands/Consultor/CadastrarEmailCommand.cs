using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarEmailCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarEmailCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}