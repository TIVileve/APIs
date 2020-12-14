using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
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