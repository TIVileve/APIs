using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
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