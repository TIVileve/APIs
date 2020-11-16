using FluentValidation;
using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public abstract class ClienteValidation<T> : AbstractValidator<T> where T : ClienteCommand
    {
    }
}