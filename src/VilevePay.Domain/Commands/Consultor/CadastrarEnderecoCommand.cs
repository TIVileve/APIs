using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarEnderecoCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarEnderecoCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}