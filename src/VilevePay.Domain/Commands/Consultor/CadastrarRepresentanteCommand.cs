using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarRepresentanteCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarRepresentanteCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarRepresentanteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}