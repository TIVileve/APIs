using System;
using FluentValidation;
using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public abstract class ClienteValidation<T> : AbstractValidator<T> where T : ClienteCommand
    {
        protected void ValidateClienteId()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty).WithMessage("O campo cliente id é obrigatório.");
        }

        protected void ValidateEnderecoId()
        {
            RuleFor(c => c.EnderecoId)
                .NotEqual(Guid.Empty).WithMessage("O campo endereço id é obrigatório.");
        }

        protected void ValidateDependenteId()
        {
            RuleFor(c => c.DependenteId)
                .NotEqual(Guid.Empty).WithMessage("O campo dependente id é obrigatório.");
        }
    }
}