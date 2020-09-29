using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarDadosBancariosCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarDadosBancariosCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarDadosBancariosCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}