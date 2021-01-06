using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class ObterDependenteCommandValidation : ClienteValidation<ObterDependenteCommand>
    {
        public ObterDependenteCommandValidation()
        {
            ValidateClienteId();
        }
    }
}