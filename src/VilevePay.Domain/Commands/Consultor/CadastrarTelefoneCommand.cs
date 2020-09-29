using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarTelefoneCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarTelefoneCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarTelefoneCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}