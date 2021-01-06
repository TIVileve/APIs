using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
{
    public class ObterOperacaoBancariaCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterOperacaoBancariaCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterOperacaoBancariaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}