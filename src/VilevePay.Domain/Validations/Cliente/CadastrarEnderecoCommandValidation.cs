using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class CadastrarEnderecoCommandValidation : ClienteValidation<CadastrarEnderecoCommand>
    {
        public CadastrarEnderecoCommandValidation()
        {
            ValidateClienteId();
        }
    }
}