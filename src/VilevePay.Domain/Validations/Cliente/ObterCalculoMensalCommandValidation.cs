using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class ObterCalculoMensalCommandValidation : ClienteValidation<ObterCalculoMensalCommand>
    {
        public ObterCalculoMensalCommandValidation()
        {
            ValidateClienteId();
        }
    }
}