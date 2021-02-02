using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class ObterClientePorIdCommandValidation : ClienteValidation<ObterClientePorIdCommand>
    {
        public ObterClientePorIdCommandValidation()
        {
            ValidateClienteId();
        }
    }
}