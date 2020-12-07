using System;
using FluentValidation;
using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public abstract class ClienteValidation<T> : AbstractValidator<T> where T : ClienteCommand
    {
        protected void ValidateClienteId()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty).WithMessage("O campo cliente id é obrigatório.");
        }
    }
}