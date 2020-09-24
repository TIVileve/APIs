using FluentValidation;
using VilevePay.Domain.Commands.Endereco;

namespace VilevePay.Domain.Validations.Endereco
{
    public abstract class EnderecoValidation<T> : AbstractValidator<T> where T : EnderecoCommand
    {
        protected void ValidateCep()
        {
            RuleFor(e => e.Cep)
                .NotEmpty().WithMessage("O campo cep é obrigatório.")
                .Length(9).WithMessage("O campo cep deve ter 9 caracteres.");
        }
    }
}