using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class ValidarPessoaJuridicaCommand : ConsultorCommand, IRequest<bool>
    {
        public ValidarPessoaJuridicaCommand(string cnpj)
        {
            Cnpj = cnpj;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarPessoaJuridicaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}