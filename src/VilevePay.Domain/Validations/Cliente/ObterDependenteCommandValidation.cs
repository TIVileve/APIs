using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class ObterDependenteCommandValidation : ClienteValidation<ObterDependenteCommand>
    {
        public ObterDependenteCommandValidation()
        {
            ValidateClienteId();
        }
    }
}