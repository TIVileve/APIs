using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class ObterDependentePorIdCommandValidation : ClienteValidation<ObterDependentePorIdCommand>
    {
        public ObterDependentePorIdCommandValidation()
        {
            ValidateClienteId();
            ValidateDependenteId();
        }
    }
}