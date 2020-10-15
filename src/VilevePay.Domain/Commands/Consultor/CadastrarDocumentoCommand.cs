using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarDocumentoCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarDocumentoCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarDocumentoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}