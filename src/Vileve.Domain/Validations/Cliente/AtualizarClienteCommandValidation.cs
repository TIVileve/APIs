using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class AtualizarClienteCommandValidation : ClienteValidation<AtualizarClienteCommand>
    {
        public AtualizarClienteCommandValidation()
        {
            ValidateClienteId();
        }
    }
}