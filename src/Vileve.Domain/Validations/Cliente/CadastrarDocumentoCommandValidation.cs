using Vileve.Domain.Commands.Cliente;

namespace Vileve.Domain.Validations.Cliente
{
    public class CadastrarDocumentoCommandValidation : ClienteValidation<CadastrarDocumentoCommand>
    {
        public CadastrarDocumentoCommandValidation()
        {
            ValidateClienteId();
        }
    }
}