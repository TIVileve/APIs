using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class CadastrarPagamentoCommandValidation : ClienteValidation<CadastrarPagamentoCommand>
    {
        public CadastrarPagamentoCommandValidation()
        {
            ValidateClienteId();
        }
    }
}