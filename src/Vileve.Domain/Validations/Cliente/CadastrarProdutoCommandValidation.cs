using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class CadastrarProdutoCommandValidation : ClienteValidation<CadastrarProdutoCommand>
    {
        public CadastrarProdutoCommandValidation()
        {
            ValidateClienteId();
        }
    }
}