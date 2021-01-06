using Vileve.Domain.Commands.Endereco;

namespace Vileve.Domain.Validations.Endereco
{
    public class ObterEnderecoCommandValidation : EnderecoValidation<ObterEnderecoCommand>
    {
        public ObterEnderecoCommandValidation()
        {
            ValidateCep();
        }
    }
}