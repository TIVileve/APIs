using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
{
    public class ObterTipoPagamentoCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterTipoPagamentoCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterTipoPagamentoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}