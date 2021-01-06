using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class CadastrarEnderecoCommandValidation : ClienteValidation<CadastrarEnderecoCommand>
    {
        public CadastrarEnderecoCommandValidation()
        {
            ValidateClienteId();
        }
    }
}