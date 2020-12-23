using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
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