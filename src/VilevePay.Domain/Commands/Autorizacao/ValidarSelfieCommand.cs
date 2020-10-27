using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class ValidarSelfieCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarSelfieCommand(string codigoConvite, string fotoBase64)
        {
            CodigoConvite = codigoConvite;
            FotoBase64 = fotoBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarSelfieCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}