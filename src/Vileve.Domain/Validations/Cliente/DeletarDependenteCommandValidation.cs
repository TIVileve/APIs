using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class DeletarDependenteCommandValidation : ClienteValidation<DeletarDependenteCommand>
    {
        public DeletarDependenteCommandValidation()
        {
            ValidateClienteId();
            ValidateDependenteId();
        }
    }
}