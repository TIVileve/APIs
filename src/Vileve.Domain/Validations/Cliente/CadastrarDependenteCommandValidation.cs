using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class CadastrarDependenteCommandValidation : ClienteValidation<CadastrarDependenteCommand>
    {
        public CadastrarDependenteCommandValidation()
        {
            ValidateClienteId();
        }
    }
}