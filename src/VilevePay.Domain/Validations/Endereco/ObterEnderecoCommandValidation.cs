using VilevePay.Domain.Commands.Endereco;

namespace VilevePay.Domain.Validations.Endereco
{
    public class ObterEnderecoCommandValidation : EnderecoValidation<ObterEnderecoCommand>
    {
        public ObterEnderecoCommandValidation()
        {
            ValidateCep();
        }
    }
}