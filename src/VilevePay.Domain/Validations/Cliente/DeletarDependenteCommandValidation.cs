using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
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