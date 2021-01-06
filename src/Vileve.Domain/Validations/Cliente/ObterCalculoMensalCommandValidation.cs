using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class ObterCalculoMensalCommandValidation : ClienteValidation<ObterCalculoMensalCommand>
    {
        public ObterCalculoMensalCommandValidation()
        {
            ValidateClienteId();
        }
    }
}