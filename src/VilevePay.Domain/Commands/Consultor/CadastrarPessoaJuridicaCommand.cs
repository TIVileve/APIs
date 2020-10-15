using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class CadastrarPessoaJuridicaCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarPessoaJuridicaCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarPessoaJuridicaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}