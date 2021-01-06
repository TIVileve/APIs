using MediatR;
using Vileve.Domain.Validations.Autorizacao;

namespace Vileve.Domain.Commands.Autorizacao
{
    public class ValidarSelfieCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarSelfieCommand(string codigoConvite, string numeroCelular, string fotoBase64)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            FotoBase64 = fotoBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarSelfieCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}