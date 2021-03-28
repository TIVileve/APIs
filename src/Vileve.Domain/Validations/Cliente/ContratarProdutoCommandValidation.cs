using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class ContratarProdutoCommandValidation : ClienteValidation<ContratarProdutoCommand>
    {
        public ContratarProdutoCommandValidation()
        {
            ValidateClienteId();
        }
    }
}