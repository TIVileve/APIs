using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class AtualizarDependenteCommandValidation : ClienteValidation<AtualizarDependenteCommand>
    {
        public AtualizarDependenteCommandValidation()
        {
            ValidateClienteId();
            ValidateDependenteId();
        }
    }
}