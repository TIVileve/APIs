using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class CadastrarDependenteCommandValidation : ClienteValidation<CadastrarDependenteCommand>
    {
        public CadastrarDependenteCommandValidation()
        {
            ValidateClienteId();
        }
    }
}