using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class AtualizarEnderecoCommandValidation : ClienteValidation<AtualizarEnderecoCommand>
    {
        public AtualizarEnderecoCommandValidation()
        {
            ValidateClienteId();
            ValidateEnderecoId();
        }
    }
}