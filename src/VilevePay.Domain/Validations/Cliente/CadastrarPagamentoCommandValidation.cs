using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class CadastrarPagamentoCommandValidation : ClienteValidation<CadastrarPagamentoCommand>
    {
        public CadastrarPagamentoCommandValidation()
        {
            ValidateClienteId();
        }
    }
}