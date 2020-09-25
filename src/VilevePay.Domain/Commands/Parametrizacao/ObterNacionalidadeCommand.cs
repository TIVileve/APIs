using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
{
    public class ObterNacionalidadeCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterNacionalidadeCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterNacionalidadeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}