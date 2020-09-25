using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
{
    public class ObterPerfilUsuarioCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterPerfilUsuarioCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterPerfilUsuarioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}