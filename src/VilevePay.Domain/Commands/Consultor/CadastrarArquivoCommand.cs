using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarArquivoCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarArquivoCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarArquivoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}