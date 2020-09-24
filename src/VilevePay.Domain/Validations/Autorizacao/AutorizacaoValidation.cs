using FluentValidation;
using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public abstract class AutorizacaoValidation<T> : AbstractValidator<T> where T : AutorizacaoCommand
    {
        protected void ValidateCodigoConvite()
        {
            RuleFor(a => a.CodigoConvite)
                .NotEmpty().WithMessage("O campo código do convite é obrigatório.")
                .Length(6).WithMessage("O campo código do convite deve ter 6 caracteres.");
        }

        protected void ValidateNumeroCelular()
        {
            RuleFor(a => a.NumeroCelular)
                .NotEmpty().WithMessage("O campo número de celular é obrigatório.")
                .Length(13, 14).WithMessage("O campo número de celular deve ter entre 13 e 14 caracteres.");
        }

        protected void ValidateCodigoToken()
        {
            RuleFor(a => a.CodigoToken)
                .NotEmpty().WithMessage("O campo código do token é obrigatório.")
                .Length(4).WithMessage("O campo código do token deve ter 4 caracteres.");
        }

        protected void ValidateEmail()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("O campo e-mail é obrigatório.")
                .EmailAddress().WithMessage("O campo e-mail está inválido.");
        }

        protected void ValidateSenha()
        {
            RuleFor(a => a.Senha)
                .NotEmpty().WithMessage("O campo senha é obrigatório.");
        }

        protected void ValidateConfirmarSenha()
        {
            RuleFor(a => a.ConfirmarSenha)
                .Equal(a => a.Senha).WithMessage("As senhas devem corresponder.");
        }
    }
}